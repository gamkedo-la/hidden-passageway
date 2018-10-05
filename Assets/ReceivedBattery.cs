using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceivedBattery : MonoBehaviour {
    public Transform dest;
    public GameObject light;
    private bool isHeld = false;
    private Transform battery;
    PickUpCell puc;
    public void Update () {
        if (isHeld) {
            battery.position = dest.position;
            battery.rotation = dest.rotation;
            light.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        puc = other.GetComponent<PickUpCell>();
        if (puc){
            battery = puc.transform;
            puc.enabled = false;
            isHeld = true;
            Debug.Log("battery received");  
        }

    }
}
