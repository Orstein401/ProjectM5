using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;

    [Header("Parametres Rock ")]
    [SerializeField] private float travelTime;
    [SerializeField] private float height;
    [SerializeField] private float radius;

    private float timer;

    private Vector3 start;
    private Vector3 end;
    private void OnCollisionEnter(Collision collision)
    {
        DetectEnemy();
        Destroy(gameObject);
    }
    private void DetectEnemy()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, radius, enemyLayer);
        foreach (Collider enemy in enemies)
        {
            if (enemy.TryGetComponent<StateController>(out StateController enem))
            {
                //offset per evitare che collidano i nemici
                Vector3 randomOffset = Random.insideUnitSphere * 1.5f;
                randomOffset.y = 0;

                enem.TriggerPoint = transform.position+randomOffset;
                enem.IsTrigger = true;
            }

        }
    }
    private void Update()
    {
        FlyToPoint();
    }
    public void SetTrajectory(Vector3 Start, Vector3 End)
    {
        start=Start; 
        end=End;
    }
    //Non Funziona perfettamente per via del fatto che può attraversare il pavimento e tornare su per poi colpirlo
    public void FlyToPoint()
    {
        timer += Time.deltaTime;
        float time = timer / travelTime;

        Vector3 position = Vector3.Lerp(start, end, time);
        position.y += Mathf.Sin(time * Mathf.PI) * height;

        transform.position = position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
