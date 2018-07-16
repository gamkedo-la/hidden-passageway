using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternSway : MonoBehaviour {

    LanternScript mainLanternScript;
    public GameObject target;
    Rigidbody targetRB;
    public bool constraintsFrozen = false;

    Vector3 targetPos;
    Quaternion targetRot;

    Rigidbody rb;
    //The sway from camera moving
    float swayAmount = 6;
    Vector3 swayDir;

    //sway from moving forwards, backwards, left and right
    float movementSway = 6;

    float maxVelocity = 1.6f;
    Vector3 currentVelocity = new Vector3(0,0,0);

    Vector3 currentPos;
    Vector3 previousPos;

	// Use this for initialization
	void Start () {
        mainLanternScript = gameObject.GetComponent<LanternScript>();
        rb = gameObject.GetComponent<Rigidbody>();
        previousPos = gameObject.transform.position;
        targetRB = target.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (mainLanternScript.held)
        {
            currentVelocity = rb.velocity;
            float rotateSpeed = Input.GetAxis("Mouse X");

            if (Input.GetAxis("Vertical") != 0)
            {
                rb.AddForce(Vector3.right * Input.GetAxis("Vertical") * movementSway);
            }
            if (Input.GetAxis("Horizontal") != 0)
            {
                rb.AddForce(-Vector3.forward * Input.GetAxis("Horizontal") * movementSway);
            }


            if ((currentVelocity.x + currentVelocity.y + currentVelocity.z) < maxVelocity && (currentVelocity.x + currentVelocity.y + currentVelocity.z) > -maxVelocity)
            {
                //BEST LOOKING SWAY SO FAR
                if (rotateSpeed < 0)
                {
                    rb.AddForce((Vector3.forward * rotateSpeed) * -swayAmount);
                }
                if (rotateSpeed > 0)
                {
                    rb.AddForce((Vector3.forward * rotateSpeed) * -swayAmount);
                }
            }
        }
    }

    //Prevents the target for the hinge from flying off while lantern is held.
    public void ToggleFreezeTargetPos()
    {
        if (constraintsFrozen)
        {
            targetRB.constraints = RigidbodyConstraints.FreezePosition;
        }
        else
            targetRB.constraints = RigidbodyConstraints.None;
    }
}