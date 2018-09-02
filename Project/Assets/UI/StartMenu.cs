using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public Sprite[] screens;
    public Image current;
    int count = 0;

    public void Update()
    {
        if (Input.anyKeyDown)
        {
            count++;
            if (count < screens.Length)
            {
                current.sprite = screens[count];
            }
            else
            {
                Game.FSM.SwitchState("Begin");

            }
        }
    }
}
