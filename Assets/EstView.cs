using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EstView : MonoBehaviour {
    public Transform[] viewWaypoints;
    public float duration = 5.0f;
    public Text progressUpdate;
    public int wpNow = 0;
    private float durationStart = 0.0f;
    private Camera useCamera;
    private Transform wasCamParent;
    private GameObject playerGO;

    private Vector3 wasPos;
    private Quaternion wasRot;

    GameObject menuGO, aimerGO, mouseLookGO;
    GameObject TipTextShadowGO, TipTextFGGO;

    public GameObject[] turnOffWhenDone;
    public GameObject[] turnOnWhenDone;
    public GameObject[] destroyWhenDone;

    public GameObject turnOffOnStartClick;
    public GameObject[] turnOnAfterWaitingForClick;
    private AetherNightIntroManage nightIntroManager;

    public bool waitForClickToStart = false;
    public bool stopIntroAudioAfterEstShots = false;

	// Use this for initialization
	void Awake () {
        nightIntroManager = GetComponent<AetherNightIntroManage>();
        if (SceneWarp.fromScene == "MainHub")
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/MainHub/StartGame", gameObject);
        }

        if (SceneWarp.fromScene != null && SceneWarp.fromScene != "MainHub")
        {
            foreach(GameObject eachGO in turnOnAfterWaitingForClick)
            {
                eachGO.SetActive(true);
            }
            waitForClickToStart = false;
            gameObject.SetActive(false);
            return;
        }
        menuGO = GameObject.Find("MenuHub");
        aimerGO = GameObject.Find("Aimer");
        mouseLookGO = GameObject.Find("MouseLockTip");
        TipTextShadowGO = GameObject.Find("TipTextShadow");
        TipTextFGGO = GameObject.Find("TipTextFG");

        TipTextShadowGO.GetComponent<Text>().text = "";;
        TipTextFGGO.GetComponent<Text>().text = "";;
        menuGO.SetActive(false);
        aimerGO.SetActive(false);
        if (mouseLookGO)
        {
            mouseLookGO.SetActive(false);
        }

        GameObject camGO = GameObject.Find("TransitionCam");
        useCamera = camGO.GetComponent<Camera>();
        wasCamParent = useCamera.transform.parent;
        useCamera.transform.SetParent(null);
        wasPos = useCamera.transform.position;
        wasRot = useCamera.transform.rotation;
        playerGO = GameObject.Find("Player");
        playerGO.SetActive(false);
	}

    void QuitEst()
    {
        ViewControl.timeWhenIntroEnded = Time.timeSinceLevelLoad;

        if (nightIntroManager)
        {
            nightIntroManager.ReturnToDay();
        }

        foreach(GameObject offGO in turnOffWhenDone)
        {
            offGO.SetActive(false);
        }
        foreach (GameObject onGO in turnOnWhenDone)
        {
            onGO.SetActive(true);
        }
        foreach (GameObject destGO in destroyWhenDone)
        {
            Destroy(destGO);
        }

        if (stopIntroAudioAfterEstShots)
        {
            MusicNotStartedYet.instance.StopSong();
        }

        useCamera.transform.position = wasPos;
        useCamera.transform.rotation = wasRot;
        playerGO.SetActive(true);
        menuGO.SetActive(true);
        aimerGO.SetActive(true);
        if (mouseLookGO)
        {
            mouseLookGO.SetActive(true);
        }
        useCamera.transform.SetParent(wasCamParent);
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (waitForClickToStart)
        {
            /*if (Input.GetMouseButtonUp(0))
            {*/
                durationStart = Time.timeSinceLevelLoad;
                foreach (GameObject eachGO in turnOnAfterWaitingForClick)
                {
                    eachGO.SetActive(true);
                }
                turnOffOnStartClick.SetActive(false);
                waitForClickToStart = false;
            /*}
            else
            {
                return;
            }*/
        }

        if (SceneWarp.fromScene == null) // i.e. hub
        {
            MusicNotStartedYet.instance.StartSong(); // hammering it, otherwise hub didn't start
        }

        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1") ||
            Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Tab)
            || Input.GetKeyDown(KeyCode.LeftShift))
        {
            // avoids accidental skip click
            if (wpNow > 0 || Time.timeSinceLevelLoad - durationStart > 0.3f)
            {
                QuitEst();
                return;
            }
        }

        float lerpPt = (Time.timeSinceLevelLoad-durationStart) / duration;
        if (lerpPt > 1.0f || Input.GetKeyDown(KeyCode.N))
        {
            wpNow+=2;
            if (wpNow + 1 >= viewWaypoints.Length)
            {
                QuitEst();
                return;
            }
            else
            {
                durationStart = Time.timeSinceLevelLoad;
                lerpPt = 0.0f;
            }
        }

        int wpPairs = Mathf.FloorToInt(viewWaypoints.Length * 0.5f);
        int pairYet = Mathf.FloorToInt(wpNow * 0.5f);
        float percPerSegment = 1.0f / wpPairs;
        float percentage = pairYet * percPerSegment + lerpPt * percPerSegment;
        float totalTime = duration * wpPairs;
        string percSoFar = ""+Mathf.FloorToInt( (1.0f-percentage) * totalTime);
        if (percSoFar.Length < 2)
        {
            percSoFar = "0" + percSoFar;
        }
        progressUpdate.text = "Click to Skip Intro (0:"+percSoFar+")";

        useCamera.transform.position = Vector3.Lerp(viewWaypoints[wpNow].position,viewWaypoints[wpNow+1].position, lerpPt);
        useCamera.transform.rotation = Quaternion.Slerp(viewWaypoints[wpNow].rotation, viewWaypoints[wpNow + 1].rotation, lerpPt);
	}
}
