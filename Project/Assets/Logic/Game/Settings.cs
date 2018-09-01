using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NSGame
{

    public class Settings : MonoBehaviour
    {
        [Header("Game Settings")]

        public int MONEY_CAP = 1000;

        public float TIME_BEFORE_START = 5f;
        public float INTERMISSION_TIME = 5f;
        public float MIN_WAVE_TIME = 60f;
        public float MAX_WAVE_TIME = 120f;

        public float ENEMIES_INTERV_TIME_MIN = 5f;
        public float ENEMIES_INTERV_TIME_MAX = 10f;

        public int MIN_ENMIES = 5;
        public int MAX_ENMIES = 10;
        public int DIFF_COEFF = 5;

        public float TIME_NEW_ITEM_MIN = 10f;
        public float TIME_NEW_ITEM_MAX = 20f;


        private static Settings _instance;
        public static NSGame.Settings Instance { get { return _instance; } }


        private void Awake()
        {
            _instance = this;
        }

    }
}
