using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayUpright : MonoBehaviour {
    Quaternion startRot;
	// Use this for initialization
	void Start () {
        startRot = transform.rotation;
	}
	
	void LateUpdate () {
        transform.rotation = startRot;
	}
}
