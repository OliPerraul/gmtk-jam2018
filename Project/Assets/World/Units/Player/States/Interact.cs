using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NSPlayer
{
    public class Interact : PlayerState
    {
        public override string GetName()
        {
            return "Interact";
        }

        public override void Enter(AState from, GameObject arg = null)
        {
            base.Enter(from, arg);
            DoInteract(arg.GetComponent<Unit>());
        }
        
        public void DoInteract(Unit unit)
        {
            Context.FaceUnit(unit);
            unit.onResponseFinished.AddListener(OnResponseReceived);
            unit.Respond(Context);

        }

        public virtual void OnResponseReceived()
        {
            Context.fsm.SwitchState("Idle");
            
        }

        // Can interact : is correct tool equiped?
    }
}

