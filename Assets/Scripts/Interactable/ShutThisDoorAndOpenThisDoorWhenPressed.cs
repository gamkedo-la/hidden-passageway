using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutThisDoorAndOpenThisDoorWhenPressed : MonoBehaviour
{
    public doorOpenClose[] doorsToBeOpened;
    public doorOpenClose[] doorsInGroup;
    bool hasButtonBeenPressed = false;
    Camera mainCam;

    private void Start()
    {
        GameObject camGO = GameObject.Find("TransitionCam");
        mainCam = camGO.GetComponent<Camera>();
    }
    
    void Update()
    {
        RaycastHit rayHitInfo;
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out rayHitInfo, 1.5f))
            {               
                if (rayHitInfo.collider.gameObject == gameObject)
                {
                    hasButtonBeenPressed = !hasButtonBeenPressed;
                    OpenDoors(hasButtonBeenPressed);
                }
            }           
        }
    }

    void OpenDoors(bool hasButtonBeenPressed)
    {
        if(hasButtonBeenPressed)
        {
            for (int i = 0; i < doorsInGroup.Length; i++)
            {
                doorsInGroup[i].buttonPressed = true;
                doorsInGroup[i].Close();
            }
        }
        else if(!hasButtonBeenPressed)
        {
            if (doorsToBeOpened != null)
            {
                for (int i = 0; i < doorsToBeOpened.Length; i++)
                {
                    doorsToBeOpened[i].buttonPressed = true;
                    doorsToBeOpened[i].Open();
                }
            }
        }
    }
}
