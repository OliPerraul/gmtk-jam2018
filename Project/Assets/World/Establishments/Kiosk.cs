using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSUnit;

namespace NSEstablishment
{
    // FARM GOODS
    public class Kiosk : Establishment
    {
        public override void Respond(Pusheable pusheable)
        {
            base.Respond(pusheable);

            Economy.Instance.money += pusheable.value;

        }


        
    }

   
}