using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutThisDoorAndOpenThisDoorWhenPressed : MonoBehaviour
{
    public doorOpenClose doorToBeOpened;
    public doorOpenClose[] doorsInGroup;

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
                    OpenMyDoorOnly();
                }
            }           
        }
    }

    void OpenMyDoorOnly()
    {
        for(int i = 0; i < doorsInGroup.Length; i++)
        {
            doorsInGroup[i].Close();
        }

        doorToBeOpened.Open();
    }
}
