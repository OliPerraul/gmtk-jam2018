using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace NSGame
{
    public class StartMenu : GameState
    {
        public override string GetName()
        {
            return "StartMenu";
        }

        
        public override void Tick()
        {
            base.Tick();// do something
        }


        public override void Enter(AState from, params GameObject[] args)
        {
            base.Enter(from, args);
            SceneManager.LoadScene("StartMenu");

        }


    }
}

