using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour {

    public float destTIm = 5f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, destTIm);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
