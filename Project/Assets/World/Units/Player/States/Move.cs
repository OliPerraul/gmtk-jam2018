using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSLevel;


namespace NSPlayer
{
    public class Move : Listening
    {

        private Block interestBlock = null;


        public override string GetName()
        {
            return "Move";
        }


        // Use this for initialization
        public override void Enter(AState from, GameObject arg = null)
        {
            interestBlock = arg.GetComponent<Block>();
            FindPath(interestBlock);
            MoveToBlock(interestBlock);

            turn = true;
            
            // TODO : reconsider call
            FindSelectableBlocks();
        }

        public override void Tick()
        {
            DoMove();
            base.Tick();
        }


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
            if (Physics.Raycast(ray, out hit, rayCastDistance, layer))
            {
                block = hit.collider.GetComponent<BlockColliderData>().block;
            }

            return block;
        }

        public void ComputeAdjacencyLists(float jumpHeight, Block target)
        {
            foreach (Block block in NSLevel.Level.Instance.blocks)
            {
                block.FindNeighbors(jumpHeight, target);
            }
        }

        public void FindSelectableBlocks()
        {

            // TODO block certain block
      
           //selectableBlocks = Level.Instance.blocks;

            //return;

            /// RECONSIDER THIS


            ComputeAdjacencyLists(jumpHeight, null);
            GetCurrentBlock();

            Queue<Block> process = new Queue<Block>();

            process.Enqueue(currentBlock);
            currentBlock.visited = true;
            //currentBlock.parent = ??  leave as null 

            while (process.Count > 0)
            {
                Block t = process.Dequeue();

                selectableBlocks.Add(t);
                t.selectable = true;

                foreach (Block block in t.adjacencyList)
                {
                    if (!block.visited)
                    {
                        block.parent = t;
                        block.visited = true;
                        block.distance = 1 + t.distance;
                        process.Enqueue(block);
                    }
                }
                
            }
        }

        public void MoveToBlock(Block block)
        {
            path.Clear();
            block.target = true;
            moving = true;

            Block next = block;
            while (next != null)
            {
                path.Push(next);
                next = next.parent;
            }
        }

        public void DoMove()
        {
            if (path.Count > 0)
            {
                Block t = path.Peek();
                Vector3 target = t.transform.position;

                if (Vector3.Distance(Context.transform.position, target) >= 0.05f)
                {
                    bool jump = Context.transform.position.y != target.y;

                    if (jump)
                    {
                        Jump(target);
                    }
                    else
                    {
                        CalculateHeading(target);
                        SetHorizotalVelocity();
                    }

                    //Locomotion
                    Context.transform.forward = heading;
                    Context.transform.position += velocity * Time.deltaTime;
                }
                else
                {
                    //Block center reached
                    Context.transform.position = target;
                    path.Pop();
                }
            }
            else
            {
                turn = false;
                moving = false;

                // Interact
                if (interestBlock.unit)
                {
                    Context.fsm.SwitchState("Interact", interestBlock.unit.gameObject);

                }
                else
                {
                    RemoveSelectableBlocks();
                    Context.fsm.SwitchState("Idle");

                }

            }
        }

        protected void RemoveSelectableBlocks()
        {
            if (currentBlock != null)
            {
                currentBlock.current = false;
                currentBlock = null;
            }

            foreach (Block block in selectableBlocks)
            {
                block.Reset();
            }

            selectableBlocks.Clear();
        }

        void CalculateHeading(Vector3 target)
        {
            heading = target - Context.transform.position;
            heading.Normalize();
        }

        void SetHorizotalVelocity()
        {
            velocity = heading * moveSpeed;
        }

        void Jump(Vector3 target)
        {
            if (fallingDown)
            {
                FallDownward(target);
            }
            else if (jumpingUp)
            {
                JumpUpward(target);
            }
            else if (movingEdge)
            {
                MoveToEdge();
            }
            else
            {
                PrepareJump(target);
            }
        }

        void PrepareJump(Vector3 target)
        {
            float targetY = target.y;
            target.y = Context.transform.position.y;

            CalculateHeading(target);

            if (Context.transform.position.y > targetY)
            {
                fallingDown = false;
                jumpingUp = false;
                movingEdge = true;

                jumpTarget = Context.transform.position + (target - Context.transform.position) / 2.0f;
            }
            else
            {
                fallingDown = false;
                jumpingUp = true;
                movingEdge = false;

                velocity = heading * moveSpeed / 3.0f;

                float difference = targetY - Context.transform.position.y;

                velocity.y = jumpVelocity * (0.5f + difference / 2.0f);
            }
        }

        void FallDownward(Vector3 target)
        {
            velocity += Physics.gravity * Time.deltaTime;

            if (Context.transform.position.y <= target.y)
            {
                fallingDown = false;
                jumpingUp = false;
                movingEdge = false;

                Vector3 p = Context.transform.position;
                p.y = target.y;
                Context.transform.position = p;

                velocity = new Vector3();
            }
        }

        void JumpUpward(Vector3 target)
        {
            velocity += Physics.gravity * Time.deltaTime;

            if (Context.transform.position.y > target.y)
            {
                jumpingUp = false;
                fallingDown = true;
            }
        }

        void MoveToEdge()
        {
            if (Vector3.Distance(Context.transform.position, jumpTarget) >= 0.05f)
            {
                SetHorizotalVelocity();
            }
            else
            {
                movingEdge = false;
                fallingDown = true;

                velocity /= 5.0f;
                velocity.y = 1.5f;
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

            //if (tempPath.Count <= move)
            //{
            //    return t.parent;
            //}

            Block endBlock = null;
            for (int i = 0; i <= tempPath.Count; i++)
            {
                endBlock = tempPath.Pop();
            }

            return endBlock;
        }


        protected void FindPath(Block target)
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
            Debug.Log("Path not found");
        }


    }
}

