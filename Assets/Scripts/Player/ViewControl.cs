﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class ViewControl : MonoBehaviour {
	public Text linkClue;
    public Text linkClueShadow;
    public Image paperView;
    private float lookAngLimit = 45.0f;

    ReadableScrap readScript;
    int pageViewed = -1;

    public static ViewControl instance;

    // Use this for initialization
    void Start () {
        instance = this;
    }
	
	// Update is called once per frame
	void Update () {
		if(Cursor.lockState != CursorLockMode.Locked) {
			return;
		}

        if (paperView.enabled)
        {
            if(Input.GetMouseButtonDown(0))
            {
                pageViewed++;
                if (pageViewed < readScript.pageToRead.Length)
                {
                    paperView.sprite = readScript.pageToRead[pageViewed];
                    paperView.enabled = true;
                    WalkControl.instance.areFeetLocked = true;
                } else {
                    paperView.enabled = false;
                    WalkControl.instance.areFeetLocked = false;
                }
            }
            return;
        }

		transform.Rotate(Vector3.right, Time.deltaTime * -60.0f * Input.GetAxis("Mouse Y"));
		float lookAng = transform.rotation.eulerAngles.x;
		if(lookAng > 180.0f) {
			lookAng = lookAng - 360.0f;
			if(lookAng < -lookAngLimit) {
				transform.Rotate(Vector3.right, ((-lookAngLimit) - lookAng));
			}
		} else {
			if(lookAng > lookAngLimit) {
				transform.Rotate(Vector3.right, (lookAngLimit-lookAng));
			}
		}

		if(linkClue.text != "") {
            linkClueShadow.text = linkClue.text = "";
		}

		RaycastHit rhInfo;
		if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rhInfo, 4.0f)) {		
            MouseTipOnLook mtol = rhInfo.collider.gameObject.GetComponent<MouseTipOnLook>();

            if(mtol) {
                if(Input.GetMouseButtonDown(0)) {
                    readScript = mtol.GetComponent<ReadableScrap>();
                    if(readScript)
                    {
                        pageViewed=0;
                        paperView.sprite = readScript.pageToRead[pageViewed];
                        paperView.enabled = true;
                        WalkControl.instance.areFeetLocked = true;
                    } else
                    {
                        mtol.SendMessage("triggerAction");
                    }
                } else {
                    TriggerComponentEnable activateComponent = mtol.GetComponent<TriggerComponentEnable>();
                
                    if(activateComponent == null || activateComponent.canBeUsed())
                    {
                        linkClueShadow.text = linkClue.text = mtol.displayText;
                    } else {
                        linkClueShadow.text = linkClue.text = "(already used)";
                    }
                }
			}
		}
	}

	public void OpenLink(string URL) {
		#if UNITY_WEBGL
		openWindow(URL);
		#else
		Application.OpenURL(URL);
		#endif
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);
}
