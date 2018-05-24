﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlideToPos : MonoBehaviour {
    public Transform endPos;
    public float duration = 2.0f;
    public bool isDone = false;

    bool isStarted = false;
    float startTime;
    Vector3 startPos;
    Quaternion startRot;
    Quaternion camRotReset;
    Quaternion camRotWorldInitial;
    private string mySaveName;
    bool isReversing = false;

    private Vector3 posA;
    private Quaternion rotA;
    private Vector3 posB;
    private Quaternion rotB;

    private bool isTouchingPlayer;
    private static GameObject playerGO;
    Transform wasPlayerParent;

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player") {
            isTouchingPlayer = true;
            //Debug.Log(name +" is now touching player");
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTouchingPlayer = false;
            //Debug.Log(name + " is no longer touching player");
        }
    }

    public void Start()
    {
        if(playerGO == null) {
            playerGO = GameObject.FindWithTag("Player");
        }
        isStarted = false;
        camRotReset = Camera.main.transform.localRotation;

        mySaveName = SceneManager.GetActiveScene().name +
                                      gameObject.name;
        int wasFinished = PlayerPrefs.GetInt(mySaveName, 0);

        // storing separately, used for reverse option
        posA = transform.position;
        rotA = transform.rotation;
        posB = endPos.position;
        rotB = endPos.rotation;

        if (wasFinished == 1)
        {
            transform.position = endPos.position;
            transform.rotation = endPos.rotation;
            isStarted = isDone = true;
            GameObject[] interactionSwitches = GameObject.FindGameObjectsWithTag("InteractionSwitch");
            bool matchFound = false;
            for (int i = 0; i < interactionSwitches.Length; i++)
            {
                TriggerComponentEnable tceScript = interactionSwitches[i].GetComponent<TriggerComponentEnable>();
                if (tceScript.toEnable == gameObject)
                {
                    matchFound = true;
                }
            }
            if(matchFound == false) {
                Debug.LogWarning("Is the tag set to InteractionSwitch for switch to " + gameObject.name);
            }
        }
    }

    public void Reverse() {
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

    public void Activate()
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
        camRotWorldInitial = Camera.main.transform.rotation;
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
            WalkControl.instance.areFeetLocked = true;
        } else {
            WalkControl.instance.enabled = false;
            ViewControl.instance.enabled = false;
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
            if (isTouchingPlayer)
            {
                WalkControl.instance.areFeetLocked = false;
                playerGO.transform.parent = wasPlayerParent;
            } else {
                Camera.main.transform.rotation = camRotWorldInitial;
                ViewControl.instance.enabled = true;
                WalkControl.instance.enabled = true;
            }

            if(isReversing) {
                isReversing = false;
                isStarted = isDone = false;
            }
            return;
        }
        else if (isTouchingPlayer == false)
        {
            float camTurn = Mathf.Clamp01((Time.time - startTime) * 2.0f);
            Camera.main.transform.rotation = Quaternion.Slerp(camRotWorldInitial,
                      Quaternion.LookRotation(transform.position -
                      Camera.main.transform.position), camTurn);

            if (camTurn >= 1.0f) {
                camTurn = Mathf.Clamp01(((startTime + duration) - Time.time) * 2.0f);
                Camera.main.transform.rotation = Quaternion.Slerp(camRotWorldInitial,
                          Quaternion.LookRotation(transform.position -
                          Camera.main.transform.position), camTurn);
            }
        }
        transform.position = Vector3.Lerp(startPos, endPos.position, movePerc);
        transform.rotation = Quaternion.Slerp(startRot, endPos.rotation, movePerc);
    }
}