using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroling : EnemyParent
{
    [Header("Waypoint")]
    [SerializeField] private Transform[] waypoints;
    private int currentPoint;

    [Header("Parametres Couritine")]
    private Coroutine waitRoutine;
    private bool isWaiting;

    [Header("Parametres Animation")]
    private bool isWalking;
    private bool isRunning;

    private void Start()
    {
        currentState = STATE.Patrol;
        enemyAgent.SetDestination(waypoints[currentPoint].position);
    }
    private void Update()
    {
        DrawConeOfViewQuaterion(stat.Subdivision);
        StateMachine();

    }

    protected override void StateMachine()
    {

        switch (currentState)
        {
            case STATE.Patrol:

                if (ConeVisual())
                {
                    currentState = STATE.Chase;

                    return;
                }
                Patroling();
                break;

            case STATE.Chase:

                if (waitRoutine!=null)
                {
                    StopCoroutine(waitRoutine);
                    isWaiting = false;
                    waitRoutine = null;
                }

                if(enemyAgent.isStopped)enemyAgent.isStopped = false;

                if (!ConeVisual())
                {
                    currentState = STATE.Patrol;
                    enemyAgent.SetDestination(waypoints[currentPoint].position);

                    return;
                }
                ChasingTarget();
                break;
        }
    }

    protected void Patroling()
    {
        if(isWaiting) return;

        if (!enemyAgent.pathPending && enemyAgent.remainingDistance <= enemyAgent.stoppingDistance)
        {
            if (waitRoutine == null) //non è necessario, ma fa un controllo aggiuntivo
            {
                waitRoutine = StartCoroutine(Wait());
            }
          
        }



    }
    IEnumerator Wait()
    {
      
        enemyAgent.isStopped = true;
        isWaiting=true;

        yield return new WaitForSeconds(interval);

        currentPoint= (currentPoint+1)%waypoints.Length;
        enemyAgent.SetDestination(waypoints[currentPoint].position);

        enemyAgent.isStopped=false;
        isWaiting=false;
        waitRoutine = null;

    }
}
