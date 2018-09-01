using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSLevel;

namespace NSPlayer
{
    public class Path : MonoBehaviour
    {
        public Stack<Block> stack = new Stack<Block>();
        public float cost = 0f;
        public Block destination;
        public bool interactOnFinished = false;
        public Unit interactionUnit = null;



    }
}
