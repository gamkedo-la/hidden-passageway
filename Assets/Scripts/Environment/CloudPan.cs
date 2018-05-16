using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudPan : MonoBehaviour {
	void Update () {
		transform.position += Time.deltaTime * (transform.right * -1.1f +
			transform.up * -0.8f);
	}
}
