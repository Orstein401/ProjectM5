using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private string[] nameValue;
    private void Awake()
    {
        animator=GetComponent<Animator>();
    }

    public void ChangeAnimation(bool firstPar, bool secondPar)
    {
        animator.SetBool(nameValue[0],firstPar);
        animator.SetBool(nameValue[1], secondPar);
    }

    public void ChangeAnimation(bool firstPar, bool secondPar, bool thirdPar)
    {
        animator.SetBool(nameValue[0], firstPar);
        animator.SetBool(nameValue[1], secondPar);
        animator.SetBool(nameValue[2], thirdPar);

    }
}
