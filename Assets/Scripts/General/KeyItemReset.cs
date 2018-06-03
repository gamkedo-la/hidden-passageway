using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemReset : MonoBehaviour {


    public GameObject resetableObject;
    private Vector3 startPoint;
	// Use this for initialization
	void Start ()
    {
        startPoint = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            transform.position = startPoint;
        }

    }
}
