using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace NSResources
{
    [System.Serializable]
    public class Transform
    {
        public Vector3 position = new Vector3(0,0,0);
        public Quaternion rotation = new Quaternion();
        public Vector3 scale = new Vector3(1, 1, 1);

    }
}

