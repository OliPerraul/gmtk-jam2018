//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace NSPlayer
//{
//    public class GroundedMovement : PlayerState
//    {

//        public override string GetName()
//        {
//            return "GroundedMovement";

//        }


//        public override void Tick()
//        {
//            // Store the input axes.
//            float h = Input.GetAxisRaw("Horizontal");
//            float v = Input.GetAxisRaw("Vertical");

//            // Move the player around the scene.
//            Move(h, v);
//        }


//        void Move(float h, float v)
//        {
//            // Set the movement vector based on the axis input.
//            Context.movement.Set(h, 0f, v);

//            // Normalise the movement vector and make it proportional to the speed per second.
//            ((Player)context).movement = Context.movement.normalized * Context.speed * Time.deltaTime;

//            // Move the player to it's current position plus the movement.
//            Context.playerRigidbody.MovePosition(transform.position + Context.movement);
//        }



//    }
//}
