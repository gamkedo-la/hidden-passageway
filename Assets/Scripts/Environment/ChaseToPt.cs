using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseToPt : MonoBehaviour {
	public Transform[] ptList;
	private int ptNow = 0;
	private float timeScale = 0.1f;
	private float transitionStep = 0.0f;

	void Update () {
		transitionStep += Time.deltaTime * timeScale;
		Camera.main.transform.position =
			Vector3.Lerp(ptList[ptNow].position, ptList[ptNow+1].position, transitionStep);
		Camera.main.transform.rotation =
			Quaternion.Slerp(ptList[ptNow].rotation, ptList[ptNow+1].rotation, transitionStep);

		if(transitionStep >= 1.0f && ptNow < ptList.Length-2) {
			ptNow++;
			transitionStep = 0.0f;
		}
	}
}
