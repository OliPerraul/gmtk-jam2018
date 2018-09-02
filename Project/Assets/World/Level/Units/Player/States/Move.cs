using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSLevel;


namespace NSPlayer
{
    public class Move : Listening
    {

        Vector3 tpos = Vector3.negativeInfinity;
        [SerializeField]
        float movingSPeedLatedSystem = .4f;

        bool directionSEtted = false;


        private Block interestBlock = null;


        public override string GetName()
        {
            return "Move";
        }


        // Use this for initialization
        public override void Enter(AState from, params GameObject[] args)
        {
            Context.anim.SetBool("Walk", true);

            path = args[0].GetComponent<Path>();
            turn = true;
            moving = true;
            directionSEtted = false;

        }

        public override void Tick()
        {
            DoMove();
            base.Tick();
        }


        public void DoMove()
        {
            if (path.stack.Count > 0)
            {
                Block t = path.stack.Peek();
                tpos = t.transform.position;

                if (!directionSEtted)
                {
                    Quaternion look = Quaternion.LookRotation(tpos - Context.transform.position, Vector3.up);
                    Context.transform.rotation = look;
                    directionSEtted = true;
                        
                 }

                if (!VectorUtil.SufficientlyClose(Context.transform.position, tpos))
                {
                    Context.transform.position = Vector3.Lerp(Context.transform.position, tpos, movingSPeedLatedSystem);
                                   
                }
                else
                {
                    //Block center reached
                    Context.transform.position = tpos;
                    path.stack.Pop();
                    directionSEtted = false;
                
                    //// If second to last and job is 'Interact' then stop
                    //if (interestBlock.unit != null)
                    //{
                    //    if (path.stack.Count == 0)
                    //    {
                    //        int iasd = 0;
                    //    }

                    //    if (path.stack.Peek() == interestBlock)
                    //    {
                    //        path.stack.Pop();
                    //        turn = false;
                    //        moving = false;

                    //        // Interact

                    //        Context.fsm.SwitchState("Interact", interestBlock.unit.gameObject);
                    //        return;

                    //    }
                    //}

                }
            }
            else
            {
                turn = false;
                moving = false;

                //if (path.interactOnFinished)
                //{
                //    Context.fsm.SwitchState("Interact", path.interactionUnit.gameObject, path.destination.gameObject);
                //}
                //else
                //{

                //TODO Interact on end move
                Context.fsm.SwitchState("Idle", path.destination.gameObject);
                //}
 
            }
        }

        public override void Exit(AState to)
        {
            base.Exit(to);
            Destroy(path.gameObject);
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




    }
}

