using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : StateTransition
{
    [SerializeField] private bool mustBeTrigger;
    public override bool IsConditionMeet()
    {
        if (mustBeTrigger)
        {
            return controller.IsTrigger;
        }
        return !controller.IsTrigger;

    }

   
}
