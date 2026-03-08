using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperviseState : BaseState
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] float rotationStep = 45f;

    private float angleRotationY;
    private Coroutine waitRoutine;

    [SerializeField] private string nameTurnPar="IsTurning";
    private void Start()
    {
        angleRotationY = transform.eulerAngles.y;
    }
    public override void OnStateEnter()
    {
        controller.EnemyAgent.speed = 0.5f;
    }
    public override void StateUpdate()
    {
        Quaternion angleRotation = Quaternion.Euler(0f, angleRotationY, 0f);

        if (Quaternion.Angle(transform.rotation, angleRotation) > 0.05f)
        {
            controller.Parent.transform.rotation = Quaternion.RotateTowards(controller.Parent.transform.rotation, angleRotation, rotationSpeed * Time.deltaTime);
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
        controller.Animator.SetBool(nameTurnPar, false);
    }

    IEnumerator WaitAndSetAngle()
    {
        controller.Animator.SetBool(nameTurnPar, false);
        yield return new WaitForSeconds(controller.Interval);
        angleRotationY += rotationStep;
        angleRotationY %= 360f;
        waitRoutine = null;

        controller.Animator.SetBool(nameTurnPar,true);


    }

}
