using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOutTheLights : MonoBehaviour {
    public GameObject lightsGO;
	private void OnTriggerEnter(Collider other)
	{
        Debug.Log("enter the trigger");
        lightsGO.SetActive(false);
    }

}
