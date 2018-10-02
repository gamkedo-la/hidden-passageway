using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlideToPos : AbstractActivateable {
    public Transform endPos;
    public float duration = 2.0f;
    public bool lookAtAction = true;
    public bool rememberPreviousState = true;

    Camera mainCam;
    bool isStarted = false;
    float startTime;
    Vector3 startPos;
    Quaternion startRot;
    Quaternion camRotReset;
    static Quaternion camRotWorldInitial;
    private string mySaveName;
    bool isReversing = false;

    private Vector3 posA;
    private Quaternion rotA;
    private Vector3 posB;
    private Quaternion rotB;

    private bool isTouchingPlayer;
    private static GameObject playerGO;
    Transform wasPlayerParent;

    [FMODUnity.EventRef]
    public string startSound;
    [FMODUnity.EventRef]
    public string moveSound;
    [FMODUnity.EventRef]
    public string endSound;
    FMOD.Studio.EventInstance MoveLoopAudio;

	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tags.Player) {
            isTouchingPlayer = true;
            //Debug.Log(name +" is now touching player");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == Tags.Player)
        {
            isTouchingPlayer = false;
            //Debug.Log(name + " is no longer touching player");
        }
    }

    public void Start()
    {
        mainCam = Camera.main;
        if (playerGO == null) {
            playerGO = GameObject.FindWithTag(Tags.Player);
        }
        isStarted = false;
        camRotReset = mainCam.transform.localRotation;

        // storing separately, used for reverse option
        posA = transform.position;
        rotA = transform.rotation;
        posB = endPos.position;
        rotB = endPos.rotation;

        mySaveName = PlayerPrefsHelper.GetPrefsName(gameObject);
        int previousState = PlayerPrefs.GetInt(mySaveName, 0);

        if (rememberPreviousState && previousState == 1)
        {
            transform.position = endPos.position;
            transform.rotation = endPos.rotation;
            isStarted = isDone = true;
        }

        WarnAboutIncorrectTags();

        if (moveSound.Length > 2) {
            MoveLoopAudio = FMODUnity.RuntimeManager.CreateInstance(moveSound);
        }
    }

    public override void Reverse() {
        isStarted = isDone = false;

        Activate();

        // overriding how Activate() left them:
        PlayerPrefs.SetInt(mySaveName, 0);
        endPos.position = posA;
        endPos.rotation = rotA;
        startPos = posB;
        startRot = rotB;
        isReversing = true; // important to toggle off isStarted and isDone after
    }

    public override void Activate()
    {
        if (isStarted)
        {
            return;
        }
        if (isDone)
        {
            // this.enabled = false;
            return;
        }

        PlayerPrefs.SetInt(mySaveName, 1);
        if ((isReversing == false && callPrev == null) ||
                    (isReversing && callNext == null)) // start of chain?
        {
            camRotWorldInitial = mainCam.transform.rotation;
        }
        isStarted = true;
        startTime = Time.time;
        startPos = posA;
        startRot = rotA;
        endPos.position = posB;
        endPos.rotation = rotB;

        if (isTouchingPlayer) // elevator or spinning disc etc, so attach to it
        {
            wasPlayerParent = playerGO.transform.parent;
            playerGO.transform.parent = transform;
            WalkControl.instance.rb.isKinematic = true;
            WalkControl.instance.areFeetLocked = true;
        } else {
            WalkControl.instance.rb.isKinematic = true;
            WalkControl.instance.enabled = false;
            ViewControl.instance.enabled = false;
        }

        if (startSound.Length>0)
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(startSound, gameObject);
        }
        if(moveSound.Length > 0) {
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(MoveLoopAudio, gameObject.transform, GetComponent<Rigidbody>());
            MoveLoopAudio.start();
        }
    }

    void Update () {
        if(isStarted == false || isDone)
        {
            return;
        }
        float movePerc = (Time.time - startTime) / duration;
        if (movePerc > 1.0f)
        {
            isDone = true;

            movePerc = 1.0f;

            transform.position = endPos.position;
            transform.rotation = endPos.rotation;

            if(endSound.Length > 0) {
                FMODUnity.RuntimeManager.PlayOneShotAttached(endSound, gameObject);
            }
            if(moveSound.Length > 0) {
                MoveLoopAudio.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }

            if (isTouchingPlayer)
            {
                WalkControl.instance.areFeetLocked = false;
                WalkControl.instance.rb.isKinematic = false;
                playerGO.transform.parent = wasPlayerParent;
            } else {
                if ((isReversing == false && callNext == null) ||
                    (isReversing && callPrev == null)) // end of chain?
                {
                    if (lookAtAction) {
                        mainCam.transform.rotation = camRotWorldInitial;
                    }
                    WalkControl.instance.rb.isKinematic = false;
                    ViewControl.instance.enabled = true;
                    WalkControl.instance.enabled = true;
                }

            }

            if(isReversing) {
                isReversing = false;
                isStarted = isDone = false;
                if (callPrev)
                {
                    callPrev.Reverse();
                }
            } else {
                if (callNext)
                {
                    callNext.Activate();
                }
            }
            return;
        }
        else if (isTouchingPlayer == false && lookAtAction)
        {
            if (callPrev == null && callNext == null) // no chain?
            {
                float camTurn = Mathf.Clamp01((Time.time - startTime) / duration * 4.0f);
                mainCam.transform.rotation = Quaternion.Slerp(camRotWorldInitial,
                          Quaternion.LookRotation(transform.position -
                          mainCam.transform.position), camTurn);

                if (camTurn >= 1.0f)
                {
                    camTurn = Mathf.Clamp01(((startTime + duration) - Time.time) / duration * 4.0f);
                    mainCam.transform.rotation = Quaternion.Slerp(camRotWorldInitial,
                              Quaternion.LookRotation(transform.position -
                              mainCam.transform.position), camTurn);
                }
            } else {
                mainCam.transform.rotation = Quaternion.Slerp(mainCam.transform.rotation,
                        Quaternion.LookRotation( ( isReversing ? startPos : endPos.position) -
                                                      mainCam.transform.position), 4.0f * Time.deltaTime); // tilting to watch
            }
        }
        transform.position = Vector3.Lerp(startPos, endPos.position, movePerc);
        transform.rotation = Quaternion.Slerp(startRot, endPos.rotation, movePerc);

    }

    private void WarnAboutIncorrectTags()
    {
        bool matchFound = false;
        if (callPrev != null) // part of a sequence?
        {
            matchFound = true;
        }
        if (matchFound == false) // called directly by a player switch?
        {
            GameObject[] interactionSwitches = GameObject.FindGameObjectsWithTag(Tags.InteractionSwitch);
            for (int i = 0; i < interactionSwitches.Length; i++)
            {
                TriggerComponentEnable tceScript = interactionSwitches[i].GetComponent<TriggerComponentEnable>();
                // Skip this object if it has no script
                if (tceScript == null)
                {
                    Debug.LogWarning("Does the object " + interactionSwitches[i].name + " correctly have the tag set to " + Tags.InteractionSwitch + "?");
                    continue;
                }
                if (tceScript.toEnable == this || tceScript.toEnable2 == this || tceScript.toEnable3 == this)
                {
                    matchFound = true;
                }
            }
        }
        if (matchFound == false) {
            Debug.LogWarning("Is the tag set to " + Tags.InteractionSwitch + " for switch to " + gameObject.name + "?");
        }
    }
}
