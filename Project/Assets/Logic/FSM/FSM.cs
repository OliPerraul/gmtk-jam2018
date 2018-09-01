using UnityEngine;
using System.Collections.Generic;
#if UNITY_ANALYTICS
using UnityEngine.Analytics;
#endif

namespace NSFSM
{
    /// <summary>
    /// The Game manager is a state machine, that will switch between state according to current gamestate.
    /// </summary>
    public class FSM : MonoBehaviour
    {

        public IContext context;

        static public NSFSM.FSM instance { get { return s_Instance; } }
        static protected NSFSM.FSM s_Instance;

        public AState[] states;
        public AState Top { get { if (m_StateStack.Count == 0) return null; return m_StateStack[m_StateStack.Count - 1]; } }

        protected List<AState> m_StateStack = new List<AState>();
        protected Dictionary<string, AState> m_StateDict = new Dictionary<string, AState>();


        public AState ________currentState_______;

        public void Launch(IContext from)
        {
            s_Instance = this;

            // We build a dictionnary from state for easy switching using their name.
            m_StateDict.Clear();

            if (states.Length == 0)
                return;

            for (int i = 0; i < states.Length; ++i)
            {
                m_StateDict.Add(states[i].GetName(), states[i]);
                states[i].context = from;
            }

            m_StateStack.Clear();
            PushState(states[0].GetName());
        }

        public void Tick()
        {
            ________currentState_______ = m_StateStack[m_StateStack.Count - 1];


            if (m_StateStack.Count > 0)
            {
                m_StateStack[m_StateStack.Count - 1].Tick();
            }
        }

        // State management
        public void SwitchState(string newState, params GameObject[] args)
        {
            AState state = FindState(newState);
            if (state == null)
            {
                Debug.LogError("Can't find the state named " + newState);
                return;
            }

            m_StateStack[m_StateStack.Count - 1].Exit(state);
            state.Enter(m_StateStack[m_StateStack.Count - 1], args);
            m_StateStack.RemoveAt(m_StateStack.Count - 1);
            m_StateStack.Add(state);
        }

        public AState FindState(string stateName)
        {
            AState state;
            if (!m_StateDict.TryGetValue(stateName, out state))
            {
                return null;
            }

            return state;
        }

        public void PopState()
        {
            if (m_StateStack.Count < 2)
            {
                Debug.LogError("Can't pop states, only one in stack.");
                return;
            }

            m_StateStack[m_StateStack.Count - 1].Exit(m_StateStack[m_StateStack.Count - 2]);
            m_StateStack[m_StateStack.Count - 2].Enter(m_StateStack[m_StateStack.Count - 2]);
            m_StateStack.RemoveAt(m_StateStack.Count - 1);
        }

        public void PushState(string name)
        {
            AState state;
            if (!m_StateDict.TryGetValue(name, out state))
            {
                Debug.LogError("Can't find the state named " + name);
                return;
            }

            if (m_StateStack.Count > 0)
            {
                m_StateStack[m_StateStack.Count - 1].Exit(state);
                state.Enter(m_StateStack[m_StateStack.Count - 1]);
            }
            else
            {
                state.Enter(null);
            }
            m_StateStack.Add(state);
        }
    }

}

