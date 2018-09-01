using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemFactory : ScriptableObject
{

    private static Dictionary<string, Item> items;
    private static Dictionary<int, string> idxNameMap;


    private static Item prototypeItem;

    public static void Initialize(Item[] baseItems)
    {
        prototypeItem = baseItems[0];
        items = new Dictionary<string, Item>();
        idxNameMap = new Dictionary<int, string>();

        // Init inventory items
        foreach (Item item in baseItems)
        {
            items[item.name] = item;
        }

    }

    public static Item Instantiate(string name)
    {
        return Instantiate(items[name]);
    }

    public static Item InstantiateRandom()
    {
        Item item = null;
        DSUtils.DictionaryGetRandomEntry<string, Item>(items, ref item);
        return item;
    }



    // Get Sprite from base item
    public static void Create(string name, Sprite icon, int count, int cost)
    {
        Item item = Instantiate(prototypeItem);
        item.name = name;
        item.icon = icon;
        item.count = count;
        item.cost = cost;

        items[name] = item;
    }


}
