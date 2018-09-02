using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSLevel;
using UnityEngine.Events;

namespace NSUnit
{
    public class Pusheable : Unit
    {

        /// <summary>
        ///  SOME PUSHEABLE ACTUALLY BECOME HARVESTABLE WHEN EQUIPPED WITH AN ITEM
        /// </summary>


        public enum PUSHEABLE_TYPE
        {
            CARROT,
            PUMKIN,
            HAYBUNDLE,
            LOGS,

            PUMKIN_SEEDS,
            CARROT_SEEDS,

            DEAD_CARROT,
            DEAD_PUMKIN,

            NOONE,

            TOOL,

        }
        public PUSHEABLE_TYPE pusheableType;

        [SerializeField]
        GameObject product;

        [SerializeField]
        GameObject basVersion;
        

        [SerializeField]
        public float moveSpeed = 0.8f;
        public bool stable = true;
        private bool falling = false;
        private bool destroy = false;
        private bool sold = false;


        public float value = 10.00f;


        [SerializeField]
        private float fallspeed = 10;
        [SerializeField]
        private float fadeSpeed = .08f;
        [SerializeField]
        private float shrinkSpeed = .02f;

        [SerializeField]
        private float goToStoreY = 2;

        private Vector3 offsetyV;

        public Vector3 targetPosition;

        Color invis;

        

        
        private void Start()
        {
            type = TYPE.PUSHEABLE;
            //invis = model.GetComponent<MeshRenderer>().material.color;
            invis.a = 0;
            offsetyV = new Vector3(0, goToStoreY, 0);

            moveSpeed = 0.8f;

            if (pusheableType == PUSHEABLE_TYPE.CARROT || pusheableType == PUSHEABLE_TYPE.PUMKIN)
            {
                Invoke("ExpireCrop", Random.Range(10f, 20f));

            }

        }


        public void ExpireCrop()
        {
            Pusheable pusheable = Instantiate(basVersion).GetComponent<Pusheable>();

            pusheable.transform.position = block.transform.position;

            //  pusheable.block
            pusheable.block = block;
            block.unit = pusheable;
            pusheable.block.fertile = false;
            block.walkable = false;

            Destroy(gameObject);

        }
                   

        private void Update()
        {
           // TODO FSM??
            if (!stable)
            {
                //transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed);
                if (sold)
                {
                    transform.position = Vector3.Lerp(transform.position, targetPosition+offsetyV, moveSpeed);

                    transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, shrinkSpeed);
                    // TODO iterate through models
                 //   model.GetComponent<MeshRenderer>().material.color = Color.Lerp(model.GetComponent<MeshRenderer>().material.color, invis, fadeSpeed);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed); // Vector3.Lerp(transform.position, targetPosition, moveSpeed);
                }


                if (VectorUtil.SufficientlyClose(transform.position, targetPosition))
                {
                   
                    if (falling)
                    {
                        targetPosition.y = transform.position.y -fallspeed;
                        falling = false;

                        
                        if(type == TYPE.TOOL)
                        Level.Instance.RainUnit(this);

                        return;
                    }

                    if (destroy)
                    {
                        onResponseFinished.Invoke();
                        Destroy(gameObject);

                        if (type == TYPE.TOOL)
                            Level.Instance.RainUnit(this);

                        return;
                    }


                    transform.position = targetPosition;
                    stable = true;
                    onResponseFinished.Invoke();
                                        
                    
                }


            }
        }


        public override void Respond(Unit unit)
        {
            base.Respond(unit);

            ///IF SEED READY TIO BE PLANTED, PLANT
            if ((pusheableType == PUSHEABLE_TYPE.CARROT_SEEDS || pusheableType == PUSHEABLE_TYPE.PUMKIN_SEEDS) &&
                unit.GetComponent<Player>() != null && unit.GetComponent<Player>().equippedTool == Tool.TOOL_TYPE.SHOVEl)
            {
                Pusheable pusheable = Instantiate(product).GetComponent<Pusheable>();

                pusheable.transform.position = block.transform.position;

              //  pusheable.block
                pusheable.block = block;
                block.unit = pusheable;
                pusheable.block.fertile = false;
                block.walkable = false;

                Destroy(gameObject);

            }
            else
            if (CanPush(unit.direction))
            {
                direction = unit.direction;
                Push(direction);
            }
        }

        public void Respond(Unit unit, out bool success)
        {
            base.Respond(unit);
            // TODO prompt player wait.
            // NEVER REACH
            success = false;

            if (CanPush(unit.direction)) ///LOL oh well, i guess i shoulve used that
            {
                direction = unit.direction;
                success = Push(direction);
            }

        }


        public bool Push(Vector3 direction)
        {
            Block neighbour;
            NSEstablishment.Establishment establishment;

            if (block.GetNeighbour(direction, out neighbour, out establishment))
            {
                
                if (neighbour == null)
                {
                    DoPush();

                    // Free busy block on interaction finished
                    onResponseFinished.AddListener(block.OnInteractionFinished);

                    establishment.Respond(this);
                    targetPosition = transform.position + direction * 2;
                    sold = true;
                    destroy = true;
                    Invoke("FinishInteractionIfSOld", 2f);

                    return true;

                }
                else // We got a cube
                {
                    // Pushed against othe rpushable
                    if (neighbour.unit != null && (neighbour.unit.type == Unit.TYPE.PUSHEABLE || neighbour.unit.type == Unit.TYPE.TOOL))
                    {
                        // TODO Stack same pusheable item
                        // If cant push next, then cant push
                        bool success;
                        ((Pusheable)neighbour.unit).Respond(this, out success);
                        if (!success)
                        {
                            block.busy = false;
                            return false;
                        }

                        DoPush();

                    }
                    else if (neighbour.unit != null && (neighbour.unit.type == Unit.TYPE.HARVESTABLE))
                    {
                        // RETURN CANT MOVE
                        block.busy = false;
                        return false;
                    }

                    DoPush();

                    // Free busy block on interraction finished
                    onResponseFinished.AddListener(neighbour.OnInteractionFinished);

                    block = neighbour;
                    block.busy = true;
                    block.unit = this;
                    block.walkable = false;


                    targetPosition = block.transform.position;
                    return true;
                }

            }
            else
            {
                DoPush();

               Fall(direction);
                return true;
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


        public void FinishInteractionIfSOld()
        {
            onResponseFinished.Invoke();
        }


        public void DoPush()
        {
            stable = false;
            block.walkable = true;
            block.unit = null;
        }


    }
}
