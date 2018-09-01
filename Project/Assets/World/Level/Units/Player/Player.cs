using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSTacticsMovement;

public class Player : Unit, NSFSM.IContext {

    public NSGame.Resources.TOOL_TYPE equippedTool;
    public InputControllerDefault inputs;
    public NSFSM.FSM fsm;
    public Animator anim;                      // Reference to the animator component.

    public float speed = 6f;            // The speed that the player will move at.
    public Vector3 movement;                   // The vector to store the direction of the player's movement.

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

}
