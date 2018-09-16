using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject {
    new public string name = "";
    public Sprite icon = null;
    public GameObject prefabToSpawn;

    public virtual bool Use(string itemName)
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3.5f))
        {
            if(itemName == "Red Power Source" && hit.collider.name == "mini_power_source_dark_red_slot")
            {
                ExcavateRobotPart hand = hit.collider.GetComponent<ExcavateRobotPart>();
                if (hand != null)
                    hand.isPowerSourceInSlot = true;
                return true;
            }
            else if(itemName == "Blue Power Source" && hit.collider.name == "mini_power_source_blue_slot")
            {
                MakeThisLightDimAndBrightenOverTime light = hit.collider.GetComponent<MakeThisLightDimAndBrightenOverTime>();
                if (light != null)
                    light.enabled = true;
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
