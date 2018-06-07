using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeThisLightDimAndBrightenOverTime : MonoBehaviour {
    public GameObject[] gameObjectsList;
    public float dimmingRateForLightIntensity = 0.05f;
    public float brighteningRateForLightIntensity = 0.05f;
    public float minLightIntensity = 0.0f;
    public float maxLightIntensity = 0.5f;

    Light lightFixture;     
    bool isLightDimming = true;

    // Use this for initialization
    void Start () {
        Debug.Log("Flicker Lights Script reached in GO: " + gameObject.name);
    }
	
	// Update is called once per frame
	void Update () {
        ///<summary>
        /// The following loop cycles through each light source and dims/brightens that light based on your settings by a rate of your choosing.
        /// Can be used to make light sources flicker on and off
        ///</summary>
        for (int i = 0; i < gameObjectsList.Length; i++)
        {
            lightFixture = gameObjectsList[i].GetComponent<Light>();

            if(lightFixture.intensity > minLightIntensity && isLightDimming)
            {
                lightFixture.intensity -= dimmingRateForLightIntensity;

                if (lightFixture.intensity <= minLightIntensity && i == (gameObjectsList.Length - 1))
                {
                    isLightDimming = false;
                }
            }
            else if(lightFixture.intensity < maxLightIntensity && !isLightDimming)
            {
                lightFixture.intensity += brighteningRateForLightIntensity;

                if(lightFixture.intensity >= maxLightIntensity && i == (gameObjectsList.Length - 1))
                {
                    isLightDimming = true;
                }
            }
        }
    }
}
