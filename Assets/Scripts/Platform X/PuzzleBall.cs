using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBall : MonoBehaviour {

	[SerializeField] private float resetPositionY = -30f;

	private Rigidbody rb;
	private Vector3 resetPosition;
	private Quaternion resetRotation;

	void Start () {
		rb = gameObject.GetComponent<Rigidbody>();
		resetPosition = transform.position;
		resetRotation = transform.rotation;
	}

	void Update () {
		if (transform.position.y <= resetPositionY) {
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			transform.position = resetPosition;
			transform.rotation = resetRotation;
		}
	}
}
