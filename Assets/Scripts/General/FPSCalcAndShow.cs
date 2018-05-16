using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCalcAndShow : MonoBehaviour {
	private Text fpsText;
	private float deltaTime;
	// Use this for initialization
	void Start () {
		fpsText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
		float fps = 1.0f / deltaTime;
		fpsText.text = fps.ToString("N3");
	}
}
