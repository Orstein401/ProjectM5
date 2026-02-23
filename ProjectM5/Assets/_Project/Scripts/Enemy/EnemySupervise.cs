using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySupervise : EnemyParent
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationTime;

    private float angleRotationY;
    private bool isRotating;
    private Coroutine waitRoutine;

    private void Start()
    {
        currentState = STATE.Supervise;
        angleRotationY = transform.eulerAngles.y;
        Debug.Log(angleRotationY);
    }

    private void Update()
    {
        DrawConeOfViewQuaterion(subdivision);
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
                Supervising();
                break;

            case STATE.Chase:

                if (!ConeVisual())
                {
                    currentState = STATE.Supervise;
                    return;
                }
                ChasingTarget();
                break;
        }
    }

    protected void Supervising()
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
                waitRoutine= StartCoroutine(CalculateRotation());

            }
        }

    }

    IEnumerator CalculateRotation()
    {

        yield return new WaitForSeconds(interval);
        angleRotationY += 45f;
        if (angleRotationY >= 360f) angleRotationY = 0f;
        waitRoutine = null;
        Debug.Log(angleRotationY);

    }
}
