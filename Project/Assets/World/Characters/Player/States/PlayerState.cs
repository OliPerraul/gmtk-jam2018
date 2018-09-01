using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSLevel;

public class PlayerState : AState
{
    public bool turn = false;
    protected List<Block> selectableBlocks = new List<Block>();
    protected Stack<Block> path = new Stack<Block>();
    protected Block currentBlock;

    public bool moving = false;
    public int move = 5;
    public float jumpHeight = 2;
    public float moveSpeed = 2;
    public float jumpVelocity = 4.5f;
    //Raycasts will not detect colliders for which the raycast origin is inside the collider"
    [SerializeField]
    protected float rayCastOffsetUp = 4;
    [SerializeField]
    protected float rayCastDistance = 4;
    [SerializeField]
    protected LayerMask layer;
    protected Vector3 velocity = new Vector3();
    protected Vector3 heading = new Vector3();
    protected float halfHeight = 0;
    protected bool fallingDown = false;
    protected bool jumpingUp = false;
    protected bool movingEdge = false;
    protected Vector3 jumpTarget;
    protected Block actualTargetBlock;


    public Player Context
    {
        get { return (Player)context; }
    }

    public override string GetName()
    {
        throw new NotImplementedException();
    }

}

