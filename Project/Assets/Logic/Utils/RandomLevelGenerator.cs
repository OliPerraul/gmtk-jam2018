using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLevelGenerator : MonoBehaviour {

    [SerializeField]
    private int width;
    [SerializeField]
    private int height;


    //buildings and solids
    [SerializeField]
    private int minobs;
    [SerializeField]
    private int maxobs;

    private int numofobs;
    private static int obstypes = 4;
    private static int[,] obs = new int[4, 2] { { 1, 1 } , { 1, 1 }, { 1, 1 } , { 1, 1 } };


    //dough balls
    [SerializeField]
    private int mindough;
    [SerializeField]
    private int maxdough;

    private int numofdough;


    //entrance points
    [SerializeField]
    private int minent;
    [SerializeField]
    private int maxent;

    private int numofent;


    //matrices
    private int[,] mapmatrix;
    private int[,] obsIDmatrix;

    public void Generate () {
        numofobs = Random.Range(minobs, maxobs);
        numofent = Random.Range(minent, maxent);
        numofdough = Random.Range(mindough, maxdough);

        GenerateEmptyMap();
        GenerateEntrances();
        GenerateBuildings();
        GenerateGoat();
        GenerateDough();
        //PrintMatrix(mapmatrix);
        //PrintMatrix(obsIDmatrix);
    }

    public int[,] GetMapMatrix()
    {
        return mapmatrix;
    }

    public int[,] GetObsIDMatrix()
    {
        return obsIDmatrix;
    }

    public void PrintMatrix(int[,] matrix)
    {
        string temp;
        for (int y=0; y<height; y++)
        {
            temp = "";
            for (int x=0; x<width; x++)
            {
                temp += matrix[y, x].ToString() + " ";
            }
            print(temp);
        }
    }

    private void PlaceBlock(int x, int y, int[] block, int blockID)
    {
        for (int yc=0; yc<block[1]; yc++)
        {
            for (int xc=0; xc<block[0]; xc++)
            {
                mapmatrix[y + yc, x + xc] = 1;
            }
        }
        obsIDmatrix[y, x] = blockID;
    }

    //test if block fits in spot
    private bool TestSpaceAvailable(int x, int y, int[] block)
    {
        bool x_ok = true;
        if (x+(block[0]-1) <= width-1)
        {
            for (int bx=0; bx<block[0]; bx++)
            {
                if (mapmatrix[y, x+bx] == 1)
                {
                    x_ok = false;
                    break;
                }
            }
        }
        else
        {
            x_ok = false;
        }

        bool y_ok = true;
        if (x_ok)
        {            
            if (y+(block[1]-1) <= height - 1)
            {
                for (int by = 0; by < block[1]; by++)
                {
                    if (mapmatrix[y+by, x] == 1)
                    {
                        y_ok = false;
                        break;
                    }
                }
            }
            else
            {
                y_ok = false;
            }
        }
        else
        {
            y_ok = false;
        }

        if (x_ok && y_ok)
        {
            return true;
        }

        return false;
    }

    //test if no perpendicular blocks intersecting middle of building
    private bool TestPerpRule(int x, int y, int[] block)
    {
        bool x_ok = true;
        for (int i=0; i<block[0]-2; i++)
        {
            if (y!=0)
            {
                if (mapmatrix[y-1, x+1+i] == 1)
                {
                    x_ok = false;
                    break;
                }
            }
            if (y != height-1)
            {
                if (mapmatrix[y+1, x+1+i] == 1)
                {
                    x_ok = false;
                    break;
                }
            }
        }

        bool y_ok = true;
        if (x_ok)
        {
            for (int j = 0; j < block[1] - 2; j++)
            {
                if (x != 0)
                {
                    if (mapmatrix[y +1+j, x-1] == 1)
                    {
                        y_ok = false;
                        break;
                    }
                }
                if (x != width - 1)
                {
                    if (mapmatrix[y +1+j, x +1] == 1)
                    {
                        y_ok = false;
                        break;
                    }
                }
            }
        }
        else
        {
            y_ok = false;
        }

        if (x_ok && y_ok)
        {
            return true;
        }

        return false;
    }

    private bool TestValidSpace(int x, int y, int[] block)
    {
        if (TestSpaceAvailable(x, y, block))
        {
            if (TestPerpRule(x, y, block))
            {
                return true;
            }
        }

        return false;
    }

    private void GenerateEmptyMap()
    {
        mapmatrix = new int[height, width];
        obsIDmatrix = new int[height, width];

        for (int y=0; y<height; y++)
        {
            for (int x=0; x<width; x++)
            {
                mapmatrix[y, x] = 0;
                obsIDmatrix[y, x] = -1;
            }
        }
    }

    private void GenerateEntrances()
    {
        int spotx;
        int spoty;

        for (int i=0; i<numofent; i++)
        {
            bool space = false;
            while (space==false)
            {
                // 0 top/bottom, 1 left/right
                int side = Random.Range(0, 2);

                if (side==0)
                {
                    spotx = Random.Range(1, width - 1);
                    spoty = Random.Range(0, 2) * (height-1);
                }
                else
                {
                    spotx = Random.Range(0,2) * (width-1);
                    spoty = Random.Range(0, height - 1);
                }

                int[] block = new int[]{1, 1};

                if (TestSpaceAvailable(spotx, spoty, block))
                {
                    space = true;
                    PlaceBlock(spotx, spoty, block, -5);
                }

            }
        }
    }

    private void GenerateBuildings()
    {
        for (int i=0; i<numofobs; i++)
        {
            int blockID = Random.Range(0, obstypes);

            bool space = false;
            while (space == false)
            {
                int spotx = Random.Range(0, width);
                int spoty = Random.Range(0, height);

                int[] block = new int[] { obs[blockID, 0], obs[blockID, 1] };

                if (TestValidSpace(spotx, spoty, block))
                {
                    space = true;
                    PlaceBlock(spotx, spoty, block, blockID);
                }
            }
        }
    }

    private void GenerateGoat()
    {
        bool space = false;
        while (space== false)
        {
            int spotx = Random.Range(0, width);
            int spoty = Random.Range(0, height);

            int[] block = new int[] { 1, 1 };
            
            if (TestSpaceAvailable(spotx, spoty, block))
            {
                space = true;
                PlaceBlock(spotx, spoty, block, 99);
            }
        }
    }

    private void GenerateDough()
    {
        for (int i=0; i<numofdough; i++)
        {
            bool space = false;
            while (space == false)
            {
                int spotx = Random.Range(0, width);
                int spoty = Random.Range(0, height);

                int[] block = new int[] { 1, 1 };

                if (TestSpaceAvailable(spotx, spoty, block))
                {
                    space = true;
                    PlaceBlock(spotx, spoty, block, 100);
                }
            }
        }
    }

}
