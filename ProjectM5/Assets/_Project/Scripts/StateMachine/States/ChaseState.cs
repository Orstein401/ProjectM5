using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    [SerializeField] protected float chaseUpdateInterval=0.1f;
    [SerializeField] private string nameRunPar="IsRunning";

    protected float lastUpdateChase;
    public override void OnStateEnter()
    {
        controller.EnemyAgent.speed = 3.5f;
        controller.Animator.SetBool(nameRunPar, true);
    }
    public override void StateUpdate()
    {
        if (controller.Target != null && Time.time - lastUpdateChase >= chaseUpdateInterval)
        {
            controller.EnemyAgent.SetDestination(controller.Target.position);
            lastUpdateChase = Time.time;
        }
    }
    public override void OnStateExit()
    {
        controller.Animator.SetBool(nameRunPar, false);

    }
}