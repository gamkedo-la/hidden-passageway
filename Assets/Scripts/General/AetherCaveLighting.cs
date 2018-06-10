﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AetherCaveLighting : MonoBehaviour {

    public Light Sun;
    public float StartingLightIntensity;
    public float UndergroundLightIntensity;


	// Use this for initialization
	void Start ()
    {
        StartingLightIntensity = Sun.intensity;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Sun.intensity = UndergroundLightIntensity;
        }

    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Sun.intensity = StartingLightIntensity;
        }
    }
}