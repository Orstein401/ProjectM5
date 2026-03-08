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
    [SerializeField] private Rock rockPrefab;

    [SerializeField] private float throwCooldown = 4f;
    private float nextThrowTime;

    private Vector3 spawnPoint;
    public Vector3 SpawnPoint { get => spawnPoint; }
    public NavMeshAgent PlayerAgent { get { return playerAgent; } }

    private void Awake()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<AnimationScript>();
        cam = Camera.main;
        spawnPoint = transform.position;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerAgent.SetDestination(GetDestination());
            isWalking = true;
            anim.ChangeAnimation(isWalking, isRunning);
        }
        if (Input.GetMouseButtonDown(1) && Time.time >= nextThrowTime)
        {
            nextThrowTime = Time.time + throwCooldown;
            ThrowRock();
        }
        if (!playerAgent.pathPending && playerAgent.remainingDistance <= playerAgent.stoppingDistance)
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
        if (hitinfo.collider != null)
        {
            destination = hitinfo.point;
        }
        else
        {
            destination = transform.position; //gli do come sua destinazione il suo punto attuale, in modo tale che non venga restituito il punto 0 del mondo
        }
        return destination;
    }

    private void ThrowRock()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        Rock rock = Instantiate(rockPrefab);
        rock.transform.position = transform.position + Vector3.up;

        if (Physics.Raycast(ray, out RaycastHit hitinfo))
        {
            rock.SetTrajectory(rock.transform.position, hitinfo.point);
        }

    }
}
