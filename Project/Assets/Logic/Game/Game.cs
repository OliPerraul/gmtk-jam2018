using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour, NSFSM.IContext {

    public static Game instance = null;
    [SerializeField]
    private NSFSM.FSM _fsm;

    public static NSFSM.FSM FSM
    {
        get { return instance._fsm; }
    }

    public void Awake()
    {
        // Prevent multiple game instance
        if (!instance)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        
        _fsm.Launch(this);
    }

    public void Update()
    {
        _fsm.Tick();
    }



}
