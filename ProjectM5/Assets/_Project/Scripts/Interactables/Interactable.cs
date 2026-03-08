using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected Canvas interactCanvas;
    private bool isNearPlayer;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactCanvas.enabled = true;
            isNearPlayer = true;
        }
    }

    private void Update()
    {
        if (!isNearPlayer)return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactCanvas.enabled = false;
            isNearPlayer = false;
        }
    }

    protected abstract void Interact();
}
