using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyParent : MonoBehaviour
{
    [Header("Componets")]
    private NavMeshAgent enmyAgent;
    private LineRenderer lineRend;

    [Header("Stat for Visual Cone")]
    [SerializeField] private Transform target;
    [SerializeField] private float angularOfView;
    [SerializeField] private float sightDistance;
    [SerializeField] protected int subdivision;
    [SerializeField] private LayerMask obstacle;

    [Header("State")]
    private STATE state;

    private void Awake()
    {
        enmyAgent = GetComponent<NavMeshAgent>();
        lineRend = GetComponent<LineRenderer>();
    }

    protected bool ConeVisual()
    {
        Vector3 toTarget = target.position - transform.position;
        float sqrDistance = toTarget.sqrMagnitude;

        if (sqrDistance > sightDistance * sightDistance)
        {
            return false;
        }

        float distance = Mathf.Sqrt(sqrDistance);
        toTarget /= distance;

        //if (Vector3.Dot(transform.forward, toTarget) < Mathf.Cos(angularOfView * Mathf.Deg2Rad)) //Vector3.Angle(a,b)
        //{
        //    return false;
        //}
        if (Vector3.Dot(transform.forward, toTarget) < Vector3.Angle(transform.forward,target.position)) //Vector3.Angle(a,b)
        {
            Debug.Log("sta fuopri dagli angoli");
            return false;
        }
        if (Physics.Linecast(transform.position, target.position, obstacle))
        {

            return false;
        }

        return true;

    }


    public void DrawConeOfViewQuaterion(int subdivisions)
    {
        lineRend.positionCount = subdivisions + 1;

        float startAngle = -angularOfView;

        Vector3 originLine = transform.position;
        Vector3 rayCastOrigin = transform.position + new Vector3(0f, 0.1f, 0f);
        Vector3 forward = transform.forward;

        lineRend.SetPosition(0, originLine);

        float deltaAngle = (2 * angularOfView / subdivisions);

        for (int i = 0; i < subdivisions; i++)
        {
            float currentAngle = startAngle + deltaAngle * i;
            Vector3 dir = Quaternion.Euler(0, currentAngle, 0) * forward;
            Vector3 point = originLine + dir * sightDistance;

            if (Physics.Raycast(rayCastOrigin, dir, out var hitInfo, sightDistance, obstacle))
            {
                point = hitInfo.point - (rayCastOrigin - originLine);
            }
            lineRend.SetPosition(i + 1, point);
        }

    }
}
