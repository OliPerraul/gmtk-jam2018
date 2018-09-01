
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class DSUtils
{
    public static bool DictionaryGetRandomEntry<TKey, TValue>(Dictionary<TKey, TValue> dict, ref TValue val)
    {
        if (dict.Count == 0)
            return false;


        int idx = Random.Range(0, dict.Count-1);
        int count = 0;

        foreach (var keyvalue in dict)
        {
            if (idx == count)
            {
                val = keyvalue.Value;
                return true;
            }

            count++;
        }

        // Return first
        foreach (var keyvalue in dict)
        {
            val = keyvalue.Value;
            return true;
        }

        return false;

    }



}
