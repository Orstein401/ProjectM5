using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPostState : BaseState
{
    private Vector3 pointOrigin;

    private void Start()
    {
        pointOrigin = transform.position;
    }
    public override void OnStateEnter()
    {
    }
    public override void StateUpdate()
    {
        if (controller.EnemyAgent.destination != pointOrigin) controller.EnemyAgent.SetDestination(pointOrigin);
    }
    public override void OnStateExit()
    {
        
    }


}
