using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterDriftEnhanced : MonoBehaviour {
	Vector3 startPos;
	Quaternion startRot;
    public float MinorVal = 0.15f;
    public float LRcontrol = 0.34f;
    public float UDcontrol = 0.2f;
	// Use this for initialization
	void Start () {
		startPos = transform.localPosition;
		startRot = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = startPos + Mathf.Cos(Time.time*MinorVal)*MinorVal * Vector3.up;
		transform.localRotation = startRot *
			Quaternion.AngleAxis(Mathf.Cos(Time.time*LRcontrol)*1.0f,Vector3.right)*
			Quaternion.AngleAxis(Mathf.Cos(Time.time*UDcontrol)*1.0f,Vector3.forward);
	}
}
