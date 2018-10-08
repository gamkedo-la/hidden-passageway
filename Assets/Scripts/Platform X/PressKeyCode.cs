using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressKeyCode : MonoBehaviour {

	Camera mainCamera;

	void Start () {
		mainCamera = Camera.main;
	}

	void Update () {

        if (!Input.GetMouseButtonDown(0))
        {
			return;
		}

		RaycastHit hit;
		if (!Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2.5f))
		{
			return;
		}

		ControlPanelButton btn = hit.collider.gameObject.GetComponent<ControlPanelButton>();
		if (!btn)
		{
			Debug.Log("has raycast hit on other object? " + hit.collider.gameObject.name);
			return;
		}

		Debug.Log("clicked: "+btn.buttonValue);
	}
}
