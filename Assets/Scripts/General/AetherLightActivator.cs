using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AetherLightActivator : MonoBehaviour {

    public Light Light1;
    public Light Light2;
    public Light Light3;
    public Light Light4;
    public Light Light5;
    public Light Light6;
    public float OnIntensity;
    public float OffIntensity;


	// Use this for initialization
	void Start ()
    {
        OnIntensity = Light1.intensity;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Light1.intensity = OnIntensity;
            Light2.intensity = OnIntensity;
            Light3.intensity = OnIntensity;
            Light4.intensity = OnIntensity;
            Light5.intensity = OnIntensity;
            Light6.intensity = OnIntensity;
        }

    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Light1.intensity = OffIntensity;
            Light2.intensity = OffIntensity;
            Light3.intensity = OffIntensity;
            Light4.intensity = OffIntensity;
            Light5.intensity = OffIntensity;
            Light6.intensity = OffIntensity;
        }
    }
}
