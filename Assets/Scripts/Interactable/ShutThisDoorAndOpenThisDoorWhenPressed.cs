using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutThisDoorAndOpenThisDoorWhenPressed : MonoBehaviour
{
    public doorOpenClose[] doorsToBeOpened;
    public doorOpenClose[] doorsInGroup;
    bool hasButtonBeenPressed = false;

    private void Start()
    {
        
    }

    //Update is called once per frame
    void Update()
    {
        RaycastHit rayHitInfo;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rayHitInfo, 1.5f))
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
