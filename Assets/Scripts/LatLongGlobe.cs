using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LatLongGlobe : MonoBehaviour {
    public float targetLat = 0.0f;
    public float targetLong = 0.0f;
    private float latNow = 0.0f;
    private float longNow = 0.0f;

    public Text textOut;

    // Use this for initialization
    void Start()
    {
        latNow = targetLat;
        longNow = targetLong;
    }

    private void FixedUpdate()
    {
        float kVal = 0.9f;
        latNow = targetLat * (1.0f - kVal) + latNow * kVal;
        longNow = targetLong * (1.0f - kVal) + longNow * kVal;
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
        float longWrapped = Mathf.Repeat(longNow + 180.0f, 360.0f) - 180.0f;
        string EWlong = (longWrapped < 0.0f ? "W" : "E");
        string NSlat = (latNow < 0.0f ? "S" : "N");;
        textOut.text = " " + Mathf.Abs(latNow).ToString("N1") + "°"+NSlat+", "+ Mathf.Abs(longWrapped).ToString("N1") + "°"+EWlong;
    }
}
