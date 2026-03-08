using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Invetory : MonoBehaviour
{
    public static Invetory Instance { get; private set; }
    [SerializeField] private List<SO_Item> items = new List<SO_Item>();

    private static bool isApplicationQuit = false;
    private void Awake()
    {
        if (Instance != null && Instance != this && !isApplicationQuit)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void AddItem(SO_Item item)
    {
        items.Add(item);
    }
    public bool HasItem(SO_Item item)
    {
        return items.Contains(item);
    }
    public void RemoveItem(SO_Item item)
    {
        items.Remove(item);
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    protected virtual void OnApplicationQuit()
    {
        isApplicationQuit = true;
    }

}
