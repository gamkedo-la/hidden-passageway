using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class driveForward : MonoBehaviour {
	void Update () {
		transform.position += Time.deltaTime * 35.0f * transform.forward;
	}
}
