using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatLongGlobeChange : MonoBehaviour {
    public LatLongGlobe whichGlobe;
    public float degChange = 15.0f;
    public bool isForLatNotLong = false;

	private void Start()
	{
        if (whichGlobe == null)
        {
            Debug.LogError("Warning: missing clock for " + gameObject.name);
        }
	}
	public void triggerAction() {
        if (isForLatNotLong)
        {
            whichGlobe.targetLat += degChange;
            whichGlobe.targetLat = Mathf.Round(whichGlobe.targetLat / 5.0f) * 5.0f;
            whichGlobe.targetLat = Mathf.Clamp(whichGlobe.targetLat, -90.0f, 90.0f);
        }
        else
        {
            whichGlobe.targetLong += degChange;
            whichGlobe.targetLong = Mathf.Round(whichGlobe.targetLong / 5.0f) * 5.0f;
        }
	}
}
