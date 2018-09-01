using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSLevel;

namespace NSPlayer
{
    public class Listening : PlayerState
    {

        public override void Tick()
        {
            base.Tick();

            CheckMouse();
        }


        protected void CheckMouse()
        {
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Block")
                    {
                        NSLevel.Block t = hit.collider.GetComponent<BlockColliderData>().block;

                        if (t.selectable)
                        {
                            Context.fsm.SwitchState("Move", t.gameObject);

                        }
                    }
                }
            }
        }


    }
}
