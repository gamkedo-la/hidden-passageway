using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelsPuzzleManager : MonoBehaviour {

	[SerializeField] AbstractActivateable toEnable;
	[SerializeField] AbstractActivateable toEnable2;

	[SerializeField] ControlPanelManager[] controlPanels;

	bool hasWon = false;

	public void CheckWinState()
	{
		if (hasWon) {
			return;
		}

		hasWon = true;
		foreach (ControlPanelManager controlPanel in controlPanels) {
			hasWon = hasWon && controlPanel.HasWon();
		}

		// Debug.Log("Win state:" + (hasWon ? "=1" : "=0"));

		if (hasWon) {
			toEnable.SendMessage("Activate");
			if (toEnable2 != null) {
				toEnable2.SendMessage("Activate");
			}

			DisablePuzzle();
		}
	}

	void DisablePuzzle()
	{
		foreach (ControlPanelManager controlPanel in controlPanels) {
			controlPanel.SendMessage("Disable");
		}
	}
}
