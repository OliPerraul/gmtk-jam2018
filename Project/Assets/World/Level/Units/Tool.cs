using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSLevel;


namespace NSUnit
{
    public class Tool : Pusheable
    {
       public enum TOOL_TYPE
        {
            NONE = 0,
            SHOVEl = 1,
            RACK = 2,
            AXE = 3
        }
        TOOL_TYPE tool_type;


        public override void Respond(Unit unit)
        {
            Player player = unit.GetComponent<Player>();

            // IF player is unit collided, and nothing equipped, then equip
            if (player != null && player.equippedTool == TOOL_TYPE.NONE)
            {
                Equip(player);
            }
            else
            {
                //if (unit)
                base.Respond(unit);

            }
        }


        public void Equip(Player player)
        {
            player.equippedTool = tool_type;
            player.anim.SetInteger("EquippedTool", (int) tool_type);

        }



    }
}
