using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternScript : MonoBehaviour {

    bool held = false;

    float offsetX = 0;
    float offsetY = 0;
    float offsetZ = 0;

    float throwForce = 0;
    float forceBuildupSpeed = 10;
    [SerializeField]
    float maxThrowForce = 10;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (held)
        {
            if (Input.GetMouseButtonUp(0))
            {
                PutDown();
            }
            if (Input.GetMouseButton(1))
            {
                if(throwForce < maxThrowForce)
                {
                    throwForce += 1 * Time.deltaTime * forceBuildupSpeed;
                } else if(throwForce > maxThrowForce)
                {
                    throwForce = maxThrowForce;
                }
            }else if (Input.GetMouseButtonUp(1))
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
        gameObject.transform.SetParent(null);
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;

        held = false;
    }
    void Throw()
    {
        gameObject.transform.SetParent(null);
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;

        gameObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);

        throwForce = 0;
        held = false;
    }
}
