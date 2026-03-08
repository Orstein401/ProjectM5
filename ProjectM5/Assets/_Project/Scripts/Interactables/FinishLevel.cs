using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : Interactable
{
    [SerializeField] private SO_Item requiredItem;
    [SerializeField] private Canvas failCanvas;
    [SerializeField] private Canvas winCanvas;
    protected override void Interact()
    {
        if (Invetory.Instance.HasItem(requiredItem))
        {
            Invetory.Instance.RemoveItem(requiredItem);
            winCanvas.enabled = true;
            Time.timeScale = 0f;
        }
        else
        {
            interactCanvas.enabled = false;
            failCanvas.enabled = true;
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        if (failCanvas.enabled)
        {
            failCanvas.enabled = false;
            return;
        }
        base.OnTriggerExit(other);
    }
}
