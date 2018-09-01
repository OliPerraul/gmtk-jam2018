using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NSGame
{

    public class Begin : GameState
    {


        public int mapWidth = 20;
        public int mapHeight = 20;
        public int percentWall = 40;
        public int percentBorderWall = 40;
        public int percentTree = 5;

        public int treeVal = 2;

        public int playerVal = 3;
        public int toolVal0 = 4;
        public int toolVal1 = 5;
        public int toolVal3 = 6;


        public int[,] mapMat;
        //public int[,] obsIDMat;

        NSLevel.MapHandler map;


        public override string GetName()
        {
            return "Begin";
        }

        public override void Enter(AState from, GameObject[] args)
        {
            base.Enter(from);
            SceneManager.LoadScene("LevelGen");

            // GetComponent<NSLevel.Generator>().Generate();
            mapMat = new int[mapWidth, mapHeight];
            map = new NSLevel.MapHandler(mapWidth, mapHeight, mapMat, percentWall);

            map.percentBordersAreWall = percentBorderWall;
            map.percentTree = percentTree;


            map.Prepare();
            //map.SetFillValue(0);
            //map.PercentAreWalls = 10;
            //map.MakeCaverns();

            map.PrintMap();

           
           // obsIDMat = GetComponent<NSLevel.Generator>().GetObsIDMatrix();

            Invoke("DoGenerateLevel", 1f);

        }

        
        public void DoGenerateLevel()
        {
            /// GENERATE THE LEVEL
            GameObject block;

            for (int i = 0; i < mapMat.GetLength(0); i++)
            {
                for (int j = 0; j < mapMat.GetLength(1); j++)
                {

                    Vector3 zero = Vector3.zero;
                    Vector3 position = new Vector3(zero.x + j * NSLevel.Level.Instance.cellSize, zero.y, zero.z + i * NSLevel.Level.Instance.cellSize);

                    switch (mapMat[j, i])
                    {
                        case 0:

                            /// GROUND

                            //tilemapLayout.SetTile(gridpos, GameResources.instance.tileNull);
                            block = Instantiate(NSLevel.Resources.Instance.blockGrass);
                            block.transform.position = position;

                            break;

                            //Handle multiple
                        case 1:
                            block = Instantiate(NSLevel.Resources.Instance.tallGrass);
                            block.transform.position = position;
     
                            break;
                    }
                    //    case 99:

                    //        tilemapLayout.SetTile(gridpos, GameResources.instance.tileSolid);

                    //        go = TileObject.Create(pos, gridpos, GameResources.instance.goat);
                    //        go.transform.SetParent(layCont.transform);


                    //        break;

                    //    case 100:

                    //        // tilemapLayout.SetTile(gridpos, GameResources.instance.tileTarget);
                    //        tilemapInterests.SetTile(gridpos, GameResources.instance.tileTarget);

                    //        go = TileObject.Create(pos, gridpos, GameResources.instance.target);

                    //        go.GetComponent<ttArget>().tile = GameResources.instance.tileTarget;

                    //        gridOfObj[j, i] = go;


                    //        Game.instance.targetCOunt++;

                    //        // go.transform.SetParent(layCont.transform);


                    //        break;


                    //}

                }
            }


            // BEGIN GAME
            // /*/Game.
            Game.FSM.SwitchState("Main");
        }


        public override void Tick()
        {
            base.Tick();
        }

    }
}
