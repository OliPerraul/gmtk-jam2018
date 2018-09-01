using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSLevel;


namespace NSUnit
{
    // Carrots, Pumkin, grass, 


    public class Harvestable : Unit
    {
        public int health = 1;
        
        public override void Respond(Unit unit)
        {
            base.Respond(unit);

            health--;
            if (health > 0)
            {
                // TODO prompt animation
                onResponseFinished.Invoke();
            }
            else
            {
                Harvest();

            }
            
            // TODO prompt player wait.

            //Vector3 direction = unit.transform.forward;
            //if (CanPush(direction))
            //{
            //    Push(direction);
            //}
        }

        public void Harvest()
        {
            

        }
    
    }

}

