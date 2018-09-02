using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSTacticsMovement;
using NSUnit;

public class Player : Unit, NSFSM.IContext {

    public NSUnit.Tool.TOOL_TYPE equippedTool;
    public InputControllerDefault inputs;
    public NSFSM.FSM fsm;
    public Animator anim;                      // Reference to the animator component.

    public float speed = 6f;            // The speed that the player will move at.
    public Vector3 movement;                   // The vector to store the direction of the player's movement.


   public GameObject rack;
   public GameObject shovel;
 public   GameObject axe;


    // Use this for initialization
    void Start () {
        //movement = new Vector3();
        fsm.Launch(this);
        type = TYPE.PLAYER;
    }
	
	// Update is called once per frame
	void Update () {
        fsm.Tick();
	}

    public void ChangeEquipTool()
    {
        switch (equippedTool)
        {
            case Tool.TOOL_TYPE.NONE:
           
                axe.gameObject.SetActive(false);
                rack.gameObject.SetActive(false);
                shovel.gameObject.SetActive(false);

                break;

            case Tool.TOOL_TYPE.AXE:
                axe.gameObject.SetActive(true);
                rack.gameObject.SetActive(false);
                shovel.gameObject.SetActive(false);
                break;

            case Tool.TOOL_TYPE.SHOVEl:
                axe.gameObject.SetActive(false);
                rack.gameObject.SetActive(false);
                shovel.gameObject.SetActive(true);
                break;

            case Tool.TOOL_TYPE.RACK:
                axe.gameObject.SetActive(false);
                rack.gameObject.SetActive(true);
                shovel.gameObject.SetActive(false);
                break;
        }




    }
        
}
