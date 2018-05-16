using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Start()
    {
        isStarted = false;
        camRotReset = Camera.main.transform.localRotation;
    }

    public void Activate()
    {
        if (isStarted)
        {
            return;
        }

        if (isDone)
        {
            this.enabled = false;
            return;
        }
        ViewControl.instance.enabled = false;
        WalkControl.instance.enabled = false;
        camRotWorldInitial = Camera.main.transform.rotation;
        isStarted = true;
        startTime = Time.time;
        startPos = transform.position;
        startRot = transform.rotation;
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
            ViewControl.instance.enabled = true;
            WalkControl.instance.enabled = true;

            movePerc = 1.0f;
            Camera.main.transform.rotation = camRotWorldInitial;
            return;
        }
        else
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
