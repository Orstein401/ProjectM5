using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    [SerializeField] protected float chaseUpdateInterval=0.1f;
    protected float lastUpdateChase;
    public override void OnStateEnter()
    {

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

    }
}