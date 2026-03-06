using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    [Header("State parametres")]
    [SerializeField] private BaseState startState;
    private BaseState currentState;
    private BaseState[] allStates;

    [Header("Enemy Parametres")]
    [Header("Componets")]
    protected NavMeshAgent enemyAgent;
    protected LineRenderer lineRend;
    protected AnimationScript anim;

    [Header("Stat for Visual Cone")]
    [SerializeField] protected Transform target;
    [SerializeField] protected SO_StatEnemy stat;

    [Header("Parametres Interval")]
    [SerializeField] protected float interval;


    //Getter
    public NavMeshAgent EnemyAgent { get { return enemyAgent; } }

    public Transform Target { get { return target; } }
    public SO_StatEnemy Stat { get { return stat; } }

    public float Interval { get { return interval; } }
    private void Awake()
    {
        allStates = GetComponentsInChildren<BaseState>();
        enemyAgent = GetComponent<NavMeshAgent>();
        foreach (BaseState state in allStates)
        {
            state.SetUp(this);
        }
    }
    private void Start()
    {
        if (startState != null)
        {
            SetState(startState);
        }
    }
    private void Update()
    {
        if (currentState != null)
        {
            currentState.StateUpdate();
            foreach (StateTransition transition in currentState.Transitions)
            {
                if (transition.IsConditionMeet())
                {
                    SetState(transition.NextState);
                    break;
                }
            }
        }
    }

    public void SetState(BaseState state)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }
        currentState = state;
        currentState.OnStateEnter();
    }
}
