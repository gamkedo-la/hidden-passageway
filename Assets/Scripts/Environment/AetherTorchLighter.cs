using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AetherTorchLighter : MonoBehaviour {

    private GameObject torch;
    [SerializeField]
    private Light tLight;
    [SerializeField]
    private ParticleSystem tPs;

	// Use this for initialization
	void Start ()
    {
        torch = GameObject.Find("Handtorch");
        tLight = GetComponentInChildren<Light>();
        tPs = GetComponentInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject == torch)
        {
            tLight.enabled = true;
            tPs.enableEmission = true;
        }
    }
}
