using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AetherNocturnal : MonoBehaviour {

    [SerializeField]
    private int changeType = 0;
    // 0 is for enabling or disabling one object
    // 1 is for disabling one and enabling the other (night/day modes)
    public bool isNight;

    [SerializeField]
    private GameObject nightVersion;
    [SerializeField]
    private GameObject dayVersion;
    [SerializeField]
    private AetherGameManager agm;

    private bool ranChangeNight;
    private bool ranChangeDay;

    // Use this for initialization
    void Start ()
    {
        GameObject AGMGO = GameObject.Find("GameManager");
        agm = AGMGO.GetComponent<AetherGameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((agm.Night == true) && (ranChangeNight == false))
        {
            Nighttime();
            ranChangeNight = true;
            ranChangeDay = false;
        }
        if ((agm.Night == false) && (ranChangeDay == false))
        {
            Daytime();
            ranChangeDay = true;
            ranChangeNight = false;
        }
    }

    void Daytime()
    {
        if ((changeType == 1) && !(nightVersion == null) && (!(dayVersion == null)))
        {
            nightVersion.SetActive(false);
            dayVersion.SetActive(true);
            return;
        }
        else
        {
            Debug.Log("Nocturnal item " + this + (" is missing GameObject references!"));
            return;
        }

    }

    void Nighttime()
    {
        if (changeType == 0 && !(nightVersion == null))
        {
            nightVersion.SetActive(true);
            return;
        }
        if ((changeType == 1) && !(nightVersion == null) && (!(dayVersion == null)))
        {
            nightVersion.SetActive(true);
            dayVersion.SetActive(false);
            return;
        }
        else
        {
            Debug.Log("Nocturnal item " + this + (" is missing GameObject references!"));
            return;
        }

    }
}
