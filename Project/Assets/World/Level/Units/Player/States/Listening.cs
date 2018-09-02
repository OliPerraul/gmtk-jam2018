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
                        //Do not interact with self
                        if (t.unit == Context)
                            return;

                        // Only works on idle
                        if (TryInteract(t))
                        {
                        }


                        // TODO move and interact ??
                        ////Check path with neighbour
                        //Path bestPath = null;

                        //Block neighbour;
                        //if (t.GetNeighbour(t.transform.forward, out neighbour))
                        //{
                        //    if (FindPath(neighbour))
                        //    {
                        //        Path path = CreatePath(neighbour);
                        //        bestPath = path;
                        //    }
                        //}

                        //if (t.GetNeighbour(-t.transform.forward, out neighbour))
                        //{
                        //    if (FindPath(neighbour))
                        //    {
                        //        Path path = CreatePath(neighbour);
                        //        if (path.cost < bestPath.cost)
                        //        {
                        //            bestPath = path;
                        //        }
                        //    }
                        //}

                        //if (t.GetNeighbour(t.transform.right, out neighbour))
                        //{
                        //    if (FindPath(neighbour))
                        //    {
                        //        Path path = CreatePath(neighbour);
                        //        if (path.cost < bestPath.cost)
                        //        {
                        //            bestPath = path;
                        //        }
                        //    }
                        //}

                        //if (t.GetNeighbour(-t.transform.right, out neighbour))
                        //{
                        //    if (FindPath(neighbour))
                        //    {
                        //        Path path = CreatePath(neighbour);
                        //        if (path.cost < bestPath.cost)
                        //        {
                        //            bestPath = path;
                        //        }
                        //    }
                        //}

                        //if (bestPath != null)
                        //{
                        //    path = bestPath;
                        //    path.interactOnFinished = true;
                        //    path.interactionUnit = t.unit;
                        //    Context.fsm.SwitchState("Move", bestPath.gameObject);
                        //}


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
            }
        }


        // Overriden
        public virtual bool TryInteract(Block t)
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
