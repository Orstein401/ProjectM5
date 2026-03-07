using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    [Header("Waypoint")]
    [SerializeField] private Transform[] waypoints;

    [SerializeField] private string nameWalkPar="IsWalking";

    private int currentPoint;

    [Header("Parametres Couritine")]
    private Coroutine waitRoutine;
    private bool isWaiting;
    public override void OnStateEnter()
    {
        controller.EnemyAgent.SetDestination(waypoints[currentPoint].position);
        controller.Animator.SetBool(nameWalkPar, true);
    }

    public override void StateUpdate()
    {
        if (isWaiting) return;
        if (!controller.EnemyAgent.pathPending && controller.EnemyAgent.remainingDistance <= controller.EnemyAgent.stoppingDistance)
        {
            if (waitRoutine == null) //non è necessario, ma fa un controllo aggiuntivo
            {
                waitRoutine = StartCoroutine(Wait());
            }

        }
    }
    public override void OnStateExit()
    {
        if (waitRoutine != null)
        {
            StopCoroutine(waitRoutine);
            isWaiting = false;
            waitRoutine = null;
        }
        if (controller.EnemyAgent.isStopped) controller.EnemyAgent.isStopped = false;


    }
    IEnumerator Wait()
    {

        controller.EnemyAgent.isStopped = true;
        isWaiting = true;
        controller.Animator.SetBool(nameWalkPar,false);
        yield return new WaitForSeconds(controller.Interval);

        currentPoint = (currentPoint + 1) % waypoints.Length;
        controller.EnemyAgent.SetDestination(waypoints[currentPoint].position);

        controller.EnemyAgent.isStopped = false;
        isWaiting = false;
        waitRoutine = null;

        controller.Animator.SetBool(nameWalkPar, true);

    }
}
