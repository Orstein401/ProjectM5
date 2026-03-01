using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent playerAgent;
    private Camera cam;

    private bool isWalking;
    private bool isRunning;

    private AnimationScript anim;

    public NavMeshAgent PlayerAgent {  get { return playerAgent; } }
    private void Awake()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        anim= GetComponentInChildren<AnimationScript>();
        cam = Camera.main;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerAgent.SetDestination(GetDestination());
            isWalking = true;
            anim.ChangeAnimation(isWalking, isRunning);

        }
        if (!playerAgent.pathPending && playerAgent.remainingDistance<=playerAgent.stoppingDistance)
        {
            isWalking = false;
            anim.ChangeAnimation(isWalking, isRunning);
        }
    }
    private Vector3 GetDestination()
    {
        Vector3 destination;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hitinfo);
        if(hitinfo.collider != null)
        {
            destination = hitinfo.point;
        }
        else
        {
            destination = transform.position; //gli do come sua destinazione il suo punto attuale, in modo tale che non venga restituito il punto 0 del mondo
        }
        return destination;
    }
}
