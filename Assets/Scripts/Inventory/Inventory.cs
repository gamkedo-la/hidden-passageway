using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    #region Singleton
    public static Inventory instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory detected!");
        }
        instance = this;
    }
    #endregion

    public List<Item> items = new List<Item>();
    public int space = 9;

    public delegate void OnItemChange();
    public OnItemChange onItemChangeCallback;

    public bool AddItem(Item item)
    {
        if(items.Count >= space)
        {
            Debug.Log("No more space in Inventory");
            return false;
        }
        items.Add(item);

        if(onItemChangeCallback != null)
        {
            onItemChangeCallback.Invoke();
        }
        return true;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        
        if(onItemChangeCallback != null)
        {
            onItemChangeCallback.Invoke();
        }
    }
}
