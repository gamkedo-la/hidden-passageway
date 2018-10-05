using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceivedBattery : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        PickUpCell puc = other.GetComponent<PickUpCell>();
        if (puc){
            puc.enabled = false;
            Debug.Log("battery received");  
        }

    }
}
