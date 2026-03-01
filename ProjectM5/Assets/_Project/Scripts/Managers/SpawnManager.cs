using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    // preferirei farlo con i json, ma visto che non posso sono costretto a usare il potere oscuro dei singleton
    private Vector3 spawnPoint;
    public Vector3 SpawnPoint {  get { return spawnPoint; } }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

}
