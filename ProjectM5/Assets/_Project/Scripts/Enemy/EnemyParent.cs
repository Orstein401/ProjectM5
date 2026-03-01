using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyParent : MonoBehaviour
{
    [Header("Componets")]
    protected NavMeshAgent enemyAgent;
    protected LineRenderer lineRend;
    protected AnimationScript anim;

    [Header("Stat for Visual Cone")]
    [SerializeField] protected Transform target;
    [SerializeField] protected SO_StatEnemy stat;

    [Header("State")]
    protected STATE currentState;

    [Header("Parametres Interval")]
    [SerializeField] protected float interval;
    protected float lastUpdateChase;

    protected void Awake()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        lineRend = GetComponent<LineRenderer>();
        anim = GetComponentInChildren<AnimationScript>();
        enemyAgent.speed = stat.Speed;
    }
    protected abstract void StateMachine();

    protected virtual void ChasingTarget()
    {
        if (target != null && Time.time - lastUpdateChase >= stat.ChaseUpdateInterval)
        {
            enemyAgent.SetDestination(target.position);
            lastUpdateChase = Time.time;
        }
    }

    protected bool ConeVisual()
    {
        Vector3 toTarget = target.position - transform.position;
        float sqrDistance = toTarget.sqrMagnitude;

        if (sqrDistance > stat.SightDistance * stat.SightDistance)
        {
            return false;
        }

        float distance = Mathf.Sqrt(sqrDistance);
        toTarget /= distance;

        if (Vector3.Dot(transform.forward, toTarget) < Mathf.Cos(stat.AngularOfView * Mathf.Deg2Rad))
        {
            return false;
        }

        if (Physics.Linecast(transform.position, target.position+Vector3.up, stat.Obstacle))
        {

            return false;
        }

        return true;

    }

    public void DrawConeOfViewQuaterion(int subdivisions)
    {
        lineRend.positionCount = subdivisions + 1;

        float startAngle = -stat.AngularOfView;

        Vector3 originLine = transform.position;
        Vector3 rayCastOrigin = transform.position + new Vector3(0f, 0.1f, 0f);
        Vector3 forward = transform.forward;

        lineRend.SetPosition(0, originLine);

        float deltaAngle = (2 * stat.AngularOfView / subdivisions);

        for (int i = 0; i < subdivisions; i++)
        {
            float currentAngle = startAngle + deltaAngle * i;
            Vector3 dir = Quaternion.Euler(0, currentAngle, 0) * forward;
            Vector3 point = originLine + dir * stat.SightDistance;

            if (Physics.Raycast(rayCastOrigin, dir, out var hitInfo, stat.SightDistance, stat.Obstacle))
            {
                point = hitInfo.point - (rayCastOrigin - originLine);
            }
            lineRend.SetPosition(i + 1, point);
        }

    }
}
