using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelButton : MonoBehaviour {
	[SerializeField] ControlPanelManager controlPanel;

	[SerializeField] int buttonValue;

	public void ClickButton() {
		if (controlPanel.enabled) {
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/PlatformX/Keypad", gameObject);
			controlPanel.ClickButton(buttonValue);
		}
	}
}
