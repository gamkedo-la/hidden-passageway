using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCartWiggle : MonoBehaviour {
    Quaternion initialRot;
    float phaseShift1;
    float phaseShift2;
	// Use this for initialization
	void Start () {
        initialRot = transform.rotation;
        phaseShift1 = Random.Range(0.0f, 200.0f);
        phaseShift2= Random.Range(0.0f, 200.0f);
	}
	
	// Update is called once per frame
	void Update () {
        float pivot1 = Mathf.Cos(Time.timeSinceLevelLoad*0.3f+phaseShift1) * 13.0f;
        float pivot2 = Mathf.Cos(Time.timeSinceLevelLoad*0.7f+phaseShift2) * 7.0f;
        transform.rotation = initialRot *
            Quaternion.AngleAxis(pivot1, Vector3.right) *
                              Quaternion.AngleAxis(pivot2, Vector3.up);
	}
}
