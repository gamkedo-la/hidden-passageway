using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableGOonTrigger : MonoBehaviour {

    public GameObject[] goList;
    public bool setActiveOnTrigger;

    void OnTriggerEnter(Collider other) {
        foreach (GameObject go in goList)
        {
            go.SetActive(setActiveOnTrigger);
        }
    }
}
