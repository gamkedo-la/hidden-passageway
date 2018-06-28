using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutThisDoorAndOpenThisDoorWhenPressed : MonoBehaviour
{
    public doorOpenClose[] doorsToBeOpened;
    public doorOpenClose[] doorsInGroup;
    private bool isButtonPressed = false;

    //Update is called once per frame
    void Update()
    {
        RaycastHit rayHitInfo;
        if (Input.GetMouseButtonDown(0))
        {            
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rayHitInfo, 1.0f))
            {               
                if (rayHitInfo.collider.gameObject == gameObject)
                {                  
                    OpenMyDoorsOnly();
                }
            }           
        }
    }

    void OpenMyDoorsOnly()
    {
        if (doorsToBeOpened != null)
        {
            for (int i = 0; i < doorsToBeOpened.Length; i++)
            {
                doorsToBeOpened[i].Open();
            }
        }

        for (int i = 0; i < doorsInGroup.Length; i++)
        {
            doorsInGroup[i].Close();
        }
    }
}
