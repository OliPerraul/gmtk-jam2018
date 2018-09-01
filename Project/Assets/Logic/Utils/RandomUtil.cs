using System.Collections;
using System.Collections.Generic;
using UnityEngine;



class RandomUtils
{
    public static Random r;

    public static bool RandomBoolean()
    {
        if (Random.value >= 0.5)
        {
            return true;
        }
        return false;
    }

    public static bool PercentChance(float percentChance)
    {
        float chance =  Random.Range(0, 1);
        return (chance <= percentChance);
    }


}


