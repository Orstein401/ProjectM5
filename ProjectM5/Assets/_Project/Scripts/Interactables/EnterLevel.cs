using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterLevel : Interactable
{
    public enum Level
    {
        Hospital, SuperMarket, Street
    }
    [SerializeField] private Level typeLevel;

    protected override void Interact()
    {
        switch (typeLevel)
        {
            case Level.Hospital:
                SceneManager.LoadScene(2);
                break;

            case Level.SuperMarket:
                SceneManager.LoadScene(3);
                break;

            case Level.Street:
                SceneManager.LoadScene(1);
                break;
        }
    }
}
