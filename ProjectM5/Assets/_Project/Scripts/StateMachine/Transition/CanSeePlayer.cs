using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSeePlayer : StateTransition
{
    [SerializeField] private bool canSeePlayerRequired;
    public override bool IsConditionMeet()
    {
        Vector3 toTarget = controller.Target.position - transform.position;
        float sqrDistance = toTarget.sqrMagnitude;

        if (sqrDistance > controller.Stat.SightDistance * controller.Stat.SightDistance)
        {
            return !canSeePlayerRequired;
        }

        float distance = Mathf.Sqrt(sqrDistance);
        toTarget /= distance;

        if (Vector3.Dot(transform.forward, toTarget) < Mathf.Cos(controller.Stat.AngularOfView * Mathf.Deg2Rad))
        {
            return !canSeePlayerRequired;
        }

        if (Physics.Linecast(transform.position, controller.Target.position + Vector3.up, controller.Stat.Obstacle))
        {

            return !canSeePlayerRequired;
        }
        return canSeePlayerRequired;
    }
}
