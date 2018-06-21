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
    void OnTriggerEnter(Collider col) 
    {
        if (col.gameObject.tag == "Player")
            Destroy(this.gameObject);
    }

    }
