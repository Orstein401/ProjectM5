using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyParent : MonoBehaviour
{
    [Header("Componets")]
    protected NavMeshAgent enemyAgent;
    protected LineRenderer lineRend;

    [Header("Stat for Visual Cone")]
    [SerializeField] protected Transform target;
    [SerializeField] protected float angularOfView;
    [SerializeField] protected float sightDistance;
    [SerializeField] protected int subdivision;
    [SerializeField] protected LayerMask obstacle;

    [Header("State")]
    protected STATE currentState;

    [Header("Parametres")]
    [SerializeField] protected float interval;
    [SerializeField] protected float chaseUpdateInterval;
    protected float lastUpdateChase;

    private void Awake()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        lineRend = GetComponent<LineRenderer>();
    }
    protected abstract void StateMachine();

    protected virtual void ChasingTarget()
    {
        if (target != null && Time.time - lastUpdateChase >= chaseUpdateInterval)
        {
            enemyAgent.SetDestination(target.position);
            lastUpdateChase = Time.time;
        }
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

        if (Vector3.Dot(transform.forward, toTarget) < Mathf.Cos(angularOfView * Mathf.Deg2Rad))
        {
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
