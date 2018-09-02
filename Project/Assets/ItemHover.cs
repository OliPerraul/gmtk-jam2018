using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSUnit;

public class ItemHover : MonoBehaviour {

    //adjust this to change speed
    float speed = 1f;
    //adjust this to change how high it goes
    float height = .2f;

    


    void Update()
    {

        Tool t = GetComponent<Tool>();

        //get the objects current position and put it in a variable so we can access it later with less code
        Vector3 pos = transform.localPosition;
        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed)*.75f;
        //set the object's Y to the new calculated Y
        transform.localPosition = new Vector3(pos.x, pos.y+2+newY, pos.z) * height;
    }
}
