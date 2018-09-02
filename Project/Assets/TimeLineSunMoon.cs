using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeLineSunMoon : MonoBehaviour {

    static string[] months = new[] { "September", "October", "November", "December", "January", "February", "March", "April", "May", "June", "July", "August" };

    static int month = 0;
    public static string Month { get { return months[month]; } }


    public GameObject sunmoon;
    
    public UnityEvent onNewMonth;

    public GameObject begin;
    public GameObject end;


    RectTransform trans;

    RectTransform beg;
    RectTransform target;

    [SerializeField]
    RectTransform[] targets;
    int i = 0;

    Vector3 initso = new Vector3(1, 1, 1);//* 2;
    Vector3 targetSIze = new Vector3(1, 1, 1) * 2f;

    public float speed = 2f;


    float maxDist = 5f;

    Vector3 startPos;

    public static TimeLineSunMoon instance;

    private void Awake()
    {
        instance = this;
        
    }

    // Use this for initialization
    void Start ()
    {

        onNewMonth = new UnityEvent();
        trans = sunmoon.GetComponent<RectTransform>();
        beg = begin.GetComponent<RectTransform>();
        target = end.GetComponent<RectTransform>();


        startPos = targets[0].position;

    }
	
	        
    // Update is called once per frame
	void Update () {

        trans.position = Vector3.MoveTowards(trans.position, target.position, speed);

        if (i < targets.Length)
        {

            float dist = Vector3.Distance(transform.position, targets[i].position);
            float scale = 1-(dist / maxDist);

            trans.localScale = Vector3.Lerp(initso, targetSIze, scale);
                
            if (trans.position.x >= targets[i].position.x)
            {
                i++;
            }

        }

        if (VectorUtil.SufficientlyClose(trans.position, target.position))
        {

            Economy.Instance.RequestRent();
            trans.position = startPos;
            month++;

        }
        

	}

    public void DoPulse()
    {
        System.Collections.Hashtable hash =
                  new System.Collections.Hashtable();
        hash.Add("amount", new Vector3(0.15f, 0.15f, 0f));
        hash.Add("time", 1f);
        iTween.PunchScale(gameObject, hash);
    }



}
