using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NSLevel
{

    public class Level : MonoBehaviour
    {

        public float heightItemDrop = 100f;
        public float fallSpeed = .01f;

        public BlockSelect select;

        public Vector2Int size = new Vector2Int(256, 256);
        public int cellSize = 2;

        //public BoxCollider collider;
        //public GameObject cellPrototype;
        public GameObject blocksParent;
        public List<Block> blocks;

        private static NSLevel.Level _instance;
        public static NSLevel.Level Instance { get { return _instance; } }

        public void Awake()
        {
            _instance = this;
        }


        // Use this for initialization
        void Start()
        {

            // TODO
            // blocks = GameObjectUtil.CollapseChildrenToList<Block>(blocksParent);
            // TODO


            //cells = new Block[(int)size.x, (int)size.y];
            //RefreshGrid();
        }

        private void Update()
        {
            RandomRain();
        }

        public void DoStart()
        {
            blocks = GameObjectUtil.CollapseChildrenToList<Block>(blocksParent);
        }


        // CHeck good place for drop
        public void RainSeedsBags(int quant)
        {
            int max = quant;
            if (RandomUtils.RandomBoolean())
                max++;
            if (RandomUtils.RandomBoolean() && RandomUtils.RandomBoolean())
                max++;
            //if (RandomUtils.RandomBoolean())
            //    max++;


            int count = 0;
            float startperc = 1f;
            float percDif = .001f;
            while (count != max)
            {

                foreach (Block block in blocks)
                {
                    if (block.walkable && !block.busy && block.unit == null)
                    {
                        if (RandomUtils.PercentChance(startperc))
                        {
                            startperc -= percDif;
                            if (startperc < 0)
                                startperc = 0;

                            continue;
                        }

                        RainSingleSeedsBag(block);
                        count++;

                        if (count == max)
                            break;
                    }

                }
            }

        }

        public void RandomRain()
        {
            if (RandomUtils.PercentChance(.0000001f))
            {
                int x = Random.Range(1, 10);

                switch (x)
                {
                    case 0:
                        RainUnit(NSLevel.Resources.Instance.carrotSeeds);
                        break;

                        
                    case 1:
                        RainUnit(NSLevel.Resources.Instance.pumkinSeeds);
                        break;




                }
            }


        }

        public void RainUnit( GameObject unit)
        {
            float startperc = 1f;
            float percDif = .001f;
            bool dropped = false;
            while (!dropped)
            {

                foreach (Block block in blocks)
                {
                    if (block.walkable && !block.busy && block.unit == null)
                    {
                        if (RandomUtils.PercentChance(startperc))
                        {
                            startperc -= percDif;
                            if (startperc < 0)
                                startperc = 0;

                            continue;
                        }

                        GameObject gameObject = Instantiate(unit);
                        var pushe = gameObject.GetComponent<NSUnit.Pusheable>();

                        pushe.transform.position = block.transform.position + Vector3.up * heightItemDrop;
                        //  seeds
                        pushe.targetPosition = block.transform.position;
                        pushe.stable = false;

                        block.unit = pushe;
                        pushe.block = block;
                        block.walkable = false;
                        block.busy = true;

                        pushe.onResponseFinished.AddListener(block.OnInteractionFinished);

                        dropped = true;

                        return;
                    }

                }
            }

        }

        // CHeck good place for drop
        public void RainUnit(Unit unit)
        {
            float startperc = 1f;
            float percDif = .001f;
            bool dropped = false;
            while (!dropped)
            {

                foreach (Block block in blocks)
                {
                    if (block.walkable && !block.busy && block.unit == null)
                    {
                        if (RandomUtils.PercentChance(startperc))
                        {
                            startperc -= percDif;
                            if (startperc < 0)
                                startperc = 0;

                            continue;
                        }

                        GameObject gameObject = Instantiate(unit.gameObject);
                        var pushe = gameObject.GetComponent<NSUnit.Pusheable>();

                        pushe.transform.position = block.transform.position + Vector3.up * heightItemDrop;
                        //  seeds
                        pushe.targetPosition = block.transform.position;
                        pushe.stable = false;

                        block.unit = pushe;
                        pushe.block = block;
                        block.walkable = false;
                        block.busy = true;

                        pushe.onResponseFinished.AddListener(block.OnInteractionFinished);

                        dropped = true;

                        return;
                    }

                }
            }

        }


        public void RainSingleSeedsBag(Block block)
        {
            GameObject gameObject;

            if (RandomUtils.RandomBoolean())
            {
                gameObject = Instantiate(NSLevel.Resources.Instance.carrotSeeds);
            }
            else
            {
                gameObject = Instantiate(NSLevel.Resources.Instance.pumkinSeeds);
            }

            var seeds = gameObject.GetComponent<NSUnit.Pusheable>();

            seeds.transform.position = block.transform.position + Vector3.up * heightItemDrop;
            //  seeds
            seeds.targetPosition = block.transform.position;
            seeds.stable = false;

            block.unit = seeds;
            seeds.block = block;
            block.walkable = false;
            block.busy = true;

            seeds.onResponseFinished.AddListener(block.OnInteractionFinished);

        }


        //void RefreshGrid()
        //{
        //    Vector3 start = cellPrototype.transform.position;

        //    //update collider pos
        //    collider.size = new Vector3(size.x * cellSize, 1, size.y * cellSize);
        //    //collider.transform.position = start;
        //    Vector3 center = (collider.size / 2);
        //    center.x -= 1; center.z -= 1;
        //    center.y = 0;

        //    collider.center = center;


        //    GameObjectUtil.DestroyImmediateChildren(cellsParent);
        //    for (int i = 0; i < size.y; i++)
        //    {
        //        for (int j = 0; j < size.x; j++)
        //        {
        //            GameObject o = GameObject.Instantiate(cellPrototype.gameObject, cellsParent.transform);
        //            o.transform.position = new Vector3(start.x + j * cellSize, start.y, start.z + i * cellSize);
        //            cells[j, i] = o.GetComponent<Block>();
        //        }

        //    }

        //}

    }
}
