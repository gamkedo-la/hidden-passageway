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
    private Light sunLight;



    private void Awake()
    {
        playerGO = GameObject.FindGameObjectWithTag(Tags.Player);
        GameObject PsGO = GameObject.Find("PlayerSpawn");
        playerSpawn = PsGO.transform;
        GameObject sun = GameObject.Find("Directional Light");
        sunLight = sun.GetComponent<Light>();


    }

    void Start ()
    {
		
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

	
	void Update ()
    {
		
	}
}
