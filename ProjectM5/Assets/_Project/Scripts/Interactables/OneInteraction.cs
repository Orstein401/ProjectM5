using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneInteraction : Interactable
{
    private Animator animator;
    [SerializeField] protected string nameValue;

    private bool active;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    protected override void Interact()
    {
        active=true;
        animator.SetBool(nameValue, active);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (!active) // faccio si che se è già stata attivata l'interazione non debba ricapitare
        {
            base.OnTriggerEnter(other);
        }
    }
}
