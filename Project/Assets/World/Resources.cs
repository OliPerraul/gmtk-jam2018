using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSLevel
{
    public class Resources : MonoBehaviour
    {
        [Header("Level")]
        [Header("\n")]

        private static Resources _instance;
        public static Resources Instance { get { return _instance; } }

        private void Awake()
        {
            _instance = this;
            //ItemFactory.Initialize(baseItems);
        }

        [Header("Player")]
        public GameObject player;
        public GameObject toolRack;
        public GameObject toolShovel;
        public GameObject toolAxe;

        [Header("Establishments")]
        public GameObject store;
        public GameObject kiosk;

        [Header("Misc")]
        public GameObject tallGrass;
        public GameObject tree;
        public GameObject blockGrass;
        public GameObject blockDirtFull;

        [Header("Seeds (Falling)")]
        public GameObject carrotSeeds;
        public GameObject pumkinSeeds;
        public GameObject Sheep;
        // SHEEP?
    }



}



