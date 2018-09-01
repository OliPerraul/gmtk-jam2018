using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;


namespace NSInventory
{

    public class GridItem : Button, IPointerClickHandler
    {

        public int index = 0;

        public Item item = null;
        public UnityEvent unityEvent;

        //Used as ref when ovj is drag
        private HoverItem hoverItem;

        //private Image image = null;

        Inventory inventory;

        GridItemData data;

        // Use this for initialization
        public void Start()
        {
            inventory = (Inventory)Game.FSM.Top.GetStateComponent("Inventory");
            data = GetComponent<GridItemData>();

        }

        public void Update()
        {
            Refresh();
        }


        public void Refresh()
        {
            if (item != null)
            {
                if (item.useModel)
                {
                    //TODO use more mesh, mat
                    data.meshFilter.mesh = item.model.mesh[0];
                    data.meshRenderer.material = item.model.material[0];

                    return;
                }

                data.image.sprite = item.icon;
            }
            else
            {
                data.image.sprite = null;
                data.meshFilter.mesh = null;
                data.meshRenderer.material = null;
            }
        }


        public override void OnPointerDown(PointerEventData eventData)
        {
            if (item != null)
            {
                GameObject gobj = Instantiate(NSInventory.Resources.Instance.hoverItem);

                gobj.transform.position = transform.position;

                hoverItem = gobj.GetComponent<HoverItem>(); ;
                hoverItem.item = item;

                unityEvent.AddListener(hoverItem.OnMouseReleased);

                item = null;
                inventory.RemoveItem(index);
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (hoverItem != null)
            {
                unityEvent.Invoke();

                if (!hoverItem.successful)
                {
                    inventory.AddItem(hoverItem.item, index);
                }

                Destroy(hoverItem.gameObject);
                hoverItem = null;
            }
        }

    }
}
