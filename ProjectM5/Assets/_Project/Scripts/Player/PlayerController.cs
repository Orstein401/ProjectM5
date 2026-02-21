using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent playerAgent;
    private Camera cam;

    private void Awake()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerAgent.SetDestination(GetDestination());
        }
    }
    private Vector3 GetDestination()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hitinfo);
        Vector3 destination = hitinfo.point;
        return destination;
    }
}
