using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSLevel;

namespace NSPlayer
{
    public class Interact : PlayerState
    {
        [SerializeField]
        private float lagTime = 0.2f;
        private Block arrivalBlock = null;

        public override string GetName()
        {
            return "Interact";
        }

        public override void Enter(AState from, params GameObject[] args)
        {
            base.Enter(from, args);
            Unit unit = args[0].GetComponent<Unit>();

            if (unit == null)
            {// TRY interacting establishment
               var estab =  args[0].GetComponent<NSEstablishment.Establishment>();
                DoInteract(estab);
            }
            else
            {
                DoInteract(unit);
            }

            arrivalBlock = args[1].GetComponent<Block>();
            Context.fsm.SwitchState("Idle", Context.block.gameObject);
        }
        
        public void DoInteract(Unit unit)
        {
            Context.FaceUnit(unit);
            //unit.onResponseFinished.AddListener(OnResponseReceived);
            unit.Respond(Context);
            Invoke("Finish", lagTime);

        }

        
        public void DoInteract(NSEstablishment.Establishment esta)
        {
            Context.FaceEstablishment(esta);

            //unit.onResponseFinished.AddListener(OnResponseReceived);
            esta.Respond(Context);
            Invoke("Finish", lagTime);

        }


        public virtual void Finish()
        {
            // DO not move character
            Context.fsm.SwitchState("Idle", Context.block.gameObject);
            
        }

        // Can interact : is correct tool equiped?
    }
}

