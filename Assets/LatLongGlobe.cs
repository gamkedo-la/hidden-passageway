using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatLongGlobe : MonoBehaviour {
    public float targetLat = 0.0f;
    public float targetLong = 0.0f;
    private float latNow = 0.0f;
    private float longNow = 0.0f;
    // Use this for initialization
    void Start()
    {
        latNow = targetLat;
        longNow = targetLong;
    }

    private void FixedUpdate()
    {
        float kVal = 0.9f;
        latNow = targetLat * (1.0f - kVal) + targetLat * kVal;
        longNow = targetLong * (1.0f - kVal) + targetLong * kVal;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(minutesNow);
        // float offsetForZeroDegAtTop = -90.0f;
        transform.rotation = Quaternion.AngleAxis(latNow
                                                  /*+ offsetForZeroDegAtTop*/, Vector3.right)
            * Quaternion.AngleAxis(longNow
                                   /*+ offsetForZeroDegAtTop*/, Vector3.up);
    }
}
