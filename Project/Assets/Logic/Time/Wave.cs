using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameLogic
{
    class Wave
    {
        private CountDown CountDown;
        private CountDown CountDownNextEnemy;


        public bool finished = false;

        public int enemyCount = 5;

        private List<Vector3> entries;

        private float duration = 10f;

        public Wave(int difficulty, float timeleft)
        {

            duration = timeleft;

            //main CountDown
            GameObject obj = Object.Instantiate(NSGame.Resources.Instance.countDown);
            CountDown = obj.GetComponent<CountDown>();

            //CountDown next enemy
            GameObject obj2 = Object.Instantiate(NSGame.Resources.Instance.countDown);
            CountDownNextEnemy = obj2.GetComponent<CountDown>();
           
            //Get entries
            //entries = BlockmapUtils.GetEntries(World.instance.blockmapLayout);

            //Set the monster count
           // SetMonsterCount();

        }

        public void Start()
        {
            CountDown.startTimer(duration);
            WaitNextEnemy();
        }


        public void Update()
        {
            //if ((CountDownNextEnemy.stop) && (enemyCount > 0))
            //{
            //    SpawnNextMonster();
            //    WaitNextEnemy();
            //}

            if (CountDown.stop)
            {
                finished = true;
                CountDownNextEnemy.Kill();
                CountDown.Kill();
            }
        }

   
       // public void SetMonsterCount()
       // {
       //     enemyCount = Random.Range(GameSettings.instance.MIN_ENMIES+Game.instance.difficulty*GameSettings.instance.DIFF_COEFF,
       //                 GameSettings.instance.MAX_ENMIES+Game.instance.difficulty*GameSettings.instance.DIFF_COEFF);

       //}

        public void WaitNextEnemy()
        {
            //float interv = Random.Range(GameSettings.instance.ENEMIES_INTERV_TIME_MIN, GameSettings.instance.ENEMIES_INTERV_TIME_MAX);
            //CountDownNextEnemy.startTimer(interv);
        }

        //public void SpawnNextMonster()
        //{
        //    enemyCount--;

        //    int entryIdx = Random.Range(0, entries.Count - 1);

        //    Vector3 entry = entries[entryIdx];

        //    DoughMonster monster = GameResources.Create(entry, GameResources.instance.doughMonstwe).GetComponent<DoughMonster>();

        //    monster.gameObject.transform.SetParent(World.instance.monstConta.transform);

        //    monster.SetDifficulty(Game.instance.difficulty);
        //}
        

    }
}
