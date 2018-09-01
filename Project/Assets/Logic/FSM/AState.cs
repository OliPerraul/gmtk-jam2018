using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AState : MonoBehaviour
{
    public NSFSM.IContext context;

    [SerializeField]
    private bool debug = false;

    private bool active;
    public bool Active { get { return active; } }

    protected static AState instance = null;

    public abstract string GetName();

    [SerializeField]
    private NSFSM.AStateComponent[] components;


    public NSFSM.AStateComponent GetStateComponent(string name)
    {
        foreach (NSFSM.AStateComponent component in components)
        {
            if (component.GetName() == name)
                return component;   
        }

        throw new Exception("State component " + name + " not found.");
    }



    public virtual void Enter(AState from, GameObject arg = null)
    {
        if(debug)
        Debug.Log(GetName() + " Entered");

        foreach (NSFSM.AStateComponent component in components)
        {
            // TODO add arg
            component.Enter();
        }


        active = true;
    }

    public virtual void Exit(AState to)
    {
        if(debug)
        Debug.Log(GetName() + " Exit");

        foreach (NSFSM.AStateComponent component in components)
        {
            component.Exit();
        }
        
        active = false;       
    }

    public virtual void Tick()
    {
        if(debug)
        Debug.Log(GetName() + " Tick");

        foreach (NSFSM.AStateComponent component in components)
        {
            component.Tick();
        }

    }
}


