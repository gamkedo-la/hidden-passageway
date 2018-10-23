using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLights : AbstractActivateable {

	[SerializeField] LightsPuzzleManager lightsPuzzleManager;
	[SerializeField] bool winState;
	[SerializeField] Light[] lightsToToggle;
    [SerializeField] GameObject[] lightGOsToToggle;

	bool currentState = false;

	TriggerComponentEnable triggerScript;

	// string mySaveName;

	void Awake()
	{
		// mySaveName = PlayerPrefsHelper.GetPrefsName(gameObject);
        int previousState = 0;//PlayerPrefs.GetInt(mySaveName, 0);
		if (previousState == 1) {
			DoToggleLights();
		}

		GameObject[] interactionSwitches = GameObject.FindGameObjectsWithTag(Tags.InteractionSwitch);
		for (int i = 0; i < interactionSwitches.Length; i++)
		{
			TriggerComponentEnable tceScript = interactionSwitches[i].GetComponent<TriggerComponentEnable>();
			// Skip this object if it has no script
			if (tceScript == null)
			{
				continue;
			}
			if (tceScript.toEnable == this || tceScript.toEnable2 == this || tceScript.toEnable3 == this)
			{
				triggerScript = tceScript;
			}
		}
	}

	public bool HasWon ()
	{
		// Debug.Log("has won?" + mySaveName + " = " + (currentState?"1":"0") + "=="+(winState?"1":"0"));
		return currentState == winState;
	}

	public void Disable ()
	{
		if (triggerScript) {
			triggerScript.enabled = false;
			enabled = false;
		}
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
		/*foreach (Light light in lightsToToggle) {
			light.enabled = !light.enabled;
		}*/
        foreach (GameObject lightGO in lightGOsToToggle)
        {
            lightGO.SetActive(lightGO.activeSelf == false);
        }


		currentState = !currentState;

		// PlayerPrefs.SetInt(mySaveName, currentState ? 1 : 0);

		lightsPuzzleManager.CheckWinState();
	}
}
