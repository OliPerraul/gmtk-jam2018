using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSUnit;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NSEstablishment
{

    // SEED GOODS
    public class Shop : Establishment
    {
        public static Shop shop;

        private void Awake()
        {
            shop = this;
        }

        public int price = 5;


        // Hit the store, take money to spawn seeds
        // Hit the store, take money to spawn seeds
        // TODO the more you hit
        // THE BIGGER THE COMBO THE MORE DROP YOU GET 
        public override void Respond(Pusheable pusheable)
        {
            base.Respond(pusheable);

            RainSeedsBags(1);

            //Economy.Instance.money += pusheable.value;

        }

        // Hit the store, take money to spawn seeds
        // TODO the more you hit
            // THE MORE YOU GET THE MORE EXPANSIVE
        public override void Respond(Player player)
        {
            base.Respond(player);

            if (Economy.Instance.money <= price)
                return;

            Economy.Instance.money -= price;

AudioSource dd=           GetComponent<AudioSource>();

            
            dd.Play();

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