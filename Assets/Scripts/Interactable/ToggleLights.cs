using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLights : AbstractActivateable {

	[SerializeField] LightsPuzzleManager lightsPuzzleManager;
	[SerializeField] bool winState;
	[SerializeField] Light[] lightsToToggle;

	[SerializeField] bool currentState = true;

	string mySaveName;

	void Awake()
	{
		mySaveName = PlayerPrefsHelper.GetPrefsName(gameObject);
        int previousState = PlayerPrefs.GetInt(mySaveName, 0);
		if (previousState == 1) {
			DoToggleLights();
		}
	}

	public bool HasWon()
	{
		Debug.Log("has won?" + mySaveName + " = " + (currentState?"1":"0") + "=="+(winState?"1":"0"));
		return currentState == winState;
	}

	public override void Activate ()
	{
		isDone = true;
		DoToggleLights();

		if (callNext) {
			callNext.Activate();
		}
	}

	public override void Reverse ()
	{
		isDone = false;
		DoToggleLights();

		if (callPrev) {
			callPrev.Reverse();
		}
	}

	void DoToggleLights()
	{
		foreach (Light light in lightsToToggle) {
			light.enabled = !light.enabled;
		}

		currentState = !currentState;

		PlayerPrefs.SetInt(mySaveName, currentState ? 1 : 0);

		lightsPuzzleManager.CheckWinState();
	}
}
