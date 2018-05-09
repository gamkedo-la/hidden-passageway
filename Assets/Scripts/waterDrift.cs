using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterDrift : MonoBehaviour {
	Vector3 startPos;
	Quaternion startRot;
	// Use this for initialization
	void Start () {
		startPos = transform.position;
		startRot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = startPos + Mathf.Cos(Time.time*0.15f)*0.15f * Vector3.up;
		transform.rotation = startRot *
			Quaternion.AngleAxis(Mathf.Cos(Time.time*0.34f)*1.0f,Vector3.right)*
			Quaternion.AngleAxis(Mathf.Cos(Time.time*0.2f)*1.0f,Vector3.forward);
	}
}
