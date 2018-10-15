using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMeIfWebGL : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            gameObject.SetActive(false);
        }
	}
}
