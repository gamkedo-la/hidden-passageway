using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockTime : MonoBehaviour {
    public Transform hourHand;
    public Transform minHand;
    public float targetTime = 0.0f;
    private float minutesNow=0.0f;
	// Use this for initialization
	void Start () {
        minutesNow = targetTime;
	}

	private void FixedUpdate()
	{
        float kVal = 0.9f;
        minutesNow = targetTime * (1.0f-kVal) + minutesNow * kVal;
	}

	// Update is called once per frame
	void Update () {
        // Debug.Log(minutesNow);
        float offsetForZeroDegAtTop = -90.0f;
        minHand.rotation = Quaternion.AngleAxis(minutesNow * 360.0f / 60.0f
                                                +offsetForZeroDegAtTop, Vector3.forward);
        float hourNow = minutesNow / 60.0f;
        hourHand.rotation = Quaternion.AngleAxis(hourNow * 360.0f / (12.0f)
                                                 +offsetForZeroDegAtTop, Vector3.forward);
	}
}
