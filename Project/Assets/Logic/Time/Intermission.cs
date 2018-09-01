using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameLogic
{
    class Intermission
    {
        private CountDown CountDown;
        public bool finished = false;

        private float duration = 10;  

        public Intermission(float timeleft)
        {
            GameObject obj = Object.Instantiate(NSGame.Resources.Instance.countDown);
            CountDown = obj.GetComponent<CountDown>();

            duration = timeleft;
        }
    
        public void Start()
        {
            CountDown.startTimer(duration);
        }


        public void Update()
        {
            if (CountDown.stop)
            {
                finished = true;
                CountDown.Kill();
            }
        }


    }
}
