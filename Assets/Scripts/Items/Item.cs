using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject {
    new public string name = "";
    public Sprite icon = null;
    
    public virtual void Use()
    {
        //For using power sources and other game vital objects. For now, just a Log.
        Debug.Log("Using " + name);
    }
}
