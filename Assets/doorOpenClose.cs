using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpenClose : MonoBehaviour {
    public Animator anim;
    public bool startsOpen;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        
        if(startsOpen)
        {
            Open();
        }
	}
	
	// Update is called once per frame
	public void Close() {
        anim.Play("doorClose");
    }

    public void Open()
    {
        anim.Play("doorOpen");
    }
}
