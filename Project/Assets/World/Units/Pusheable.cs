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
        
        
        Vector3 targetPosition;


        private void Update()
        {

            // TODO FSM??
            if (!stable)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed);

                if (VectorUtil.SufficientlyClose(transform.position, targetPosition))
                {
                    onResponseFinished.Invoke();
                    stable = true;
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
            block.unit = null;
            if (block.GetNeighbour(direction, out neighbour))
            {
                neighbour.unit = this;
                targetPosition = neighbour.transform.position;
            }
            else
            {
                Fall();
            }
        }



        /// TODO
        private bool CanPush(Vector3 direction)
        {
            return true;
        }


        private void Fall()
        {


        }



    }
}
