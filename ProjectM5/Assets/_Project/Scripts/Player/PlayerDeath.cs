using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private int numLife;

    private PlayerController controller;
    [SerializeField] private float timerRespawn;

    private Coroutine deathRoutine;

    private AnimationScript anim;
    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        anim = GetComponentInChildren<AnimationScript>();
    }
    private void Start()
    {
        SpawnManager.Instance.SetSpawnPoint(transform.position);
    }
    private void DeathPlayer()
    {
        if (numLife < 1) 
        {
            int indexScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(indexScene);
            return;
        }
        if (deathRoutine == null)
        {
            numLife -= 1;
            deathRoutine = StartCoroutine(DeathRoutine());
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyParent>(out var enemy))
        {
            DeathPlayer();
        }
    }

    private IEnumerator DeathRoutine()
    {

        controller.enabled = false;
        anim.ChangeAnimation(false, false,true);
        yield return new WaitForSeconds(timerRespawn);
        controller.enabled = true;
        transform.position = SpawnManager.Instance.SpawnPoint;
        controller.PlayerAgent.SetDestination(transform.position); // lo faccio per resetare la destinazione, altrimenti riparte verso l'ultimo punto cliccato prima della morte
        anim.ChangeAnimation(true, false, false);
        deathRoutine = null;
    }

}
