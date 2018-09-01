using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class GameObjectUtil
{
    public static void DestroyChildren(GameObject o)
    {
        foreach (Transform child in o.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public static void DestroyImmediateChildren(GameObject o)
    {
        foreach (Transform child in o.transform)
        {
            GameObject.DestroyImmediate(child.gameObject);
        }
    }

    private static void _RecurseCollapseChildrenToList<TComponent>(GameObject parent, ref List<TComponent> collapsedChildren)
    {
        foreach (Transform child in parent.transform)
        {
            var component = child.gameObject.GetComponent<TComponent>();
            if (component != null)
            {
                collapsedChildren.Add(component);
            }

            _RecurseCollapseChildrenToList(child.gameObject, ref collapsedChildren);
        }
    }

    public static List<TComponent> CollapseChildrenToList<TComponent>(GameObject parent)
    {
        List<TComponent> collapsedChildren = new List<TComponent>();

        foreach (Transform child in parent.transform)
        {
            _RecurseCollapseChildrenToList(child.gameObject, ref collapsedChildren);
        }

        return collapsedChildren;
    }


}

