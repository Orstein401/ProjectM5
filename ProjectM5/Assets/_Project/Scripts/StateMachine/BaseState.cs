using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected StateController controller;

    protected StateTransition[] transitions;

    public StateTransition[] Transitions { get { return transitions; } }
    public void SetUp(StateController controller)
    {
        this.controller = controller;
        transitions = GetComponents<StateTransition>();
        foreach(StateTransition transition in transitions)
        {
            transition.SetUpTransition(this, controller);
        }
    }
    public abstract void OnStateEnter();
    public abstract void StateUpdate();
    public abstract void OnStateExit();
}
