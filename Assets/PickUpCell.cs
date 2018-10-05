using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCell : MonoBehaviour {
    public Transform carrySpot;
    public bool isCarrying = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (isCarrying) {
            transform.position = carrySpot.position;
            transform.rotation = carrySpot.rotation;  
        }

	}
}
