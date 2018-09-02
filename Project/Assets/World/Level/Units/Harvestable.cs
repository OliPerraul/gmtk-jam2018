using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSLevel;


namespace NSUnit
{
    // Carrots, Pumkin, grass, 


    public class Harvestable : Unit
    {
        public enum HARVEST_TYPE
        {
            TREE,
            GRASS,


        }
        public HARVEST_TYPE harvestType;


        public int health = 1;

        public int stackSize;

        public Pusheable product;



        private void Start()
        {
            type = TYPE.HARVESTABLE;
        }


        public override void Respond(Unit unit)
        {
            base.Respond(unit);

            if (unit.GetComponent<Player>() == null)
                return;

            if(unit.GetComponent<Player>().equippedTool != Tool.TOOL_TYPE.AXE && harvestType == HARVEST_TYPE.TREE)
                return;


            health--;

            if (health == 0)
            {
                Harvest();
            }
            else
            {
                // TODO prompt animation
                onResponseFinished.Invoke();

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
            

            Pusheable pusheable = Instantiate(product).GetComponent<Pusheable>();

            pusheable.transform.position = block.transform.position;

            pusheable.block = block;
            block.unit = pusheable;
            block.walkable = false;

            //block.SetUnit(pusheable);
            Invoke("NotifyPlayer", 1f);


        }

        private void NotifyPlayer()
        {
            Destroy(gameObject);
            onResponseFinished.Invoke();
        }
    
    }

}

