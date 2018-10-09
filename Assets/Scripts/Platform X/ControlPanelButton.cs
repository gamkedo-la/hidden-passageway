using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelButton : MonoBehaviour {
	[SerializeField] ControlPanelManager controlPanel;

	[SerializeField] int buttonValue;

	public void ClickButton() {
		if (controlPanel.enabled) {
			controlPanel.ClickButton(buttonValue);
		}
	}
}
