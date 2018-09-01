using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSLevel;

namespace NSPlayer
{
    public class Interact : PlayerState
    {

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
            unit.onResponseFinished.AddListener(OnResponseReceived);
            unit.Respond(Context);

        }

        public virtual void OnResponseReceived()
        {
            // DO not move character
            Context.fsm.SwitchState("Idle", Context.block.gameObject);
            
        }

        // Can interact : is correct tool equiped?
    }
}

