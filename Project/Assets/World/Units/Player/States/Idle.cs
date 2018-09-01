using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSLevel;

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

        public override void Enter(AState from, params GameObject[] args)
        {
            base.Enter(from, args);


            // On start
            if (args.Length == 0)
                return;

            Context.block.unit = null;
            Context.block = args[0].GetComponent<Block>();
            Context.block.unit = Context;
        }

        public override bool TryInteract(Block t)
        {
            base.TryInteract(t);

            // TODO : Always know current block instead
            List<Block> adjacent = Context.block.GetAdjacentBlocks();
            foreach (Block block in adjacent)
            {
                //TODO /REFACTOR
                // Do not move, just interact
                if (block == t)
                {
                    DoInteract(t.unit, 2f);
                    return true;
                }
            }

            return false;
        }

        public void DoInteract(Unit unit, float delayTime)
        {
            //yield return new WaitForSeconds(delayTime);
            Context.fsm.SwitchState("Interact", unit.gameObject, unit.block.gameObject);
        }

        //IEnumerator DoInteract(Unit unit, float delayTime)
        //{
        //    yield return new WaitForSeconds(delayTime);
        //    Context.fsm.SwitchState("Interact", unit.gameObject, unit.block.gameObject);
        //}

    }
}

