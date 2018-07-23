using UnityEngine;

public class ItemPickup : MonoBehaviour {
    public Item item;
    public GameObject parentOfGOToBeDestroyed;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2.5f))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    Pickup();
                }
            }
        }
    }

    public void Pickup()
    {
        bool wasItemPickedUp = Inventory.instance.AddItem(item);

        if(wasItemPickedUp)
        {
            Debug.Log("Picking up: " + item.name);
            Destroy(parentOfGOToBeDestroyed);
        }
        
    }
}
