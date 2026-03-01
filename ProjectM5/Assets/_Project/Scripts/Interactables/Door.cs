using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    private Animator animator;
    [SerializeField] protected string nameValue;

     private bool active;
    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }
    protected override void Interact()
    {
        active = !active;
        animator.SetBool(nameValue, active);
    }
}
