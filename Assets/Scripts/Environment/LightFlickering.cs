using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickering : MonoBehaviour {

    //THIS IS MADE FOR SMALLER OBJECTS (NOT TESTED ON LARGER SCALED OBJECTS).
    //JUST ADD THIS SCRIPT TO THE LIGHT YOU WISH TO FLICKER.

    float minFlickerRad = 0;
    float maxFlickerRad = 1;
    [SerializeField]
    float radiusMod = .5f;
    float minFlickerIntensity = 0;
    float maxFlickerIntensity = 1;
    [SerializeField]
    float intensityMod = 1f;
    float currentFlicker = .5f;
    public Light lightToMod;
    [SerializeField]
    float flickerSpeed = .08f;

	// Use this for initialization
	void Start () {
        lightToMod = gameObject.GetComponent<Light>();

        minFlickerRad = lightToMod.range - radiusMod;
        maxFlickerRad = lightToMod.range + radiusMod;
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            lightToMod.intensity *= 2.0f;
            lightToMod.range *= 2.0f;
        }
        minFlickerIntensity = lightToMod.intensity - intensityMod;
        maxFlickerIntensity = lightToMod.intensity + intensityMod;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        lightToMod.range = Mathf.Lerp(lightToMod.range, Random.Range(minFlickerRad, maxFlickerRad), flickerSpeed);
        lightToMod.intensity = Mathf.Lerp(lightToMod.intensity, Random.Range(minFlickerIntensity, maxFlickerIntensity), flickerSpeed);

        /*lightToMod.range = Random.Range(minFlickerRad, maxFlickerRad);
        lightToMod.intensity = Random.Range(minFlickerIntensity, maxFlickerIntensity);*/
    }
}
