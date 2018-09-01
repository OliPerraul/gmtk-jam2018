using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public Button startButton;

    public void Start()
    {
        startButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        Game.FSM.SwitchState("Begin");
    }


}
