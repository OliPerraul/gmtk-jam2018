using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSFSM
{
    public abstract class AStateComponent : MonoBehaviour
    {
        public abstract void Tick();
        public abstract void Enter();
        public abstract void Exit();

        public abstract string GetName();


    }
}