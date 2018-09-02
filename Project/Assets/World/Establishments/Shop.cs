using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSUnit;

namespace NSEstablishment
{

    // SEED GOODS
    public class Shop : Establishment
    {



        // Hit the store, take money to spawn seeds
        // Hit the store, take money to spawn seeds
        // TODO the more you hit
        // THE BIGGER THE COMBO THE MORE DROP YOU GET 
        public override void Respond(Pusheable pusheable)
        {
            base.Respond(pusheable);

            RainSeedsBags(3);

            //Economy.Instance.money += pusheable.value;

        }

        // Hit the store, take money to spawn seeds
        // TODO the more you hit
            // THE MORE YOU GET THE MORE EXPANSIVE
        public override void Respond(Player player)
        {
            base.Respond(player);

            //TODO Quant modif
            // Suff funds?
            RainSeedsBags(3);

            //Economy.Instance.money += pusheable.value;

        }


        public void RainSeedsBags(int quant)
        {
            NSLevel.Level.Instance.RainSeedsBags(quant);
        }
        

    }


}