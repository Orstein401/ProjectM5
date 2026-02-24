using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroling : EnemyParent
{
    [SerializeField] private Transform[] waypoints;

    private int currentPoint;

    private Coroutine waitRoutine;
    private bool IsWaiting;

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
                    IsWaiting = false;
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
        if(IsWaiting) return;

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
        IsWaiting=true;

        yield return new WaitForSeconds(interval);

        currentPoint= (currentPoint+1)%waypoints.Length;
        enemyAgent.SetDestination(waypoints[currentPoint].position);

        enemyAgent.isStopped=false;
        IsWaiting=false;
        waitRoutine = null;

    }
}
