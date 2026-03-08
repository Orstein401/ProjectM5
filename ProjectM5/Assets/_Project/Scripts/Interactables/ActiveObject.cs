using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObject : Interactable
{
    [SerializeField] private GameObject obj;
    [SerializeField] private BoxCollider trigger;
    [SerializeField] private SO_Item requiredItem;

    protected override void Interact()
    {
        if (Invetory.Instance.HasItem(requiredItem))
        {
            trigger.enabled = true;
            Invetory.Instance.RemoveItem(requiredItem);
            obj.SetActive(true);
          
        }
    }
}
