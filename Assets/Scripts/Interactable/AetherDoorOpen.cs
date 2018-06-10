using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AetherDoorOpen : MonoBehaviour {
    public bool StartMoving;
    public bool Finished;
    public Vector3 StartingLocation;
    public Vector3 EndLocation;
    public BoxCollider boxcol;
    public float AddedVal;


	// Use this for initialization
	void Awake ()
    {
        StartingLocation = transform.localPosition;
        boxcol = GetComponent<BoxCollider>();
	}
    void Start()
    {
        EndLocation = new Vector3(StartingLocation.x, StartingLocation.y + AddedVal, StartingLocation.z);
    }


    // Update is called once per frame
    // Totally going to smooth this out, just wanted the base idea done for presentation
    void Update ()
    {
        if (StartMoving && !Finished) 
        {
            transform.localPosition = new Vector3(StartingLocation.x, (transform.localPosition.y + Time.deltaTime), StartingLocation.z);
            boxcol.enabled = false;
            if ((transform.localPosition.y) >= EndLocation.y) 
            {
                Finished = true;
            }
        }
        if (Finished)
        {
            boxcol.enabled = true;
        }

    }
}
