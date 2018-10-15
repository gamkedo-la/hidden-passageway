using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatLongGlobeChange : MonoBehaviour {
    public LatLongGlobe whichGlobe;
    public int utcChange = 1;
    public bool isForLatNotLong = false;

	private void Start()
	{
        if (whichGlobe == null)
        {
            Debug.LogError("Warning: missing globe for " + gameObject.name);
        }
	}
	public void triggerAction() {
        if (isForLatNotLong)
        {
            whichGlobe.UTCNow += utcChange;
            whichGlobe.targetLong += 14.4f * utcChange;
        }
        else
        {
            whichGlobe.UTCNow+= utcChange;
            whichGlobe.targetLong += 14.4f * utcChange;
        }
	}
}
