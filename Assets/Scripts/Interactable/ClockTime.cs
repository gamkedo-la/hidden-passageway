using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockTime : MonoBehaviour {
    public Transform hourHand;
    public Transform minHand;
    private float minutesNow=0.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        minutesNow += Time.deltaTime * 20.0f;
        // Debug.Log(minutesNow);
        float offsetForZeroDegAtTop = -90.0f;
        minHand.rotation = Quaternion.AngleAxis(minutesNow * 360.0f / 60.0f
                                                +offsetForZeroDegAtTop, Vector3.forward);
        float hourNow = minutesNow / 60.0f;
        hourHand.rotation = Quaternion.AngleAxis(hourNow * 360.0f / (12.0f)
                                                 +offsetForZeroDegAtTop, Vector3.forward);
	}
}
