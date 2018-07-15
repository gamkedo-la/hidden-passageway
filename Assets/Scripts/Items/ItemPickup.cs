using UnityEngine;

public class ItemPickup : MonoBehaviour {
    public Item item;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 1.5f))
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

    }
}
