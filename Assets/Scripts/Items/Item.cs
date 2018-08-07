using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject {
    new public string name = "";
    public Sprite icon = null;
    public GameObject prefabToSpawn;

    public virtual bool Use()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3.5f))
        {
            switch(hit.collider.name)
            {
                case "mini_power_source_dark_red_slot":
                    ExcavateRobotPart hand = hit.collider.GetComponent<ExcavateRobotPart>();
                    hand.isPowerSourceInSlot = true;
                    return true;
            }
        }
        return false;
    }

    public virtual void RemoveItem()
    {
        Instantiate(prefabToSpawn);
    }
}
