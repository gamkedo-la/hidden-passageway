using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameRateToggleTester : MonoBehaviour {
	public GameObject[] listToToggle;
	public Text debugText;

	void keyToggleAndAnnounce(KeyCode whichKey, int elementIndex) {
		if(Input.GetKeyDown(whichKey) && listToToggle[elementIndex] != null) {
			listToToggle[elementIndex].SetActive(listToToggle[elementIndex].activeSelf == false);
			debugText.text = listToToggle[elementIndex].name + " is now " +
				(listToToggle[elementIndex].activeSelf ? "on" : "off");
			StartCoroutine(clearDebugText());
		}
	}

	IEnumerator clearDebugText () {
		float startTime = Time.time;
		while(Time.time < startTime + 1.0f) {
			yield return null;
		}

		debugText.text = "";
	}

	void Update () {
		keyToggleAndAnnounce(KeyCode.Alpha1, 0);
		keyToggleAndAnnounce(KeyCode.Alpha2, 1);
		keyToggleAndAnnounce(KeyCode.Alpha3, 2);
		keyToggleAndAnnounce(KeyCode.Alpha4, 3);
		keyToggleAndAnnounce(KeyCode.Alpha5, 4);
		keyToggleAndAnnounce(KeyCode.Alpha6, 5);
		keyToggleAndAnnounce(KeyCode.Alpha7, 6);
		keyToggleAndAnnounce(KeyCode.Alpha8, 7);
		keyToggleAndAnnounce(KeyCode.Alpha9, 8);
		keyToggleAndAnnounce(KeyCode.Alpha0, 9);
	}
}
