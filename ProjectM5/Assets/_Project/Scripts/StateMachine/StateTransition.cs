using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateTransition : MonoBehaviour
{
    [SerializeField] private BaseState nextState;
    protected BaseState ownerState;
    protected StateController controller;

    public BaseState NextState { get { return nextState; } }

    public void SetUpTransition(BaseState ownerState, StateController controller)
    {
        this.ownerState= ownerState; 
        this.controller = controller;
    }
    public abstract bool IsConditionMeet();
}
