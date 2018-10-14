using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimateSet : MonoBehaviour
{
    public GameObject refShip;
    private List<GameObject> shipList = new List<GameObject>();

	// Use this for initialization
	void Start () {
        float skipWid = 30.0f, skipHei = 30.0f;
        for (int i = 0; i < 20; i++)
        {
            for (int ii = 0; ii < 20; ii++)
            {
                if ((i == 10 && ii == 10) == false)
                {
                    GameObject tempGO = (GameObject)GameObject.Instantiate(refShip, Vector3.right * i * skipWid + Vector3.forward * ii * skipHei, Quaternion.identity);
                    tempGO.name = i + "_" + ii;
                    shipList.Add(tempGO);
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
