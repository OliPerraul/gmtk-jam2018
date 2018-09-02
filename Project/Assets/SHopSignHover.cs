using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSUnit;

public class SHopSignHover : MonoBehaviour
{

    //adjust this to change speed
    [SerializeField]
    float speed = 2f;
    //adjust this to change how high it goes
    [SerializeField]
    float height = .4f;

    [SerializeField]
    float heighAboveStore = 6f;




    void Update()
    {

        //get the objects current position and put it in a variable so we can access it later with less code
        Vector3 pos = transform.localPosition;
        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed) * .75f;
        //set the object's Y to the new calculated Y
        transform.localPosition = new Vector3(pos.x, pos.y + heighAboveStore + newY, pos.z) * height;
    }
}
