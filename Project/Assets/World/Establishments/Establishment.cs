using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NSEstablishment
{
    public abstract class Establishment : MonoBehaviour
    {
        public GameObject model;

        public GameObject floatingTextMoney;
        public GameObject floatingTextMultiplier;



        public virtual void Respond(NSUnit.Pusheable pusheable)
        {
            DoPulse();

            if (pusheable.type == Unit.TYPE.TOOL)
                NSLevel.Level.Instance.RainUnit(pusheable.gameObject);

        }

        public virtual void Respond(Player pusheable)
        {
            DoPulse();
        }


        public void DoPulse()
        {
            System.Collections.Hashtable hash =
                      new System.Collections.Hashtable();
            hash.Add("amount", new Vector3(0.15f, 0.15f, 0f));
            hash.Add("time", 1f);
            iTween.PunchScale(model, hash);
        }
    }
}
