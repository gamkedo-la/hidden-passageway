using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCheat : MonoBehaviour {
    public GameObject playerGO;
    public Transform[] teleList;

	void Update () {
        if (Input.GetKey(KeyCode.LeftShift) == false)
        {
            return;
        }

        int whichDest = 0;
        if (Input.GetKeyDown(KeyCode.Alpha1) && teleList.Length>whichDest)
        {
            playerGO.transform.position = teleList[whichDest].position;
        }
        whichDest++;
        if (Input.GetKeyDown(KeyCode.Alpha2) && teleList.Length > whichDest)
        {
            playerGO.transform.position = teleList[whichDest].position;
        }
        whichDest++;
        if (Input.GetKeyDown(KeyCode.Alpha3) && teleList.Length > whichDest)
        {
            playerGO.transform.position = teleList[whichDest].position;
        }
        whichDest++;
        if (Input.GetKeyDown(KeyCode.Alpha4) && teleList.Length > whichDest)
        {
            playerGO.transform.position = teleList[whichDest].position;
        }
        whichDest++;
        if (Input.GetKeyDown(KeyCode.Alpha5) && teleList.Length > whichDest)
        {
            playerGO.transform.position = teleList[whichDest].position;
        }
        whichDest++;
        if (Input.GetKeyDown(KeyCode.Alpha6) && teleList.Length > whichDest)
        {
            playerGO.transform.position = teleList[whichDest].position;
        }
        whichDest++;
        if (Input.GetKeyDown(KeyCode.Alpha7) && teleList.Length > whichDest)
        {
            playerGO.transform.position = teleList[whichDest].position;
        }
        whichDest++;
        if (Input.GetKeyDown(KeyCode.Alpha8) && teleList.Length > whichDest)
        {
            playerGO.transform.position = teleList[whichDest].position;
            playerGO.transform.rotation = teleList[whichDest].rotation;
        }
        whichDest++;
        if (Input.GetKeyDown(KeyCode.Alpha9) && teleList.Length > whichDest)
        {
            playerGO.transform.position = teleList[whichDest].position;
        }
        whichDest++;
        if (Input.GetKeyDown(KeyCode.Alpha0) && teleList.Length > whichDest)
        {
            playerGO.transform.position = teleList[whichDest].position;
        }
        whichDest++;
		
	}
}
