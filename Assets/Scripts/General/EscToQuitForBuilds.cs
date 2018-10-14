using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscToQuitForBuilds : MonoBehaviour {

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log("EscToQuitForBuilds script is no longer used");
			// Application.Quit();
		}
	}
}
