using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroling : EnemyParent
{
    private void Update()
    {
        ConeVisual();
        DrawConeOfViewQuaterion(subdivision);
    }
}
