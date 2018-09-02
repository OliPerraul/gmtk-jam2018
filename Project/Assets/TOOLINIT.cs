using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TOOLINIT : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GetComponent<Unit>().type = Unit.TYPE.TOOL;

	}
	
	// Update is called once per frame

}
