using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeRobotPartExcavateItselfOutOfSandDune : MonoBehaviour {
    public Transform robotPart;
    public Transform posToStartTilting;

    public float rateOfRotationY = 0.5f;
    public float rateOfRotationZ = 20.0f;
    public float rateOfRotationX = 20.0f;

    public float startingAngY = 0.0f;
    public float startingAngZ = 45.0f;
    public float startingAngX = -15.0f;

    public float rateOfPositionSlide = 0.5f;

	// Use this for initialization
	void Start () {
        Debug.Log("Excavating robot part: " + robotPart.name);
	}
	
	// Update is called once per frame
	void Update () {
        RotatePart();
        SlidePart();
	}

    void RotatePart()
    {
        robotPart.rotation = Quaternion.AngleAxis(startingAngX, Vector3.right) * Quaternion.AngleAxis(startingAngZ, Vector3.forward)
            * Quaternion.AngleAxis(startingAngY, Vector3.up);

        startingAngY += rateOfRotationY * Time.deltaTime;
        startingAngZ += rateOfRotationZ * Time.deltaTime;
    }

    void SlidePart()
    {
        robotPart.position += (robotPart.up * rateOfPositionSlide * Time.deltaTime);
    }
}
