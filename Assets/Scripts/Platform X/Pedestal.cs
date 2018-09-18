using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal : MonoBehaviour {

	private bool isActive = false;

	void OnTriggerStay(Collider other)
	{
		PuzzleBall pb = other.GetComponent<PuzzleBall>();
		if (pb != null) {
			isActive = true;
		}
	}

	void Update () {
		if (isActive) {
			transform.Rotate(Vector3.up, 10 * Time.deltaTime);
		}

		// Will be re-enabled in OnTriggerStay if there's still a ball
		isActive = false;
	}
}
