using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSInventory
{
    public class Resources : MonoBehaviour
    {
        [Header("Inventory")]
        [Header("\n")]

        public GameObject gridItem;
        public GameObject hoverItem;

        public Item[] baseItems;
        public Dictionary<string, Item> items;



        private static Resources _instance;
        public static Resources Instance { get { return _instance; } }


        private void Awake()
        {
            _instance = this;
            ItemFactory.Initialize(baseItems);

        }
    }



}



