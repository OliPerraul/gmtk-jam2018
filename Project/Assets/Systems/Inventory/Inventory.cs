//using System;
using System.Collections;
using System.Collections.Generic;
using NSFSM;
using UnityEngine;
using UnityEngine.Events;


namespace NSInventory
{

    public class Inventory : NSFSM.AStateComponent
    {
        [HideInInspector]
        public UnityEvent onInventoryUpdated;

        public delegate void OnItemChanged();
        public OnItemChanged onItemChangedCallback;
        private CountDown countDownNewItem;

        public Item[] items = new Item[15];


        private static Inventory _instance;
        public static Inventory Instance { get { return _instance; } }

        public void Awake()
        {
            _instance = this;   
        }


        public override void Enter()
        {
            //Timer
            countDownNewItem = CountDown.Create();

            WaitNewItem();
        }

        public override void Exit()
        {
            Clear();
            countDownNewItem.Kill();
        }


        public override void Tick()
        {
            if (countDownNewItem.stop)
            {
                AddItemRandom();
                WaitNewItem();
            }
        }

        public void AddItemRandom()
        {
            AddItemSlotRandom(ItemFactory.InstantiateRandom());
        }

        public void AddItemSlotRandom(Item item)
        {

            int slot = Random.Range(0, items.Length - 1);

            if (items[slot] == null)
            {
                if (AddItem(item, slot))
                    return;
            }

            for (int i = 0; i < items.Length - 1; i++)
            {
                if (AddItem(item, i))
                    return;
            }
        }

        public void Clear()
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = null;
            }

            OnInventoryUpdated();
        }

        public bool AddItem(Item item, int idx)
        {
            if (items[idx] != null)
                return false;

            items[idx] = item;
            OnInventoryUpdated();

            return true;
        }

        public bool RemoveItem(int idx)
        {
            if (items[idx] != null)
            {
                items[idx] = null;
                return true;
            }

            return false;
        }


        public void OnInventoryUpdated()
        {
            onInventoryUpdated.Invoke();
        }


        public void WaitNewItem()
        {
            float wait = Random.Range(NSGame.Settings.Instance.TIME_NEW_ITEM_MIN, NSGame.Settings.Instance.TIME_NEW_ITEM_MAX);
            countDownNewItem.startTimer(wait);
        }

        public override string GetName()
        {
            return "Inventory";
        }
    }

}
