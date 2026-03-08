using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SearchingState : BaseState
{
    [SerializeField] private float rotationSpeed=50f;
    private float angleRotationY;
    private float baseRotationY;
    private Coroutine searchRoutine;
    private int currentIndex=0;

    private bool arrived;

    private float searchTimer;
    [SerializeField] private float maxSearchTime = 6f;

    [SerializeField] private float[] searchAngles =new float[] { 45f, 0f, -45f };

    [SerializeField] private string nameRunPar = "IsRunning";
    [SerializeField] private string nameTurnPar = "IsTurning";


    public override void OnStateEnter()
    {
        arrived = false;
        currentIndex = 0;
        searchTimer = 0;
        controller.Animator.SetBool(nameRunPar,true);
        controller.EnemyAgent.speed = 3.5f;
    }

    public override void StateUpdate()
    {
     
        controller.EnemyAgent.SetDestination(controller.TriggerPoint);

        if (!controller.EnemyAgent.pathPending && controller.EnemyAgent.remainingDistance <= controller.EnemyAgent.stoppingDistance)
        {
            if (!arrived)
            {
                arrived=true;
                baseRotationY = transform.eulerAngles.y;
                angleRotationY = baseRotationY + searchAngles[currentIndex];
                controller.Animator.SetBool(nameRunPar, false);

            }

            searchTimer += Time.deltaTime;
            if (searchTimer >= maxSearchTime)
            {
                controller.IsTrigger = false;
            }

            Quaternion angleRotation = Quaternion.Euler(0f, angleRotationY, 0f);
            if (Quaternion.Angle(controller.Parent.rotation, angleRotation) > 0.05f)
            {
                controller.Parent.transform.rotation = Quaternion.RotateTowards(controller.Parent.transform.rotation, angleRotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                if (searchRoutine == null)
                {
                    searchRoutine = StartCoroutine(Searching());

                }
            }
        }
      

    }

    public override void OnStateExit()
    {
        if(searchRoutine != null)
        {
            StopCoroutine(searchRoutine);
            searchRoutine = null;
        }
    }
    IEnumerator Searching()
    {
       
        controller.Animator.SetBool(nameTurnPar, false);

        yield return new WaitForSeconds(1.5f);
        controller.Animator.SetBool(nameTurnPar, true);
        currentIndex++;
        currentIndex %= searchAngles.Length;

        angleRotationY = baseRotationY + searchAngles[currentIndex];

        searchRoutine = null;
    }
}
