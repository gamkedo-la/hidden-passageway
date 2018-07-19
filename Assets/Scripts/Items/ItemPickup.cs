using UnityEngine;

public class ItemPickup : MonoBehaviour {
    public Item item;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2.5f))
            {
                if(item != null)
                {
                    Pickup();
                }
            }
        }
    }
    void Pickup()
    {
        bool wasItemPickedUp = Inventory.instance.AddItem(item);

        if(wasItemPickedUp)
        {
            Debug.Log("Picking up: " + item.name);
            Destroy(gameObject);
        }
        
    }
}
