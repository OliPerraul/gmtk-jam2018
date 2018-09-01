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

        public override void TryInteract(Block t)
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
                    Context.fsm.SwitchState("Interact", t.unit.gameObject, t.gameObject);
                    return;
                }
            }
        }
    }
}

