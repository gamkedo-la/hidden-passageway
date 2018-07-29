using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcavateRobotPart : MonoBehaviour {
    public Transform robotPart;

    [SerializeField] private float rateOfRotationY = 100.0f;
    [SerializeField] private float rateOfRotationZ = 5.0f;
    [SerializeField] private float rateOfRotationX = 0.0f;

    private float startingAngY = 0.0f;
    private float startingAngZ = 45.0f;
    private float startingAngX = -15.0f;

    private float rateOfPositionSlide = 1.0f;

    float stoppingPosForX = -100.0f;
    float stoppingPosForY = 80.0f;
    float stoppingPosForZ = -100.0f;

    public bool isPowerSourceInSlot = false;
    
	// Update is called once per frame
	public void Update () {
        if(isPowerSourceInSlot && robotPart != null)
        {
            if ((robotPart.position.x > stoppingPosForX) && (robotPart.position.y < stoppingPosForY) && (robotPart.position.z > stoppingPosForZ))
            {
                RotatePart();
                SlidePart();
            }
            else
            {
                isPowerSourceInSlot = false;
                return;
            }
        }
	}

    void RotatePart()
    {
        if(robotPart != null)
        {
            robotPart.rotation = Quaternion.AngleAxis(startingAngX, Vector3.right) * Quaternion.AngleAxis(startingAngZ, Vector3.forward)
            * Quaternion.AngleAxis(startingAngY, Vector3.up);
        }

        startingAngY += rateOfRotationY * Time.deltaTime;
        startingAngZ += rateOfRotationZ * Time.deltaTime;
    }

    void SlidePart()
    {
        if(robotPart != null)
        {
            robotPart.position += (robotPart.up * rateOfPositionSlide * Time.deltaTime);
        }
    }
}
