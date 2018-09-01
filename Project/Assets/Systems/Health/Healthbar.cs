using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour {

    private const float ZERO = 0;
    private const float MAX = 1;

    [SerializeField]
    private float MAX_OFFSET = 1.0f;
    [SerializeField]
    private float ST_OFF = 4.5f;
  

    public float health = 1;

    
    [SerializeField]
    private GameObject healthObject;


    //st po
    Vector3 startPos;


    // Use this for initialization
    void Start ()
    {
        startPos = healthObject.transform.localPosition;
        startPos.x -= ST_OFF;
        healthObject.transform.localPosition = startPos;

	}
	
	// Update is called once per frame
	void Update ()
    {
      //  Debug.Log(startPos);

        float offset = Mathf.Clamp(Mathf.Lerp(ZERO, MAX_OFFSET, 1-(health / MAX)), ZERO, MAX_OFFSET);
        
        Vector3 pos = startPos;
        pos.x -= offset;

        healthObject.transform.localPosition = pos;
	}
}
