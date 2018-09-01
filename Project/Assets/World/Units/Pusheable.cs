using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSLevel;
using UnityEngine.Events;

namespace NSUnit
{
    public class Pusheable : Unit
    {
        [SerializeField]
        private float moveSpeed = 0.04f;
        private bool stable = true;
        private bool falling = false;
        private bool destroy = false;


        [SerializeField]
        private float fallspeed = 10;


        Vector3 targetPosition;


        private void Update()
        {

            // TODO FSM??
            if (!stable)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed);

                if (VectorUtil.SufficientlyClose(transform.position, targetPosition))
                {

                    if (falling)
                    {
                        targetPosition.y = transform.position.y -fallspeed;
                        falling = false;
                        return;
                    }

                    if (destroy)
                    {
                        Destroy(gameObject);
                        return;
                    }


                    stable = true;
                    onResponseFinished.Invoke();
                    
                    
                }


            }
        }


        public override void Respond(Unit unit)
        {
            base.Respond(unit);
            // TODO prompt player wait.

            Vector3 direction = unit.transform.forward;
            if (CanPush(direction))
            {
                Push(direction);
            }
        }


        public void Push(Vector3 direction)
        {
            Block neighbour;
            stable = false;
            block.walkable = true;
            block.unit = null;
            if (block.GetNeighbour(direction, out neighbour))
            {
                block = neighbour;
                block.unit = this;
                block.walkable = false;

                targetPosition = block.transform.position;
            }
            else
            {
               Fall(direction);
            }
        }



        /// TODO
        private bool CanPush(Vector3 direction)
        {
            return true;
        }


        private void Fall(Vector3 direction)
        {
            targetPosition = transform.position + direction * 2;
            falling = true;
            destroy = true;
         }



    }
}
