using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpenClose : MonoBehaviour {
    private Transform startPos;
    public Transform endPos;
    public bool startsOpen;

	// Use this for initialization
	void Start () {
        GameObject startGO = new GameObject();
        startGO.name = gameObject.name + "_initial";
        startGO.transform.position = transform.position;
        startGO.transform.rotation = transform.rotation;
        startPos = startGO.transform;
        if(startsOpen)
        {
            Open();
        }
	}
	
	// Update is called once per frame
	public void Close() {
        transform.position = startPos.position;
        transform.rotation = startPos.rotation;
    }

    public void Open()
    {
        transform.position = endPos.position;
        transform.rotation = endPos.rotation;
    }
}
