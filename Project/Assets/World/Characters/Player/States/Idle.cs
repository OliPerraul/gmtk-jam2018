using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NSPlayer
{
    public class Idle : Listening
    {
        public override string GetName()
        {
            return "Idle";
        }

        public override void Tick()
        {
            base.Tick();
        }
    }
}

