using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsPuzzleManager : MonoBehaviour {

	[SerializeField] AbstractActivateable toEnable;
	[SerializeField] AbstractActivateable toEnable2;

	[SerializeField] ToggleLights[] lightToggles;

	bool hasWon = false;

	void Awake()
	{
		string mySaveName = PlayerPrefsHelper.GetPrefsName(gameObject);
        hasWon = (PlayerPrefs.GetInt(mySaveName, 0) == 1);
		if (hasWon) {
			DisablePuzzle();
		}
	}

	public void CheckWinState()
	{
		if (hasWon) {
			return;
		}

		hasWon = true;
		foreach (ToggleLights lightToggle in lightToggles) {
			hasWon = hasWon && lightToggle.HasWon();
		}

		// Debug.Log("Win state:" + (hasWon ? "=1" : "=0"));

		if (hasWon) {
			toEnable.SendMessage("Activate");
			toEnable2.SendMessage("Activate");

			DisablePuzzle();
		}
	}

	void DisablePuzzle()
	{
		foreach (ToggleLights lightToggle in lightToggles) {
			lightToggle.SendMessage("Disable");
		}
	}
}
