using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using ProceduralToolkit;

/*
 
     		Flat Ground

		Tall Grass
		

		Trees

		Shops
			Kiosk
			Shop

     
     */



namespace NSLevel
{

    public class MapHandler
    {
        Random rand = new Random();



        public int[,] Map;

        public int MapWidth { get; set; }
        public int MapHeight { get; set; }
        public int PercentAreWalls { get; set; }
        public int treeVal = 2;

        public int playerVal = 3;
        public int toolVal0 = 4;
        public int toolVal1 = 5;
        public int toolVal3 = 6;


        public int percentTree = 5;

        public int percentBordersAreWall = 50;



        int fillValue = 1;
        int empty = 0;
        public void SetFillValue(int val)
        {
            if (val == 0)
            {
                empty = 1;
                fillValue = 0;
            }
            else
            {
                fillValue = 1;
                empty = 0;
            }

        }


        public MapHandler()
        {
            MapWidth = 40;
            MapHeight = 21;
            PercentAreWalls = 40;

            RandomFillMap();

        }

        public void MakeCaverns()
        {
            // By initilizing column in the outter loop, its only created ONCE
            for (int column = 0, row = 0; row <= MapHeight - 1; row++)
            {
                for (column = 0; column <= MapWidth - 1; column++)
                {
                    Map[column, row] = PlaceWallLogic(column, row);
                }
            }
        }

        public int PlaceWallLogic(int x, int y)
        {
            int numWalls = GetAdjacentWalls(x, y, 1, 1);


            if (Map[x, y] == fillValue)
            {
                if (numWalls >= 4)
                {
                    if (RandomPercent(percentTree) == fillValue)
                    {
                        return treeVal;
                    }

                    return fillValue;
                }
                if (numWalls < 2)
                {
                    return empty;
                }

            }
            else
            {
                if (numWalls >= 5)
                {
                    if (RandomPercent(percentTree) == fillValue)
                    {
                        return treeVal;
                    }

                    return fillValue;
                }
            }
            return empty;
        }

        public int GetAdjacentWalls(int x, int y, int scopeX, int scopeY)
        {
            int startX = x - scopeX;
            int startY = y - scopeY;
            int endX = x + scopeX;
            int endY = y + scopeY;

            int iX = startX;
            int iY = startY;

            int wallCounter = 0;

            for (iY = startY; iY <= endY; iY++)
            {
                for (iX = startX; iX <= endX; iX++)
                {
                    if (!(iX == x && iY == y))
                    {
                        if (IsWall(iX, iY))
                        {
                            wallCounter += 1;
                        }
                    }
                }
            }
            return wallCounter;
        }

        bool IsWall(int x, int y)
        {
            // Consider out-of-bound a wall
            if (IsOutOfBounds(x, y))
            {
                return true;
            }

            if (Map[x, y] == fillValue)
            {
                return true;
            }

            if (Map[x, y] == empty)
            {
                return false;
            }
            return false;
        }

        bool IsOutOfBounds(int x, int y)
        {
            if (x < 0 || y < 0)
            {
                return true;
            }
            else if (x > MapWidth - 1 || y > MapHeight - 1)
            {
                return true;
            }
            return false;
        }

        public void PrintMap()
        {
            PrintMatrix(Map);
        }

        /*
        string MapToString()
        {
            string returnString = string.Join(" ", // Seperator between each element
                                              "Width:",
                                              MapWidth.ToString(),
                                              "\tHeight:",
                                              MapHeight.ToString(),
                                              "\t% Walls:",
                                              PercentAreWalls.ToString(),
                                              "\n"
                                             );

            List<string> mapSymbols = new List<string>();
            mapSymbols.Add(".");
            mapSymbols.Add("#");
            mapSymbols.Add("+");

            for (int column = 0, row = 0; row < MapHeight; row++)
            {
                for (column = 0; column < MapWidth; column++)
                {
                    returnString += mapSymbols[Map[column, row]];
                }
                returnString += "\n";
            }
            return returnString;
        }
        */
        public void BlankMap()
        {
            for (int column = 0, row = 0; row < MapHeight; row++)
            {
                for (column = 0; column < MapWidth; column++)
                {
                    Map[column, row] = empty;
                }
            }
        }

        public void RandomFillMap()
        {
            // New, empty map
            Map = new int[MapWidth, MapHeight];

            int mapMiddle = 0; // Temp variable
            for (int column = 0, row = 0; row < MapHeight; row++)
            {
                for (column = 0; column < MapWidth; column++)
                {
                    // If coordinants lie on the the edge of the map (creates a border)
                    if (column == 0)
                    {
                        if (RandomPercent(percentTree) == fillValue)
                        {
                            Map[column, row] = treeVal;
                        }
                        else
                        {
                            Map[column, row] = RandomPercent(percentBordersAreWall);
                        }
                    }
                    else if (row == 0)
                    {
                        if (RandomPercent(percentTree) == fillValue)
                        {
                            Map[column, row] = treeVal;
                        }
                        else
                        {
                            Map[column, row] = RandomPercent(percentBordersAreWall);
                        }
                    }
                    else if (column == MapWidth - 1)
                    {
                        if (RandomPercent(percentTree) == fillValue)
                        {
                            Map[column, row] = treeVal;
                        }
                        else
                        {
                            Map[column, row] = RandomPercent(percentBordersAreWall);
                        }
                    }
                    else if (row == MapHeight - 1)
                    {
                        if (RandomPercent(percentTree) == fillValue)
                        {
                            Map[column, row] = treeVal;
                        }
                        else
                        {
                            Map[column, row] = RandomPercent(percentBordersAreWall);
                        }
                    }
                    // Else, fill with a wall a random percent of the time
                    else
                    {
                        mapMiddle = (MapHeight / 2);

                        if (row == mapMiddle)
                        {
                            if (RandomPercent(percentTree) == fillValue)
                            {
                                Map[column, row] = treeVal;
                            }
                            else
                            {
                                Map[column, row] = RandomPercent(percentBordersAreWall);
                            }
                        }
                        else
                        {
                            if (RandomPercent(percentTree) == fillValue)
                            {
                                Map[column, row] = treeVal;
                            }
                            else
                            {
                                Map[column, row] = RandomPercent(percentBordersAreWall);
                            }
                        }
                    }
                }
            }
        }

        int RandomPercent(int percent)
        {
            float val = percent;
            val = val / 100;


            if (RandomUtils.PercentChance(val))
            {
                return fillValue;
            }
            return empty;
        }

        public MapHandler(int mapWidth, int mapHeight, ref int[,] map, int percentWalls)
        {
            this.MapWidth = mapWidth;
            this.MapHeight = mapHeight;
            this.PercentAreWalls = percentWalls;
            // this.Map = new int[this.MapWidth, this.MapHeight];
            this.Map = map;
            RandomFillMap();
        }


        public void PrintMatrix(int[,] matrix)
        {
            string temp;
            for (int y = 0; y < this.MapHeight; y++)
            {
                temp = "";
                for (int x = 0; x < this.MapWidth; x++)
                {
                    temp += matrix[y, x].ToString() + " ";
                }
                Debug.Log(temp);
            }
        }


        public void PlacePlayerAndTools()
        {

            for (int row = 0; row < MapHeight; row++)
            {
                for (int column = 0; column < MapWidth; column++)
                {
                    if (RandomUtils.PercentChance(.85f))
                        continue;

                    if (row == 0)
                        continue;

                    if (column == 0)
                        continue;

                    if (column == MapWidth - 1)
                        continue;

                    if (row == MapHeight - 1)
                        continue;


                    int numWalls = GetAdjacentWalls(column, row, 1, 1);


                    if (numWalls <= 2)
                    {
                        Map[column, row] = playerVal;

                        int idx = playerVal;
                        int count = 0;

                        while (count < 3)
                        {

                            for (int i = row - 1; i < row + 1; i++)
                            {
                                for (int j = column - 1; j < column + 1; j++)
                                {
                                    if (RandomUtils.PercentChance(.75f))
                                        continue;
                                    if (i == row && j == column)
                                        continue;
                                    
                                    idx++;
                                    count++;
                                    
                                    Map[j, i] = idx;

                                }

                            }
                        }


                        return;


                    }
               

                }
            }

            BlankMap();
            Prepare();



        }


        public void RemoveInBetween()
        {
            int boolInt;
            bool mybool = false;
            for (int row = 0; row < MapHeight; row++)
            {
                if (row % 2 == 0)
                {
                    for (int column = 0; column < MapWidth; column++)
                    {
                        int v = Map[column, row];

                        //boolInt = mybool ? Map[column, row] : 0;
                        if (v == 3 || v == 4 || v == 5 || v == 6)
                            continue;

                        if (RandomUtils.RandomBoolean())
                            continue;

                        Map[column, row] = empty;
                        //mybool = !mybool;

                    }
                }
                else
                {

                    mybool = true;
                    for (int column = 0; column < MapWidth; column++)
                    {
                        int v = Map[column, row];

                        //boolInt = mybool ? Map[column, row] : 0;
                        if (v == 3 || v == 4 || v == 5 || v == 6)
                            continue;

                        if (RandomUtils.RandomBoolean())
                            continue;

                        boolInt = mybool ? Map[column, row] : empty;
                        Map[column, row] = boolInt;
                        mybool = !mybool;

                    }
                }


            }





        }


        public void Prepare()
        {
            SetFillValue(1);
            MakeCaverns();
            PlacePlayerAndTools();
        }

    }


}


//namespace NSLevel
//{

//    public class Generator : MonoBehaviour
//    {

//        [SerializeField]
//        private int width = 20;
//        [SerializeField]
//        private int height = 20;


//        //buildings and solids
//        [SerializeField]
//        private int minobs;
//        [SerializeField]
//        private int maxobs;

//        private int numofobs;
//        private static int obstypes = 4;
//        private static int[,] obs = new int[4, 2] { { 1, 1 }, { 1, 1 }, { 1, 1 }, { 1, 1 } };


//        //dough balls
//        [SerializeField]
//        private int mindough;
//        [SerializeField]
//        private int maxdough;

//        private int numofdough;


//        //entrance points
//        [SerializeField]
//        private int minent;
//        [SerializeField]
//        private int maxent;

//        private int numofent;


//        //matrices
//        private int[,] mapmatrix;
//        private int[,] obsIDmatrix;

//        public void Generate()
//        {
//            numofobs = Random.Range(minobs, maxobs);
//            numofent = Random.Range(minent, maxent);
//            numofdough = Random.Range(mindough, maxdough);

//            //GenerateEmptyMap();
//            //GenerateEstablishments();
//            //GenerateGrass();
//            //GenerateTrees();

//            //PrintMatrix(mapmatrix);
//            ////PrintMatrix(obsIDmatrix);

//        }

//        public int[,] GetMapMatrix()
//        {
//            return mapmatrix;
//        }

//        public int[,] GetObsIDMatrix()
//        {
//            return obsIDmatrix;
//        }



//        private void PlaceBlock(int x, int y, int[] block, int blockID)
//        {
//            for (int yc = 0; yc < block[1]; yc++)
//            {
//                for (int xc = 0; xc < block[0]; xc++)
//                {
//                    mapmatrix[y + yc, x + xc] = 1;
//                }
//            }
//            obsIDmatrix[y, x] = blockID;
//        }

//        //test if block fits in spot
//        private bool TestSpaceAvailable(int x, int y, int[] block)
//        {
//            bool x_ok = true;
//            if (x + (block[0] - 1) <= width - 1)
//            {
//                for (int bx = 0; bx < block[0]; bx++)
//                {
//                    if (mapmatrix[y, x + bx] == 1)
//                    {
//                        x_ok = false;
//                        break;
//                    }
//                }
//            }
//            else
//            {
//                x_ok = false;
//            }

//            bool y_ok = true;
//            if (x_ok)
//            {
//                if (y + (block[1] - 1) <= height - 1)
//                {
//                    for (int by = 0; by < block[1]; by++)
//                    {
//                        if (mapmatrix[y + by, x] == 1)
//                        {
//                            y_ok = false;
//                            break;
//                        }
//                    }
//                }
//                else
//                {
//                    y_ok = false;
//                }
//            }
//            else
//            {
//                y_ok = false;
//            }

//            if (x_ok && y_ok)
//            {
//                return true;
//            }

//            return false;
//        }

//        //test if no perpendicular blocks intersecting middle of building
//        private bool TestPerpRule(int x, int y, int[] block)
//        {
//            bool x_ok = true;
//            for (int i = 0; i < block[0] - 2; i++)
//            {
//                if (y != 0)
//                {
//                    if (mapmatrix[y - 1, x + 1 + i] == 1)
//                    {
//                        x_ok = false;
//                        break;
//                    }
//                }
//                if (y != height - 1)
//                {
//                    if (mapmatrix[y + 1, x + 1 + i] == 1)
//                    {
//                        x_ok = false;
//                        break;
//                    }
//                }
//            }

//            bool y_ok = true;
//            if (x_ok)
//            {
//                for (int j = 0; j < block[1] - 2; j++)
//                {
//                    if (x != 0)
//                    {
//                        if (mapmatrix[y + 1 + j, x - 1] == 1)
//                        {
//                            y_ok = false;
//                            break;
//                        }
//                    }
//                    if (x != width - 1)
//                    {
//                        if (mapmatrix[y + 1 + j, x + 1] == 1)
//                        {
//                            y_ok = false;
//                            break;
//                        }
//                    }
//                }
//            }
//            else
//            {
//                y_ok = false;
//            }

//            if (x_ok && y_ok)
//            {
//                return true;
//            }

//            return false;
//        }

//        private bool TestValidSpace(int x, int y, int[] block)
//        {
//            if (TestSpaceAvailable(x, y, block))
//            {
//                if (TestPerpRule(x, y, block))
//                {
//                    return true;
//                }
//            }

//            return false;
//        }

//        private void GenerateEmptyMap()
//        {
//            mapmatrix = new int[height, width];
//            obsIDmatrix = new int[height, width];

//            for (int y = 0; y < height; y++)
//            {
//                for (int x = 0; x < width; x++)
//                {
//                    mapmatrix[y, x] = 0;
//                    obsIDmatrix[y, x] = -1;
//                }
//            }
//        }

//        private void GenerateEstablishments()
//        {
//            int spotx;
//            int spoty;

//            for (int i = 0; i < numofent; i++)
//            {
//                bool space = false;
//                while (space == false)
//                {
//                    // 0 top/bottom, 1 left/right
//                    int side = Random.Range(0, 2);

//                    if (side == 0)
//                    {
//                        spotx = Random.Range(1, width - 1);
//                        spoty = Random.Range(0, 2) * (height - 1);
//                    }
//                    else
//                    {
//                        spotx = Random.Range(0, 2) * (width - 1);
//                        spoty = Random.Range(0, height - 1);
//                    }

//                    int[] block = new int[] { 1, 1 };

//                    if (TestSpaceAvailable(spotx, spoty, block))
//                    {
//                        space = true;
//                        PlaceBlock(spotx, spoty, block, -5);
//                    }

//                }
//            }
//        }

//        private void GenerateGrass()
//        {
//            for (int i = 0; i < numofobs; i++)
//            {
//                int blockID = Random.Range(0, obstypes);

//                bool space = false;
//                while (space == false)
//                {
//                    int spotx = Random.Range(0, width);
//                    int spoty = Random.Range(0, height);

//                    int[] block = new int[] { obs[blockID, 0], obs[blockID, 1] };

//                    if (TestValidSpace(spotx, spoty, block))
//                    {
//                        space = true;
//                        PlaceBlock(spotx, spoty, block, blockID);
//                    }
//                }
//            }
//        }
//        //


//        //

//        private void GenerateTrees()
//        {
//            bool space = false;
//            while (space == false)
//            {
//                int spotx = Random.Range(0, width);
//                int spoty = Random.Range(0, height);

//                int[] block = new int[] { 1, 1 };

//                if (TestSpaceAvailable(spotx, spoty, block))
//                {
//                    space = true;
//                    PlaceBlock(spotx, spoty, block, 99);
//                }
//            }
//        }


//        // TODO
//        private void GeneratePlayerAndTools()
//        {
//            for (int i = 0; i < numofdough; i++)
//            {
//                bool space = false;
//                while (space == false)
//                {
//                    int spotx = Random.Range(0, width);
//                    int spoty = Random.Range(0, height);

//                    int[] block = new int[] { 1, 1 };

//                    if (TestSpaceAvailable(spotx, spoty, block))
//                    {
//                        space = true;
//                        PlaceBlock(spotx, spoty, block, 100);
//                    }
//                }
//            }
//        }

//    }
//}