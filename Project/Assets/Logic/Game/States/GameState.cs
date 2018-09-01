using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameState : AState
{
    public Game Context
    {
        get { return (Game)context; }
    }

    public override string GetName()
    {
        throw new NotImplementedException();
    }



}

