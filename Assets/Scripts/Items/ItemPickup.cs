using UnityEngine;

public class ItemPickup : MonoBehaviour {
    public Item item;
    public GameObject parentOfGOToBeDestroyed;

    [FMODUnity.EventRef]
    public string itemPickupSound;
    FMOD.Studio.EventInstance ItemPickupAudio;

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
                    ItemPickupAudio = FMODUnity.RuntimeManager.CreateInstance(itemPickupSound);
                }
            }
        }
    }

    public void Pickup()
    {
        bool wasItemPickedUp = Inventory.instance.AddItem(item);

        if(wasItemPickedUp)
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(itemPickupSound, gameObject);
            Debug.Log("Picking up: " + item.name);
            Destroy(parentOfGOToBeDestroyed);
        }
        
    }
}
