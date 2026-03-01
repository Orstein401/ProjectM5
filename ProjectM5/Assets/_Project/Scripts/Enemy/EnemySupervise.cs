using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySupervise : EnemyParent
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] float rotationStep = 45f;

    private float angleRotationY;
    private Coroutine waitRoutine;

    private Vector3 pointOrigin;

 
    private void Start()
    {
        currentState = STATE.Supervise;
        angleRotationY = transform.eulerAngles.y;
        pointOrigin = transform.position;
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
            case STATE.Supervise:

                if (ConeVisual())
                {
                    currentState = STATE.Chase;
                    return;
                }
                Supervise();
                break;

            case STATE.Chase:

                if (waitRoutine != null)
                {
                    StopCoroutine(waitRoutine);
                    waitRoutine = null;
                }
                if (!ConeVisual())
                {
                    currentState = STATE.ReturnToPost;
                    return;
                }
                ChasingTarget();
                break;
            case STATE.ReturnToPost:
                if (ConeVisual())
                {
                    currentState = STATE.Chase;
                    return;
                }
                ReturnToOrigin();
                break;
        }
    }

    protected void Supervise()
    {

        Quaternion angleRotation = Quaternion.Euler(0f, angleRotationY, 0f);

        if (Quaternion.Angle(transform.rotation, angleRotation) > 0.05f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, angleRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (waitRoutine == null)
            {
                waitRoutine = StartCoroutine(WaitAndSetAngle());

            }
        }

    }

    IEnumerator WaitAndSetAngle()
    {
        yield return new WaitForSeconds(interval);
        angleRotationY += rotationStep;
        angleRotationY %= 360f;
        waitRoutine = null;

    }

    protected void ReturnToOrigin()
    {
        if (enemyAgent.destination != pointOrigin) enemyAgent.SetDestination(pointOrigin);
        if (!enemyAgent.pathPending && enemyAgent.remainingDistance <= enemyAgent.stoppingDistance)
        {
            currentState = STATE.Supervise;
        }
    }

}
