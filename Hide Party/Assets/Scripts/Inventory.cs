using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
        }

        instance = this;
    }

    #endregion

    // Called whenever inventory is changed (adding or removing items)
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;

    public List<Item> items = new List<Item>();

    [SerializeField] Item jacket;

    private void Start()
    {
        Add(jacket);        // Adds the first quest's jacket to inventory at the start.
    }

    public bool Add (Item item)
    {
        if (items.Count >= space)
        {
            Debug.Log("Not enough room in inventory");
            return false;
        }

        items.Add(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
       

        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
