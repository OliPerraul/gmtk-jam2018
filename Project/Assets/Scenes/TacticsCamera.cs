using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsCamera : MonoBehaviour 
{
   public  GameObject cam0;
   public GameObject cam1;

    public static bool reversed = false;

    public void ChangeCam()
    {

        if (cam0.active)
        {
            //  Camera.SetupCurrent(cam0);
            //cam1.tag = "MainCamera";
            //cam0.tag = "";
            reversed = false;
            cam0.SetActive(false);
            cam1.SetActive(true);
        }
        else
        {
            //cam0.tag = "MainCamera";
           // cam1.tag = "";
            reversed = true;
            cam0.SetActive(true);
            cam1.SetActive(false);
        }

    }
}
