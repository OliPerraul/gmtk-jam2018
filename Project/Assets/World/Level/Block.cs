using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSLevel
{
    public class Block : MonoBehaviour
    {
        public bool walkable = true;
        public bool current = false;
        public bool target = false;
        public bool selectable = false;
        public bool busy = false;

        [SerializeField]
        bool isShop = false;



        [SerializeField]
        public Unit unit;

        [SerializeField]
        private MeshRenderer _meshRenderer;

        [SerializeField]
        public BlockColliderData colliderData;

        public List<Block> adjacencyList = new List<Block>();

        //Needed BFS (breadth first search)
        public bool visited = false;
        public Block parent = null;
        public int distance = 0;

        //For A*
        public float f = 0;
        public float g = 0;
        public float h = 0;


        // Update is called once per frame
        void Update()
        {
            if (isShop)
            {
                _meshRenderer.material.color = Color.green;
            }
            else
            if (busy)
            {
                _meshRenderer.material.color = Color.cyan;
            }
            else if (unit)
            {
                _meshRenderer.material.color = Color.magenta;
            }
            else if (!walkable)
            {
                _meshRenderer.material.color = Color.red;
            }
            else
            {
                _meshRenderer.material.color = Color.white;
            }
        }

        public void Reset()
        {
            adjacencyList.Clear();

            current = false;
            target = false;
            //selectable = false;

            visited = false;
            parent = null;
            distance = 0;

            f = g = h = 0;
        }

        public void FindNeighbors(float jumpHeight, Block target)
        {
            Reset();

            CheckBlock(Vector3.forward, jumpHeight, target);
            CheckBlock(-Vector3.forward, jumpHeight, target);
            CheckBlock(Vector3.right, jumpHeight, target);
            CheckBlock(-Vector3.right, jumpHeight, target);
        }

        public void CheckBlock(Vector3 direction, float jumpHeight, Block target)
        {
            Vector3 halfExtents = new Vector3(0.25f, (1 + jumpHeight) / 2.0f, 0.25f);
            Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

            foreach (Collider item in colliders)
            {
                BlockColliderData blockCollider = item.GetComponent<BlockColliderData>();
                if (blockCollider != null && blockCollider.block.walkable)
                {
                    RaycastHit hit;

                    if (!Physics.Raycast(blockCollider.transform.position, Vector3.up, out hit, 1) || (blockCollider == target))
                    {
                        adjacencyList.Add(blockCollider.block);
                    }
                }
            }
        }

        public bool GetNeighbour(Vector3 direction, out Block neighbour)
        {
            RaycastHit hit;

            Ray ray = new Ray(transform.position, direction);

            if (Physics.Raycast(ray, out hit, 2, NSGame.Resources.Instance.collisionLayerBlock))
            {
                neighbour = hit.collider.GetComponent<BlockColliderData>().block;
                return true;
            }



            neighbour = null;
            return false;

            //Vector3 halfExtents = new Vector3(0.25f, 1, 0.25f);
            //Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

            //if (colliders.Length > 0)
            //{
            //    neighbour = colliders[0].GetComponent<BlockColliderData>().block;
            //    return true;
            //}

            //neighbour = null;
            //return false;
        }

        public bool GetNeighbour(Vector3 direction, out Block neighbour, out NSEstablishment.Establishment establishment)
        {
            RaycastHit hit;

            Ray ray = new Ray(transform.position, direction);

            //TODO put layer elsewhere
            if (Physics.Raycast(ray, out hit, 2, NSGame.Resources.Instance.collisionLayerEstablishment))
            {
                establishment = hit.collider.GetComponent<ColliderData>().owner.GetComponent<NSEstablishment.Establishment>();
                neighbour = null;
                return true;
            }
            else
            if (Physics.Raycast(ray, out hit, 2, NSGame.Resources.Instance.collisionLayerBlock))
            {
                neighbour = hit.collider.GetComponent<BlockColliderData>().block;
                establishment = null;
                return true;
            }
            
            neighbour = null;
            establishment = null;
            return false;

            //Vector3 halfExtents = new Vector3(0.25f, 1, 0.25f);
            //Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

            //if (colliders.Length > 0)
            //{
            //    neighbour = colliders[0].GetComponent<BlockColliderData>().block;
            //    return true;
            //}

            //neighbour = null;
            //return false;
        }



        // NEW SYSTEM, l, r, t, d
        // TODO use elsewhere
        public List<Block> GetAdjacentBlocks()
        {
            List<Block> adjacent = new List<Block>();

            Block neighbour;
            if (GetNeighbour(transform.forward, out neighbour))
            {
                adjacent.Add(neighbour);

            }

            if (GetNeighbour(-transform.forward, out neighbour))
            {
                adjacent.Add(neighbour);
            }

            if (GetNeighbour(transform.right, out neighbour))
            {
                    adjacent.Add(neighbour);
            }

            if (GetNeighbour(-transform.right, out neighbour))
            {
                adjacent.Add(neighbour);
            }

            return adjacent;
        }


        public void SetUnit(Unit unit)
        {
            unit.block = this;
            this.unit = unit;
            walkable = false;
        }




        public void OnInteractionFinished()
        {
            busy = false;
        }

        ///TODO Fix when pushed on to farm never unbusy
        public void MarkBusy()
        {
            busy = true;
           // Invoke("Unbusy", 5f);
            Invoke("Unbusy", 10f);
        }


        private void Unbusy()
        {
            busy = false;
        }



    }

}
