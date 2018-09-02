using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class VectorUtil
{

    private const float EPSILON = 0.1f;
    private const float EPSILON2 = 0.8f;

    public static bool SufficientlyClose(Vector3 pos0, Vector3 pos1)
    {
        return (Vector3.Distance(pos0, pos1) <= EPSILON);

    }


    public static bool SufficientlyClose2(Vector3 pos0, Vector3 pos1)
    {
        return (Vector3.Distance(pos0, pos1) <= EPSILON2);

    }


}

