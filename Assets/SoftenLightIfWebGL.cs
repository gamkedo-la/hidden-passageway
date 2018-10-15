using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftenLightIfWebGL : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Light myLight = GetComponent<Light>();
            myLight.intensity *= 0.525f;
        }
	}
}
