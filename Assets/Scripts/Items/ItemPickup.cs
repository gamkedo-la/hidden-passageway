using UnityEngine;

public class ItemPickup : MonoBehaviour {
    public Item item;
    public GameObject parentOfGOToBeDestroyed;

    [FMODUnity.EventRef]
    public string itemPickupSound;
    FMOD.Studio.EventInstance ItemPickupAudio;
    Camera mainCam;

    void Start()
    {
        GameObject camGO = GameObject.Find("TransitionCam");
        mainCam = camGO.GetComponent<Camera>();

        if(!string.IsNullOrEmpty(itemPickupSound))
        {
            ItemPickupAudio = FMODUnity.RuntimeManager.CreateInstance(itemPickupSound);
        }
        else
        {
            itemPickupSound = @"event:/DecayingWorld/ItemPickup";
        }
        
        ItemPickupAudio = FMODUnity.RuntimeManager.CreateInstance(itemPickupSound);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, 2.5f))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    Pickup();
                    FMODUnity.RuntimeManager.PlayOneShotAttached(itemPickupSound, gameObject);
                }
            }
        }
    }

    public void Pickup()
    {
        bool wasItemPickedUp = Inventory.instance.AddItem(item);

        if(wasItemPickedUp)
        {
            Destroy(parentOfGOToBeDestroyed);
        }
        
    }
}
