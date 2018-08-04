using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLights : AbstractActivateable {

	[SerializeField] Light[] lightsToToggle;
	public override void Activate () {
		isDone = true;
		DoToggleLights();
	}

	public override void Reverse () {
		isDone = false;
		DoToggleLights();
	}

	void DoToggleLights() {
		foreach (Light light in lightsToToggle) {
			light.enabled = !light.enabled;
		}
	}
}
