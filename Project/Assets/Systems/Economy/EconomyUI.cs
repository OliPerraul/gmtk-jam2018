using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EconomyUI : MonoBehaviour {

    [SerializeField]
    Text money;
    [SerializeField]
    Text rent;

    string textRent = "";
    string textMoney = "";

    private void Start()
    {
        textRent = rent.text;
        textMoney = money.text;
    }


    // Update is called once per frame
    void Update () {
        money.text = textMoney.Replace("{0}", Economy.Instance.money.ToString());
        rent.text = textRent.Replace("{0}", Economy.Instance.rent.ToString());
    }
}
