using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawConeVisual : MonoBehaviour
{
    private LineRenderer lineRend;
    private StateController stateController;

    [SerializeField] private int sides;
    private void Awake()
    {
        stateController = GetComponent<StateController>();
        lineRend=GetComponent<LineRenderer>();
    }
    private void Update()
    {
        DrawConeOfViewQuaterion(sides);
    }
    public void DrawConeOfViewQuaterion(int subdivisions)
    {
        lineRend.positionCount = subdivisions + 1;

        float startAngle = -stateController.Stat.AngularOfView;

        Vector3 originLine = transform.position;
        Vector3 rayCastOrigin = transform.position + new Vector3(0f, 0.1f, 0f);
        Vector3 forward = transform.forward;

        lineRend.SetPosition(0, originLine);

        float deltaAngle = (2 * stateController.Stat.AngularOfView / subdivisions);

        for (int i = 0; i < subdivisions; i++)
        {
            float currentAngle = startAngle + deltaAngle * i;
            Vector3 dir = Quaternion.Euler(0, currentAngle, 0) * forward;
            Vector3 point = originLine + dir * stateController.Stat.SightDistance;

            if (Physics.Raycast(rayCastOrigin, dir, out var hitInfo, stateController.Stat.SightDistance, stateController.Stat.Obstacle))
            {
                point = hitInfo.point - (rayCastOrigin - originLine);
            }
            lineRend.SetPosition(i + 1, point);
        }

    }
}
