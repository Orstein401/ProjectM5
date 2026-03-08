using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : Interactable
{
    [SerializeField] private SO_Item item;
    protected override void Interact()
    {
        Invetory.Instance.AddItem(item);
        interactCanvas.enabled = false;
        Destroy(gameObject);
    }

  
}
