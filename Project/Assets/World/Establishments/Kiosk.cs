using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSUnit;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSLevel;
using NSPlayer;


namespace NSEstablishment
{
    // FARM GOODS
    public class Kiosk : Establishment
    {
        AudioSource dd;

        private void Start()
        {
           dd = GetComponent<AudioSource>();
        }



        public int multiplier = 1;
        Pusheable.PUSHEABLE_TYPE previousOne = Pusheable.PUSHEABLE_TYPE.NOONE;


        public override void Respond(Pusheable pusheable)
        {
            base.Respond(pusheable);

            if (previousOne == pusheable.pusheableType)
            {
                dd.Play();

                multiplier++;

                Economy.Instance.money += pusheable.value * multiplier;
                //var go = Instantiate(floatingTextMultiplier);
                //go.transform.position = transform.position;
                //go.transform.position += Vector3.forward * multiplier_dist;
                EconomyUI.instance.SetMultiplier(multiplier);

            }
            else
            {
                dd.Play();

                previousOne = pusheable.pusheableType;

                multiplier = 1;
                Economy.Instance.money += pusheable.value;
                               
                EconomyUI.instance.SetMultiplier(1);

            }   
        }

        


        
    }

   
}