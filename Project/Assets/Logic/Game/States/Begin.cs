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

        NSLevel.Block[,] blocks;


        public override string GetName()
        {
            return "Begin";
        }

        public override void Enter(AState from, GameObject[] args)
        {
            base.Enter(from);
            SceneManager.LoadScene("LevelGen");

            blocks = new NSLevel.Block[mapWidth, mapHeight];


            // GetComponent<NSLevel.Generator>().Generate();
            mapMat = new int[mapWidth, mapHeight];
            map = new NSLevel.MapHandler(mapWidth, mapHeight, ref mapMat, percentWall);

            map.percentBordersAreWall = percentBorderWall;
            map.percentTree = percentTree;
            map.Prepare();
            map.RemoveInBetween();

            map.PrintMap();

           
           // obsIDMat = GetComponent<NSLevel.Generator>().GetObsIDMatrix();

            Invoke("DoGenerateLevel", 1f);

        }

        
        public void DoGenerateLevel()
        {
            /// GENERATE THE LEVEL
            GameObject block;

            for (int i = 0; i < map.Map.GetLength(0); i++)
            {
                for (int j = 0; j < map.Map.GetLength(1); j++)
                {


                    // PLACE KIOSK and STORE

                    // TOP


                    Vector3 zero = Vector3.zero;
                    Vector3 position = new Vector3(zero.x + j * NSLevel.Level.Instance.cellSize, zero.y, zero.z + i * NSLevel.Level.Instance.cellSize);

                    block = Instantiate(NSLevel.Resources.Instance.blockGrass);
                    block.transform.position = position;
                    block.transform.SetParent(NSLevel.Level.Instance.transform);


                    /*
                     
                        public int playerVal = 3;
                        public int toolVal0 = 4;
                        public int toolVal1 = 5;
                        public int toolVal3 = 6;

                     
                     */
                    GameObject unit = null;


                    switch (map.Map[j, i])
                    {
                        case 0:







                            /// GROUND

                            //tilemapLayout.SetTile(gridpos, GameResources.instance.tileNull);
                            //NOTHING
                            break;

                            //Handle multiple
                        case 1:
                            unit = Instantiate(NSLevel.Resources.Instance.tallGrass);
                            unit.transform.position = block.transform.position;
                            unit.transform.SetParent(NSLevel.Level.Instance.transform);

                            break;

                        //Handle multiple
                        case 2:
                            unit = Instantiate(NSLevel.Resources.Instance.tree);
                            unit.transform.position = block.transform.position;
                            unit.transform.SetParent(NSLevel.Level.Instance.transform);

                            break;

                        case 3:
                            unit = Instantiate(NSLevel.Resources.Instance.player);
                            unit.transform.position = block.transform.position;
                            unit.transform.SetParent(NSLevel.Level.Instance.transform);

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

                    NSLevel.Block b = block.GetComponent<NSLevel.Block>();
                    NSLevel.Level.Instance.blocks.Add(b);
                    blocks[j, i] = b;


                    if (unit != null)
                    {
                        Unit u = unit.GetComponent<Unit>();
                        b.SetUnit(u);
                    }
                    


                }
            }


            // PlaceStoreAndKiosk();


            PlaceKiosk();
           // PlaceStore();

           // NSLevel.Level.Instance.DoStart();

            // BEGIN GAME
            // /*/Game.
            Game.FSM.SwitchState("Main");

        }


        public void PlaceKiosk()
        {
            GameObject obj;
            int i;
            int j;

            /// DOWN
            /// 

            i = 0;
            for (j = 0; j < map.Map.GetLength(0); j++)
            {

                bool found = true;
                for (int xx = 0; xx < 2; xx++)
                {
                    try
                    {
                        if (map.Map[j + xx, 0] != 0)
                        {
                            found = false;
                            break;
                        }
                    }
                    catch (System.Exception e)
                    {
                        found = false;
                        break;
                    }
                }
                // Install Kiosk up
                if (found)
                {
                    var v = -Vector3.forward;
                    Vector3 position = blocks[j, i].transform.position + (v * 2f);

                    //Make origin at the corner
                    obj = Instantiate(NSLevel.Resources.Instance.kiosk);
                    obj.transform.position = position;
                    obj.transform.SetParent(NSLevel.Level.Instance.transform);

                    //obj.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);



                    for (int xx = 0; xx < 4; xx++)
                    {
                        map.Map[j+xx, 0] = -1;
                    }

                    return;
                }

            }



            /// LEFT
            for (i = 0; i < map.Map.GetLength(1); i++)
            {

                bool found = true;
                for (int xx = 0; xx < 2; xx++)
                {
                    try
                    {
                        if (map.Map[0, i + xx] != 0)
                        {
                            found = false;
                            break;
                        }
                    }
                    catch (System.Exception e)
                    {
                        found = false;
                        break;
                    }
                }
                // Install Kiosk up
                if (found)
                {

                    for (int xx = 0; xx < 2; xx++)
                    {
                        map.Map[0, i + xx-2] = -1;

                    }

                    var v = -Vector3.left;
                    Vector3 position = blocks[j, i].transform.position + (v * 2f);

                    //Make origin at the corner
                    obj = Instantiate(NSLevel.Resources.Instance.kiosk);
                    obj.transform.position = position;// + Vector3.left;
                    obj.transform.SetParent(NSLevel.Level.Instance.transform);

                    //obj.transform.rotation = Quaternion.LookRotation(Vector3.left, Vector3.left);

                    return;
                }

            }

            /// UP
            /// 
            i = map.Map.GetLength(1) - 1;
            for (j = 0; j < map.Map.GetLength(0); j++)
            {

                bool found = true;
                for (int xx = 0; xx < 2; xx++)
                {
                    try
                    {
                        if (map.Map[j + xx, i] != 0)
                        {
                            found = false;
                            break;
                        }
                    }
                    catch (System.Exception e)
                    {
                        found = false;
                        break;
                    }
                }
                // Install Kiosk up
                if (found)
                {
                    var v = Vector3.forward;
                    Vector3 position = blocks[j, i].transform.position + (v * 4f);

                    //Make origin at the corner
                    obj = Instantiate(NSLevel.Resources.Instance.kiosk);
                    obj.transform.position = position;
                    obj.transform.SetParent(NSLevel.Level.Instance.transform);

                    //obj.transform.rotation = Quaternion.LookRotation(-Vector3.forward, Vector3.up);


                    for (int xx = 0; xx < 2; xx++)
                    {
                        map.Map[j-2 + xx, i] = -1;
                    }

                    return;
                }

            }


            /// RIGHT
            /// 
            j = map.Map.GetLength(0) - 1;
            for (i = 0; i < map.Map.GetLength(0); i++)
            {

                bool found = true;
                for (int xx = 0; xx < 4; xx++)
                {
                    try
                    {
                        if (map.Map[j, i + xx] != 0)
                        {
                            found = false;
                            break;
                        }
                    }
                    catch (System.Exception e)
                    {
                        found = false;
                        break;
                    }
                }
                // Install Kiosk up
                if (found)
                {
                    var v = Vector3.right;
                    Vector3 position = blocks[j, i].transform.position + (v * 4f);

                    //Make origin at the corner
                    obj = Instantiate(NSLevel.Resources.Instance.kiosk);
                    obj.transform.position = position;
                    obj.transform.SetParent(NSLevel.Level.Instance.transform);

                    //obj.transform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);

                    for (int xx = 0; xx < 2; xx++)
                    {
                        map.Map[j, i-2+xx] = -1;
                    }

                    return;
                }

            }

        }








        public override void Tick()
        {
            base.Tick();
        }

    }
}
