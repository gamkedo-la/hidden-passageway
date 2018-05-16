using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideTextWhenMouseLocked : MonoBehaviour {
	private Text textToHide;
	public bool showInsteadOfHide = false;

	// Use this for initialization
	void Start () {
		textToHide = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Cursor.lockState == CursorLockMode.Locked && textToHide.enabled != showInsteadOfHide) {
			textToHide.enabled = showInsteadOfHide;
		} else if(Cursor.lockState != CursorLockMode.Locked && textToHide.enabled == showInsteadOfHide) {
			textToHide.enabled = (!showInsteadOfHide);
		}
	}
}
