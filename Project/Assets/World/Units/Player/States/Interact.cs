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
            DoInteract(args[0].GetComponent<Unit>());
            arrivalBlock = args[1].GetComponent<Block>();
        }
        
        public void DoInteract(Unit unit)
        {
            Context.FaceUnit(unit);
            //unit.onResponseFinished.AddListener(OnResponseReceived);
            unit.Respond(Context);
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

