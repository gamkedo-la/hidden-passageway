using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public bool shouldRotate = true;
    public bool rotateUp;
    public bool rotateRight;
    public float rotationSpeed = 1;
    public bool rotateAtRandom;
    public Vector3 rotationVector;
    private float randX;
    private float randY;
    private float randZ;


    // Use this for initialization

    private void Awake()
    {
        randX = Random.Range(-20, 20);
        randY = Random.Range(-20, 20);
        randZ = Random.Range(-20, 20);
    }
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
        if (rotateAtRandom)
        {
            rotationVector = new Vector3(randX, randY, randZ);
        }
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.Rotate(rotationVector * Time.deltaTime * rotationSpeed);
	}
}
