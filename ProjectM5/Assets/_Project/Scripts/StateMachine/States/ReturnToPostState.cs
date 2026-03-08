using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPostState : BaseState
{
    private Vector3 pointOrigin;
    [SerializeField] private string nameWalkPar = "IsWalking";

    private void Start()
    {
        pointOrigin = transform.position;
    }
    public override void OnStateEnter()
    {
        controller.EnemyAgent.speed = 0.3f;
        controller.Animator.SetBool(nameWalkPar,true);
    }
    public override void StateUpdate()
    {
        if (controller.EnemyAgent.destination != pointOrigin) controller.EnemyAgent.SetDestination(pointOrigin);
    }
    public override void OnStateExit()
    {
        controller.Animator.SetBool(nameWalkPar,false);

    }


}
