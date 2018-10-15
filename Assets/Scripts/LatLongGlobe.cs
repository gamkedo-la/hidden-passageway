using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LatLongGlobe : MonoBehaviour {
    public float targetLat = 0.0f;
    public float targetLong = 5.0f;
    public float offset = 0.0f;
    public int UTCNow = 0;
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
        float offsetForZeroDegAtPrime = 180.0f+offset;
        transform.rotation = Quaternion.AngleAxis(latNow, Vector3.right)
            * Quaternion.AngleAxis(longNow
                                   + offsetForZeroDegAtPrime, Vector3.up);
        if (UTCNow == -13)
        {
            UTCNow = 12;
        } else if (UTCNow == 13)
        {
            UTCNow = -12;
        }
        Debug.Log(UTCNow);
        string EWlong = (UTCNow < 0 ? "-" : "+");
        int numToShow = Mathf.Abs(UTCNow);
        textOut.text = "UTC" + EWlong + numToShow; //// 180/15 is 12
    }
}
