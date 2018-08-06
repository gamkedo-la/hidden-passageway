using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpenClose : MonoBehaviour
{
    public Animator anim;
    public bool startsOpen;
    public bool buttonPressed = false;
    public bool doorClosed = false;
    public bool doorOpened = false;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        
        if(startsOpen)
        {
            Open();
        }
        else
        {
            doorClosed = true;
        }
	}

    private void FixedUpdate()
    {
        if(buttonPressed)
        {
            //Debug.Log("playing air pressure SFX");
            buttonPressed = false;
        }
    }

    public void Close()
    {
        anim.Play("doorClose");
    }

    public void Open()
    {
        anim.Play("doorOpen");
    }
}
