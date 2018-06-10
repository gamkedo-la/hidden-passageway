using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AetherPowerUp : MonoBehaviour {
//    private GameObject powerup;

	// Use this for initialization
	void Awake ()
    {
//        powerup = this.gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    void OnTriggerEnter(Collider col) //Trigger collision fires off KeyTriggered method and logs what it hit
    {
        if (col.gameObject.tag == "Player")
            Destroy(this.gameObject);
    }

    }
