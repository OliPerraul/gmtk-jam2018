using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;

using UnityEngine;



class Economy : MonoBehaviour
{
    private static Economy _instance;
    public static Economy Instance { get { return _instance; } }

    public void Awake()
    {
        _instance = this;
    }


    //public static Economy instance;

    float interval_week = 60;
    float interval_moonth = 4*60;
    float interval_year = 52*60;

    private CountDown countd_W;
    private CountDown countd_m;
    private CountDown countd_y;


    public float money = 0;
    public float rent = 10000.00f;


    public Economy()
    {


        //   //main CountDown
        //GameObject obj = UnityEngine.Object.Instantiate(GameResources.instance.CountDown);
        //countd_W = obj.GetComponent<CountDown>();

        ////main CountDown
        //GameObject obj2 = UnityEngine.Object.Instantiate(GameResources.instance.CountDown);
        //countd_W = obj2.GetComponent<CountDown>();

        ////main CountDown
        //GameObject obj3 = UnityEngine.Object.Instantiate(GameResources.instance.CountDown);
        //countd_W = obj3.GetComponent<CountDown>();

        //countd_m.startTimer(interval_moonth);
        //countd_m.loop = true;
        //countd_y.startTimer(interval_year);
        //countd_y.loop = true;
        //countd_W.startTimer(interval_week);
        //countd_W.loop = true;


       

    }


    


}
