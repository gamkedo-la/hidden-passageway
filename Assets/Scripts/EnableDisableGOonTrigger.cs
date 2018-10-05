using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableGOonTrigger : MonoBehaviour {

    public GameObject go;
    public bool setActiveOnTrigger;

    void OnTriggerEnter(Collider other) {
        go.SetActive(setActiveOnTrigger);
    }
}
