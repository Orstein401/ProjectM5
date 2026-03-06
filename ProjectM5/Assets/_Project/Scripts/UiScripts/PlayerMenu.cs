using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMenu : MonoBehaviour
{
    [SerializeField] private Canvas menuPlayer;
    [SerializeField] private PlayerController player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenu();
        }
    }
    private void OpenMenu()
    {
        menuPlayer.enabled = !menuPlayer.enabled;
        if (menuPlayer.enabled)
        {
            Time.timeScale = 0;
            return;
        }
        Time.timeScale = 1;
    }

    public void Respawn()
    {
        menuPlayer.enabled = false;
        Time.timeScale = 1;
        player.transform.position = player.SpawnPoint;
        player.PlayerAgent.SetDestination(player.transform.position); // lo faccio per resetare la destinazione, altrimenti riparte verso l'ultimo punto cliccato prima della morte
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        int indexScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(indexScene);
    }

    public void ReturnMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
