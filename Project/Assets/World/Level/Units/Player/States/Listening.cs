using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSLevel;

namespace NSPlayer
{
    public class Listening : PlayerState
    {

        public override void Tick()
        {
            base.Tick();

            if (Context.fsm.Top == this)
            {
                CheckMouse();
            }
        }


        protected void CheckMouse()
        {
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000f, NSGame.Resources.Instance.collisionLayerBlock))
                {
                    NSLevel.Block t = hit.collider.GetComponent<BlockColliderData>().block;

                    if (t.busy)
                        return;

                    // If sel block with unit
                    if (t.selectable && t.unit != null)
                    {
                        //IF CLICK ON SELF DROP TOOLS
                        if (t.unit == Context)
                        {

                            if (Context.equippedTool != NSUnit.Tool.TOOL_TYPE.NONE)
                            {

                                Unit u = null;
                                Block b;
                                switch (Context.equippedTool)
                                {
                                    case NSUnit.Tool.TOOL_TYPE.AXE:
                       
                                                 u = Instantiate(NSLevel.Resources.Instance.toolAxe).GetComponent<Unit>();
                                    break;
                        
                               
                                    case NSUnit.Tool.TOOL_TYPE.SHOVEl:
                         
                                                 u = Instantiate(NSLevel.Resources.Instance.toolShovel).GetComponent<Unit>();
                                        break;
                           

                                    case NSUnit.Tool.TOOL_TYPE.RACK:

                                                 u = Instantiate(NSLevel.Resources.Instance.toolRack).GetComponent<Unit>();

                                        break;
                                        
                                 
                                }

                                bool found = false;
                                var bbb = t.GetAdjacentBlocks();
                                foreach (Block bb in bbb)
                                {
                                    if (bb.unit == null && bb.walkable && !bb.busy)
                                    {
                                       // Context.ChangeEquipTool();
                                        Context.equippedTool = NSUnit.Tool.TOOL_TYPE.NONE;
                                        Context.ChangeEquipTool();
                                        found = true;
                                        u.transform.position = bb.transform.position;
                                        bb.SetUnit(u);
                                        break;
                                    }

                                }

                                if(!found)
                                {
                                    
                                    Context.equippedTool = NSUnit.Tool.TOOL_TYPE.NONE;
                                    Context.ChangeEquipTool();
                                    NSLevel.Level.Instance.RainUnit(u);
                                    Destroy(u.gameObject); //destory old one

                                }

                            }
                            

                            return;
                        }
                    
                        // Only works on idle
                        if (TryInteract(t)) { }

                    }
                    // If sel block alone
                    else if (t.selectable && t.walkable)
                    {

                        if (FindPath(t))
                        {
                            Path path = CreatePath(t);
                            path.interactOnFinished = false;
                            Context.fsm.SwitchState("Move", path.gameObject);
                        }
                    }

                }
                else
                if (Physics.Raycast(ray, out hit, 1000f, NSGame.Resources.Instance.collisionLayerEstablishment))
                {
                    var col = hit.collider.GetComponent<ColliderData>();
                    var est = col.owner.GetComponent<NSEstablishment.Establishment>();
                    TryInteract(est);
                }

            }
        }


        // Overriden
        public virtual bool TryInteract(Block t)
        {
            // NOthing
            return false;
        }


        // Overriden
        public virtual bool TryInteract(NSEstablishment.Establishment est)
        {
            // NOthing
            return false;
        }






        /// BEGIN
        // TODO USE UNIT METHODS


        public void GetCurrentBlock()
        {
            currentBlock = GetTargetBlock(Context.gameObject);
            currentBlock.current = true;
        }

        public Block GetTargetBlock(GameObject target)
        {
            RaycastHit hit;
            Block block = null;

            Ray ray = new Ray(target.transform.position + new Vector3(0, rayCastOffsetUp, 0), -Vector3.up);

            //TODO put layer elsewhere
            if (Physics.Raycast(ray, out hit, 1000f, NSGame.Resources.Instance.collisionLayerBlock))
            {
                block = hit.collider.GetComponent<BlockColliderData>().block;
            }

            return block;
        }


        /// END

        protected bool FindPath(Block target)
        {
            ComputeAdjacencyLists(jumpHeight, target);
            GetCurrentBlock();

            List<Block> openList = new List<Block>();
            List<Block> closedList = new List<Block>();

            openList.Add(currentBlock);
            //currentBlock.parent = ??
            currentBlock.h = Vector3.Distance(currentBlock.transform.position, target.transform.position);
            currentBlock.f = currentBlock.h;

            while (openList.Count > 0)
            {
                Block t = FindLowestF(openList);

                closedList.Add(t);

                if (t == target)
                {
                    //path.destination = FindEndBlock(t);
                    return true;
                }

                foreach (Block block in t.adjacencyList)
                {
                    if (closedList.Contains(block))
                    {
                        //Do nothing, already processed
                    }
                    else if (openList.Contains(block))
                    {
                        float tempG = t.g + Vector3.Distance(block.transform.position, t.transform.position);

                        if (tempG < block.g)
                        {
                            block.parent = t;

                            block.g = tempG;
                            block.f = block.g + block.h;
                        }
                    }
                    else
                    {
                        block.parent = t;

                        block.g = t.g + Vector3.Distance(block.transform.position, t.transform.position);
                        block.h = Vector3.Distance(block.transform.position, target.transform.position);
                        block.f = block.g + block.h;

                        openList.Add(block);
                    }
                }
            }

     
            //todo - what do you do if there is no path to the target block?
            // PATH NOT FOUND
            return false;
        }

        // IF picked interest Block move to neighbouring block instead
        // Do not try to stop
        public Path CreatePath(Block block)
        {
            // MUST BE DESTROYED
            GameObject pathObject = Instantiate(NSGame.Resources.Instance.dummy);
            Path path = pathObject.GetComponent<Path>();

            path.destination = block;

            //path.stack.Clear();
            //block.target = true;
            //moving = true;

            Block next = block;
            while (next != null)
            {
                path.stack.Push(next);

                next = next.parent;
            }

            path.cost = block.g;
            return path;
        }


        public void ComputeAdjacencyLists(float jumpHeight, Block target)
        {
            //NSLevel.Level.Instance.blocks

            foreach (Block block in NSLevel.Level.Instance.blocks)
            {
                block.FindNeighbors(jumpHeight, target);
            }
        }


        protected Block FindLowestF(List<Block> list)
        {
            Block lowest = list[0];

            foreach (Block t in list)
            {
                if (t.f < lowest.f)
                {
                    lowest = t;
                }
            }

            list.Remove(lowest);

            return lowest;
        }


        protected Block FindEndBlock(Block t)
        {
            Stack<Block> tempPath = new Stack<Block>();

            Block next = t.parent;
            while (next != null)
            {
                tempPath.Push(next);
                next = next.parent;
            }

            if (tempPath.Count <= move)
            {
                return t.parent;
            }

            Block endTile = null;
            for (int i = 0; i <= move; i++)
            {
                endTile = tempPath.Pop();
            }

            return endTile;
        }



    }
}
