using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AetherKeyItemDestroy : MonoBehaviour {

    public GameObject target;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == target)
        {
            Destroy(this.gameObject);
        }
    }
}
