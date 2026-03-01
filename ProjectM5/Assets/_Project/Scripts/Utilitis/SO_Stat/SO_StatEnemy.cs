using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Statistics/Enemy")]
public class SO_StatEnemy : SO_Stat
{
    [Header("Vision Settings")]
    [SerializeField] private float angularOfView;
    [SerializeField] private float sightDistance;
    [SerializeField] private int subdivision;
    [SerializeField] private LayerMask obstacle;

    [Header("Chase Settings")]
    [SerializeField] private float chaseUpdateInterval;

    //Getter
    public float AngularOfView { get => angularOfView; }
    public float SightDistance { get => sightDistance; }
    public int Subdivision { get => subdivision; }

    public LayerMask Obstacle { get => obstacle; }

    public float ChaseUpdateInterval { get => chaseUpdateInterval; }
}
