using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockChange : MonoBehaviour {
    public ClockTime whichClock;
    public float minChange = 15.0f;

	private void Start()
	{
        if (whichClock == null)
        {
            Debug.LogError("Warning: missing clock for " + gameObject.name);
        }
	}
	public void triggerAction() {
        whichClock.targetTime += minChange;
        // keeps it on even increments of 5 min.
        whichClock.targetTime = Mathf.Floor(whichClock.targetTime);
	}
}
