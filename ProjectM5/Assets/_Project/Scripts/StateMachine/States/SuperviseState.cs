using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperviseState : BaseState
{
    [SerializeField] private Transform parent;
    [SerializeField] private float rotationSpeed;
    [SerializeField] float rotationStep = 45f;

    private float angleRotationY;
    private Coroutine waitRoutine;

    private void Start()
    {
        angleRotationY = transform.eulerAngles.y;
    }
    public override void OnStateEnter()
    {
       
    }
    public override void StateUpdate()
    {
        Quaternion angleRotation = Quaternion.Euler(0f, angleRotationY, 0f);

        if (Quaternion.Angle(transform.rotation, angleRotation) > 0.05f)
        {
            parent.transform.rotation = Quaternion.RotateTowards(parent.transform.rotation, angleRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (waitRoutine == null)
            {
                waitRoutine = StartCoroutine(WaitAndSetAngle());

            }
        }
    }
    public override void OnStateExit()
    {
        if (waitRoutine != null)
        {
            StopCoroutine(waitRoutine);
            waitRoutine = null;
        }
    }

    IEnumerator WaitAndSetAngle()
    {
        yield return new WaitForSeconds(controller.Interval);
        angleRotationY += rotationStep;
        angleRotationY %= 360f;
        waitRoutine = null;


    }

}
