using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class ViewControl : MonoBehaviour {
	public Text linkClue;
    public Text linkClueShadow;
    public Image paperView;
    [SerializeField]
    private float lookAngLimit = 45.0f;
    public float raycastMaxDistance = 4.0f;
    public static float timeWhenIntroEnded = 0.0f;

    ReadableScrap readScript;
    int pageViewed = -1;

    public static ViewControl instance;

    // Use this for initialization
    void Start () {
        instance = this;
    }

	// Update is called once per frame
	void Update () {
        if (ArcadePlayer.playingNow != null)
        {
            return;
        }

        if(Cursor.lockState != CursorLockMode.Locked) {
			return;
		}

        if (paperView.enabled)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                paperView.enabled = false;
                WalkControl.instance.areFeetLocked = false;
            } else if(Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
            {
                pageViewed++;
                if (readScript.pageToRead == null || pageViewed < readScript.pageToRead.Length)
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

        if (WalkControl.instance.areFeetLocked)
        {
            return;
        }

        bool ignoreDuringInit = false;
        if (SceneWarp.fromScene != null && SceneWarp.fromScene.Length > 0)
        {
            ignoreDuringInit = false;
        }
        else if (Time.timeSinceLevelLoad - timeWhenIntroEnded < 0.25f)
        {
            ignoreDuringInit = true;
        }
        if (ignoreDuringInit == false)
        {
            float angleBefore = transform.rotation.eulerAngles.x;
            float angleMoveBy = Time.deltaTime * -60.0f * Input.GetAxis("Mouse Y");
            float angleAfter = angleBefore + angleMoveBy;
            /*if (angleAfter < -lookAngLimit)
            {
                angleMoveBy = (-lookAngLimit) - angleBefore;
            }
            */
            if (angleAfter > 180.0f)
            {
                angleAfter = angleAfter - 360.0f;
                if (angleAfter < -lookAngLimit)
                {
                    angleMoveBy = (-lookAngLimit) - angleBefore;
                }
            }
            else
            {
                if (angleAfter > lookAngLimit)
                {
                    angleMoveBy = lookAngLimit - angleBefore;
                }
            }
            transform.Rotate(Vector3.right, angleMoveBy);
        }

		if(linkClue.text != "") {
            linkClueShadow.text = linkClue.text = "";
		}

        RaycastHit rhInfo;
        int ignoreMask = ~LayerMask.GetMask("Ignore Raycast");
		if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rhInfo, raycastMaxDistance,
                            ignoreMask)) {
            MouseTipOnLook mtol = rhInfo.collider.gameObject.GetComponent<MouseTipOnLook>();
            // Debug.Log(rhInfo.collider.gameObject.name);
            /*LanternScript lantern = rhInfo.collider.gameObject.GetComponent<LanternScript>();

            if (lantern)
            {
                lantern.LookedAt();
            }*/

            if (mtol) {
                if(Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump")) {
                    readScript = mtol.GetComponent<ReadableScrap>();
                    if(readScript)
                    {
                        pageViewed=0;
                        paperView.sprite = readScript.pageToRead[pageViewed];
                        paperView.preserveAspect = true;
                        paperView.enabled = true;
                        WalkControl.instance.areFeetLocked = true;
                    } else
                    {
                        mtol.SendMessage("triggerAction", SendMessageOptions.DontRequireReceiver);
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
