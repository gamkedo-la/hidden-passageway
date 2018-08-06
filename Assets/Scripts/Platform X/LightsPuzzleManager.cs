using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsPuzzleManager : MonoBehaviour {

	[SerializeField] AbstractActivateable toEnable;
	[SerializeField] AbstractActivateable toEnable2;

	[SerializeField] ToggleLights[] lightToggles;

	// @todo awake => load prev state

	public void CheckWinState()
	{
		bool hasWon = true;
		foreach (ToggleLights lightToggle in lightToggles) {
			//Debug.Log("won ? " + lightToggle.gameObject.name + (lightToggle.HasWon() ? "=1" : "=0"));
			hasWon = hasWon && lightToggle.HasWon();
		}

		Debug.Log("Win state:" + (hasWon ? "=1" : "=0"));

		if (hasWon) {
			toEnable.SendMessage("Activate");
			toEnable2.SendMessage("Activate");
			// @todo disable all light switches
		}
	}
}
