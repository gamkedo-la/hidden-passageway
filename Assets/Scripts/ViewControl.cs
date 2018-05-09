using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class ViewControl : MonoBehaviour {
	public Text linkClue;
    public Text linkClueShadow;
    private float lookAngLimit = 45.0f;

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
			TriggerComponentEnable activateComponent = rhInfo.collider.gameObject.GetComponent<TriggerComponentEnable>();
			if(activateComponent) {
				if(Input.GetMouseButtonDown(0)) {
                    activateComponent.triggerAction();
				} else {
                    if(activateComponent.wasUsed)
                    {
                        linkClueShadow.text = linkClue.text = "(already used)";
                    } else {
                        linkClueShadow.text = linkClue.text = activateComponent.displayText;
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
