using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public bool shouldRotate = true;
    public bool rotateUp;
    public bool rotateRight;
    public float rotationSpeed = 1;
    public Vector3 rotationVector;

	// Use this for initialization
	void Start ()
    {
        if (rotateUp) 
        {
            rotationVector = Vector3.up;
        }
        if (rotateRight) 
        {
            rotationVector = Vector3.right;
        }
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.Rotate(rotationVector * Time.deltaTime * rotationSpeed);
	}
}
