using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpenClose : MonoBehaviour
{
    private Animator anim;
    public bool startsOpen;
    public bool buttonPressed = false;

    [FMODUnity.EventRef]
    public string startSound;
    [FMODUnity.EventRef]
    public string moveSound;
    [FMODUnity.EventRef]
    public string endSound;
    FMOD.Studio.EventInstance MoveLoopAudio;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

        if (startsOpen)
        {
            Open();
        }
    }

    private void FixedUpdate()
    {
        if(buttonPressed)
        {
            if (startSound.Length > 0)
            {
                FMODUnity.RuntimeManager.PlayOneShotAttached(startSound, gameObject);
            }
            
            buttonPressed = false;
        }
    }

    public void Close()
    {
        //anim.Play("doorClose");

        anim.SetBool("openDoor", false);
        anim.SetBool("closeDoor", true);
    }

    public void Open()
    {
        //anim.Play("doorOpen");

        anim.SetBool("closeDoor", false);
        anim.SetBool("openDoor", true);
    }
}   
