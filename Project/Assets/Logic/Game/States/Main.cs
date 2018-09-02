using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NSGame
{
    public class Main : GameState
    {
        public override string GetName()
        {
            return "Main";
        }
        
        public override void Tick()
        {
            base.Tick();

            if (Input.GetKeyDown(KeyCode.R))
            {
                // BEGIN GAME
                // /*/Game.
                Game.FSM.SwitchState("Begin");
            }
        }

       
    }
}

