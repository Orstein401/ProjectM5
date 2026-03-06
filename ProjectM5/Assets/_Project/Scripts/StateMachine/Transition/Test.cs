using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : StateTransition
{
    public bool active;
    public override bool IsConditionMeet()
    {
        Vector3 toTarget = controller.Target.position - transform.position;
        float sqrDistance = toTarget.sqrMagnitude;

        if (sqrDistance < controller.Stat.SightDistance * controller.Stat.SightDistance)
        {
            return true;
        }

        float distance = Mathf.Sqrt(sqrDistance);
        toTarget /= distance;

        if (Vector3.Dot(transform.forward, toTarget) > Mathf.Cos(controller.Stat.AngularOfView * Mathf.Deg2Rad))
        {
            return true;
        }

        if (Physics.Linecast(transform.position, controller.Target.position + Vector3.up, controller.Stat.Obstacle))
        {

            return true;
        }
        Debug.Log("lo vede");
        return false;
    }
}
