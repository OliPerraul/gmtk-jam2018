

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
    public float rent = 100.00f;

    public float newRent = 10f;


        

    //public void Start()
    //{

    //    TimeLineSunMoon.instance.onNewMonth.AddListener(RequestRent);

    //}


    public void RequestRent()
    {
        try
        {
            EconomyUI.instance.GetComponent<AudioSource>().Play();
        }
        catch { System.Exception e; };

        if (money <= rent)
        {
            Game.FSM.SwitchState("StartMenu");

        }
        else
        {
            newRent = money * 1.5f + 100f + newRent/4;
            money -= rent;
            rent = newRent;


            TimeLineSunMoon.instance.speed += Random.Range(.02f, .04f);

        }

    }


    


}
