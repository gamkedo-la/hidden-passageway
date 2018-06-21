using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AetherPhaseDoor : MonoBehaviour {

    [SerializeField]
    private GameObject phaseTarget;
    private BoxCollider bc;

	// Use this for initialization
	void Start ()
    {
        bc = GetComponent<BoxCollider>();
        bc.enabled = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject == phaseTarget)
        {
            Debug.Log("Entered");
            bc.enabled = false;
            StopAllCoroutines();
        }
            
    }
    void OnTriggerExit(Collider col) 
    {
        if (col.gameObject == phaseTarget)
        {
            Debug.Log("Exitted");
            StartCoroutine ("CD");
        }



    }
    private IEnumerator CD()
    {
        Debug.Log("Coroutine started");
        yield return new WaitForSeconds(1);
        Debug.Log("Coroutine finished");
        bc.enabled = true;
        StopCoroutine("CD");
    }

}
