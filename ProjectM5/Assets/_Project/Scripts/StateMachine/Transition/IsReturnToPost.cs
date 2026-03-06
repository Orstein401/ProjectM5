using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsReturnToPost : StateTransition
{
    public override bool IsConditionMeet()
    {
        return (!controller.EnemyAgent.pathPending && controller.EnemyAgent.remainingDistance <= controller.EnemyAgent.stoppingDistance);
    }

}
