using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternScript : MonoBehaviour {

    bool held = false;
    public float offsetX = 0;
    public float offsetY = 0;
    public float offsetZ = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (held)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Throw();
            }
        }
    }

    public void LookedAt()
    {
        if (!held)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PickUp(null);
            }
        }
    }

    public void PickUp(Transform objToParent)
    {
        if(objToParent == null)
        {
            objToParent = Camera.main.transform;
        }
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        //gameObject.transform.SetParent(objToParent.transform);
        gameObject.transform.SetParent(objToParent);
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.transform.position = gameObject.transform.position;// + new Vector3(offsetX, offsetY, offsetZ);

        held = true;
    }
    void PutDown()
    {
        held = false;
    }
    void Throw()
    {
        gameObject.transform.SetParent(null);
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;

        held = false;
    }
}
