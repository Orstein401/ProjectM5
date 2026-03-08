using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Invetory/Item")]
public class SO_Item : ScriptableObject
{
    [SerializeField] private string nameItem;
    [SerializeField] private int id;


    public string Name {  get { return nameItem; } }
    public int Id { get { return id; } }
}
