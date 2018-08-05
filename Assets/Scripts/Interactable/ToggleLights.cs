using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLights : AbstractActivateable {

	[SerializeField] Light[] lightsToToggle;

	string mySaveName;

	void Awake()
	{
		mySaveName = PlayerPrefsHelper.GetPrefsName(gameObject);
        int previousState = PlayerPrefs.GetInt(mySaveName, 0);
		if (previousState == 1) {
			DoToggleLights();
		}
	}
	public override void Activate () {
		isDone = true;
		PlayerPrefs.SetInt(mySaveName, 1);
		DoToggleLights();
	}

	public override void Reverse () {
		isDone = false;
		PlayerPrefs.SetInt(mySaveName, 0);
		DoToggleLights();
	}

	void DoToggleLights() {
		foreach (Light light in lightsToToggle) {
			light.enabled = !light.enabled;
		}
	}
}
