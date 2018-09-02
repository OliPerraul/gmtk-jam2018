using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EconomyUI : MonoBehaviour {

    public static EconomyUI instance;


    [SerializeField]
    Text money;
    [SerializeField]
    Text rent;

    string textRent = "";
    string textMoney = "";

    private void Awake()
    {
        instance = this;
    }

    string defaultTextMoney;
      string  defaultTextRent ;

    private void Start()
    {
        defaultTextRent = textRent = rent.text;
        defaultTextMoney =textMoney = money.text;
    }

    // Update is called once per frame
    void Update () {
        money.text = textMoney.Replace("{0}", Economy.Instance.money.ToString("0.00"));
        rent.text = textRent.Replace("{0}", Economy.Instance.rent.ToString("0.00"));

    }


    public void SetMultiplier(int multiplier)
    {

        switch (multiplier)
        {
            case 0:
            textMoney = defaultTextMoney;
                money.color = Color.black;
            break;

            case 1:
                textMoney = defaultTextMoney;
                money.color = Color.black;
                        
                break;

            case 2:
                textMoney = textMoney + " X2";
                money.color = Color.white;

                break;
            case 4:
                textMoney = textMoney + " X4";
                money.color = Color.yellow;
                break;

            case 8:
                textMoney = textMoney + " X8";
                money.color = Color.red;
                break;
            //break;

            case 16:
                textMoney = textMoney + " X16";
                money.color = Color.magenta;
                break;
            //break;

            case 32:
                textMoney = textMoney + " X32";
                money.color = Color.blue;
                break;
            //break;

            case 64:
                textMoney = textMoney + " X64";
                money.color = Color.green;
                break;

            case 128:
                textMoney = textMoney + " X256";
                money.color = Color.cyan;
                break;
            //break;

            case 256:
                textMoney = textMoney + " X256";
                money.color = Color.grey;
                break;

        }


    }



}
