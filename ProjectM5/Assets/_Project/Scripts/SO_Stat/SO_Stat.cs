using System;
using UnityEngine;

[Serializable]
public class SO_Stat : ScriptableObject
{
    [Header("Settings of Moviment")]
    [SerializeField] private float speed;

    public float Speed {  get =>speed; }
}
