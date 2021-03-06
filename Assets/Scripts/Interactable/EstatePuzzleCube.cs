﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstatePuzzleCube : MonoBehaviour {
    public bool[] solution = new bool[4];
    public int currentSelection = 0;

    Transform childBlock;

    float rotationSpeed = 2;

    Vector3 currentRot;

	// Use this for initialization
	void Start () {
        childBlock = gameObject.transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {

        /*if (Input.GetKeyDown(KeyCode.O))
        {
            RotateDown();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            RotateUp();
        }*/

    }

    public void RotateUp()
    {
        transform.RotateAround(gameObject.GetComponentInChildren<Renderer>().bounds.center, childBlock.right, 90);
        //Mathf.LerpAngle(currentRot.x, currentRot.x + 90f, Time.deltaTime * rotationSpeed);
        MoveSelection(1);
    }
    public void RotateDown()
    {
        transform.RotateAround(gameObject.GetComponentInChildren<Renderer>().bounds.center, childBlock.right, -90);
        //Mathf.LerpAngle(currentRot.x, currentRot.x - 90f, Time.deltaTime * rotationSpeed);
        MoveSelection(0);

    }
    void MoveSelection(int direction)
    {
        //Moves selection up with rotation
        if(direction == 1)
        {
            if (currentSelection == 3)
            {
                currentSelection = 0;
            }
            else
                currentSelection++;
        }else if(direction == 0) //Moves Selection down with rotation
        {
            if (currentSelection == 0)
            {
                currentSelection = 3;
            }
            else
                currentSelection--;
        }
    }
}
