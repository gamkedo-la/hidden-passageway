using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class AetherGameManager : MonoBehaviour {

    public float gameCompletionCounter = 0;
    public float treasureCounter = 0;
    public float deathCount = 0;

    [SerializeField]
    private string StartTime;
    [SerializeField]
    private string EndTime;
    private float startfloat;

    public bool Night;

    private Transform playerSpawn;
    private GameObject playerGO;

    public Light Sun;
    public Light secondSun;
    public bool stayDark;
    public float StartingLightIntensity;
    public float StartingLight2Intensity;
    public float UndergroundLightIntensity;
    public float UndergroundLight2Intensity;

    public Material skyboxmat1;
    public Material skyboxmat2;



    private void Awake()
    {
        playerGO = GameObject.FindGameObjectWithTag(Tags.Player);
        GameObject PsGO = GameObject.Find("PlayerSpawn");
        playerSpawn = PsGO.transform;


    }

    void Start ()
    {
        skyboxmat1 = RenderSettings.skybox;
        Assert.IsNotNull(skyboxmat2);
        StartingLightIntensity = Sun.intensity;
        StartingLight2Intensity = secondSun.intensity;
    }

    public void GameStart()
    {
        StartTime = Time.deltaTime.ToString();
        startfloat = Time.deltaTime;

    }

    public void printStatus()
    {

    }

    public void GameFinish()
    {
        EndTime = (Time.deltaTime - startfloat).ToString();
        gameCompletionCounter = treasureCounter * 3 - deathCount;
    }




    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == Tags.Player)
        {
            MakeNight();
        }

    }

    void OnTriggerExit(Collider col)
    {
        if ((col.gameObject.tag == Tags.Player) && !(stayDark))
        {
            MakeDay();
        }
    }

    void ChangeTime()
    {
        if (Night == true)
        {
            MakeDay();
            return;
        }
        if (Night == false)
        {
            MakeNight();
            return;
        }
    }
    void MakeDay()
    {
        Sun.intensity = StartingLightIntensity;
        RenderSettings.skybox = skyboxmat1;
        secondSun.intensity = StartingLight2Intensity;
        Night = false;
        DynamicGI.UpdateEnvironment();
        Debug.Log("Made it Day");

    }
    void MakeNight()
    {
        Sun.intensity = UndergroundLightIntensity;
        RenderSettings.skybox = skyboxmat2;
        secondSun.intensity = UndergroundLight2Intensity;
        Night = true;
        DynamicGI.UpdateEnvironment();
        Debug.Log("Made it Night");
    }
}
