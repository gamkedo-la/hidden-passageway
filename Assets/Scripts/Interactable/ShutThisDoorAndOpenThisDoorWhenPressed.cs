using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutThisDoorAndOpenThisDoorWhenPressed : MonoBehaviour
{
    public GameObject doorToBeOpened;
    public GameObject doorToBeClosed;

    public Transform doorToBeClosedEndPosition;
    public Transform doorToBeOpenedEndPosition;

	// Use this for initialization
	void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit rayHitInfo;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rayHitInfo, 1.0f))
        {
            if(rayHitInfo.collider.tag.Contains("red button"))
            {
                Debug.Log("Hitting button: " + rayHitInfo.collider.name);
                if(Input.GetMouseButtonDown(0))
                {
                    doorToBeOpened.transform.position = doorToBeOpenedEndPosition.position;
                    doorToBeClosed.transform.position = doorToBeClosedEndPosition.position;
                }
            }
            else if (rayHitInfo.collider.tag.Contains("blue button"))
            {
                Debug.Log("Hitting button: " + rayHitInfo.collider.name);
                if (Input.GetMouseButtonDown(0))
                {
                    doorToBeOpened.transform.position = doorToBeOpenedEndPosition.position;
                    doorToBeClosed.transform.position = doorToBeClosedEndPosition.position;
                }
            }
        }
    }
}
