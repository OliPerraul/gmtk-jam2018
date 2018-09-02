using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSLevel;
using UnityEngine.Events;

public class Unit: MonoBehaviour
{

    public enum TYPE
    {
        DEFAULT,
        PLAYER,
        PUSHEABLE,
        HARVESTABLE
    }

    [SerializeField]
    public TYPE type = TYPE.DEFAULT;


    [SerializeField]
    protected float rayCastOffsetUp = 4;
    [SerializeField]
    protected float rayCastDistance = 4;


    public UnityEvent onResponseFinished;


    public GameObject model;


    public Block block;



    private Block GetTargetBlock(GameObject target)
    {
        RaycastHit hit;
        Block block = null;

        Ray ray = new Ray(target.transform.position + new Vector3(0, rayCastOffsetUp, 0), -Vector3.up);

        //TODO put layer elsewhere
        if (Physics.Raycast(ray, out hit, rayCastDistance, NSGame.Resources.Instance.collisionLayerBlock))
        {
            block = hit.collider.GetComponent<BlockColliderData>().block;
        }

        return block;
    }

    public Block GetCurrentBlock()
    {
        Block block = GetTargetBlock(gameObject);
        return block;
    }


    public virtual void Respond(Unit unit)
    {
        Debug.Log("Interacted with.");
        block.busy = true;
        // Free busy block on interaction finished
        onResponseFinished.AddListener(block.OnInteractionFinished);
    }



    public void FaceUnit(Unit unit)
    {
        Vector3 forward = unit.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(forward, Vector3.up);  

    }


    //public void CheckBlock(Vector3 direction, float jumpHeight, Block target)
    //{
    //    Vector3 halfExtents = new Vector3(0.25f, (1 + jumpHeight) / 2.0f, 0.25f);
    //    Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

    //    foreach (Collider item in colliders)
    //    {
    //        BlockColliderData blockCollider = item.GetComponent<BlockColliderData>();
    //        if (blockCollider != null && blockCollider.block.walkable)
    //        {
    //            RaycastHit hit;

    //            if (!Physics.Raycast(blockCollider.transform.position, Vector3.up, out hit, 1) || (blockCollider == target))
    //            {
    //                adjacencyList.Add(blockCollider.block);
    //            }
    //        }
    //    }
    //}


}

