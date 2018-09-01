using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NSInventory
{

    public class HoverItem : MonoBehaviour
    {

        // TODO use sprite too

        public Item item;

        [SerializeField]
        private MeshFilter _meshFiler;

        [SerializeField]
        private MeshRenderer _meshRenderer;

        [HideInInspector]
        public bool successful = false;


        private void Start()
        {
            if (item != null)
            {
                if (item.useModel)
                {
                    _meshRenderer.material = item.model.material[0];
                    _meshFiler.mesh = item.model.mesh[0];
                }
            }
        }


        // Update is called once per frame
        void Update()
        {
            if (item != null)
            {
                if (item.useModel)
                {
                    _meshRenderer.transform.localPosition = item.model.transform.position;
                    _meshRenderer.transform.localRotation = item.model.transform.rotation;
                    _meshRenderer.transform.localScale = item.model.transform.scale;
                    NSLevel.Level.Instance.select.SnapToSelected(transform);
                }

            }
        }



        //On Object dropped
        public void OnMouseReleased()
        {

            ////flag if so
            //if (Game.instance.money >= item.itemCost)
            //{
            //    Game.instance.money -= item.itemCost;

            //    successful = true;

            //    Vector3Int gridPos = World.instance.GetGridPos(transform);

            //    World.instance.blockmapInterests.SetBlock(gridPos, item.block);

            //    Vector3 pos = World.instance.GetWorldPos(gridPos);

            //    Trap t = GameResources.Create(pos, item.trap).GetComponent<Trap>();

            //    t.gridPos = gridPos;
            //}
        }

    }
}



