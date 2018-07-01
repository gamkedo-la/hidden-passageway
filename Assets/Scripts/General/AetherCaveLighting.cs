using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AetherCaveLighting : MonoBehaviour {

    public Light Sun;
    public Light secondSun;
    public bool stayDark;
    public float StartingLightIntensity;
    public float StartingLight2Intensity;
    public float UndergroundLightIntensity;
    public float UndergroundLight2Intensity;


    // Use this for initialization
    void Start ()
    {
        StartingLightIntensity = Sun.intensity;
        StartingLight2Intensity = secondSun.intensity;
    }

    // Update is called once per frame
    void Update ()
    {

	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == Tags.Player)
        {
            Sun.intensity = UndergroundLightIntensity;
            secondSun.intensity = UndergroundLight2Intensity;
        }

    }
    void OnTriggerExit(Collider col)
    {
        if ((col.gameObject.tag == Tags.Player) && !(stayDark))
        {
            Sun.intensity = StartingLightIntensity;
            secondSun.intensity = StartingLight2Intensity;
        }
    }
}
