using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSGame
{
    public class Resources : MonoBehaviour
    {
        [Header("Game")]
        [Header("\n")]

        public GameObject countDown;
        public GameObject dummy;


        private static Resources _instance;
        public static NSGame.Resources Instance {  get{ return _instance; } }


        private void Awake()
        {
            _instance = this;
        }

        public enum TOOL_TYPE
        {
            RACK,
            SHOVEL,
            AXE
        }

        //
        public LayerMask collisionLayerBlock = 11;
        public LayerMask collisionLayerEstablishment = 11;


        [Header("World")]
        [Header("\n")]

        public GameObject harvestable;
        public GameObject pusheable;




    }
}



