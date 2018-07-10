using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AetherCaveLighting : MonoBehaviour {

    public Light Sun;
    public Light secondSun;
    public bool stayDark;
    public float StartingLightIntensity;
    public float StartingLight2Intensity;
    public float UndergroundLightIntensity;
    public float UndergroundLight2Intensity;
    public AetherGameManager AGM;

    public Material skyboxmat1;
    public Material skyboxmat2;

    public bool isnight;

    // Use this for initialization
    void Start ()
    {
        skyboxmat1 = RenderSettings.skybox;
        Assert.IsNotNull(skyboxmat2);
        StartingLightIntensity = Sun.intensity;
        StartingLight2Intensity = secondSun.intensity;
        GameObject AGMGO = GameObject.Find("GameManager");
        AGM = AGMGO.GetComponent<AetherGameManager>();
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
            RenderSettings.skybox = skyboxmat2;
            secondSun.intensity = UndergroundLight2Intensity;
            isnight = true;
            DynamicGI.UpdateEnvironment();
            AGM.Night = true;
        }

    }

    void OnTriggerExit(Collider col)
    {
        if ((col.gameObject.tag == Tags.Player) && !(stayDark))
        {
            Sun.intensity = StartingLightIntensity;
            secondSun.intensity = StartingLight2Intensity;
            isnight = false;
            DynamicGI.UpdateEnvironment();
            AGM.Night = false;
        }
    }
}
