﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternScript : MonoBehaviour {
    Transform defaultParent;
    GameObject parentObject;
    Transform trueDefaultParent;
    public bool held = false;

    Collider[] colliders = new Collider[2];

    bool canPerformAction = true;

    [SerializeField]
    float depthOffset = 0;
    [SerializeField]
    float horizontalOffset = 0;
    [SerializeField]
    float verticalOffset = 0;

    float throwForce = 0;
    float forceBuildupSpeed = 125;

    float maxThrowForce = 75;

    LanternSway lanternSwayScript;

    [FMODUnity.EventRef]
    public string itemPickupSound;
    FMOD.Studio.EventInstance ItemPickupAudio;
    public string itemDropSound;
    FMOD.Studio.EventInstance ItemDropAudio;

    private void Awake()
    {
        defaultParent = gameObject.transform.parent;
        parentObject = gameObject.transform.parent.gameObject;
        trueDefaultParent = parentObject.transform.parent;
        colliders = gameObject.GetComponents<BoxCollider>();
        lanternSwayScript = gameObject.GetComponent<LanternSway>();
    }
    // Use this for initialization
    void Start () {
        if (!string.IsNullOrEmpty(itemPickupSound))
        {
            ItemPickupAudio = FMODUnity.RuntimeManager.CreateInstance(itemPickupSound);
        }
        else
        {
            itemPickupSound = @"event:/BatesEstate/LanternPickup";
        }

        ItemPickupAudio = FMODUnity.RuntimeManager.CreateInstance(itemPickupSound);
    }
	
	// Update is called once per frame
	void Update () {
        if (held)
        {
            if (canPerformAction)
            {
                /*if (Input.GetMouseButtonDown(0))
                {
                    PutDown();
                }*/
                if (Input.GetButtonDown("Fire2"))
                {
                    if (throwForce < maxThrowForce)
                    {
                        throwForce += 1 * Time.deltaTime * forceBuildupSpeed;
                    }
                    else if (throwForce > maxThrowForce)
                    {
                        throwForce = maxThrowForce;
                    }
                }
                else if (Input.GetButtonUp("Fire2"))
                {
                    Throw();
                }
            }else if (!canPerformAction)
            {
                if (Input.GetButtonUp("Fire1"))
                {
                    canPerformAction = true;
                }
            }
        }
    }

    public void LookedAt()
    {
        if (!held)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                PickUp(null);
                canPerformAction = false;
            }
        }
    }

    public void PickUp(Transform objToParent)
    {
        ToggleColliders();
        if (objToParent == null)
        {
            //Changed the parent from the player to the Phantom Player object to fix lantern movement with the newly implemented movement
            objToParent = GameObject.Find("PhantomPlayer").transform;
            //objToParent = GameObject.Find("Player").transform;
        }

        Vector3 newPosition = objToParent.localPosition + (objToParent.transform.forward * depthOffset) + (objToParent.transform.up * verticalOffset) + (objToParent.transform.right * horizontalOffset);

        //Changed this to try and have the target become parented to the player instead of the lantern
        parentObject.transform.SetParent(objToParent.transform);

        parentObject.transform.position = newPosition;
        parentObject.transform.rotation = objToParent.transform.localRotation;

        lanternSwayScript.constraintsFrozen = true;
        lanternSwayScript.ToggleFreezeTargetPos();

        gameObject.layer = 2;

        held = true;
        FMODUnity.RuntimeManager.PlayOneShotAttached(itemPickupSound, gameObject);
    }
    void PutDown()
    {
        ToggleColliders();
        //gameObject.transform.SetParent(defaultParent);
        parentObject.transform.SetParent(trueDefaultParent);
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;

        lanternSwayScript.constraintsFrozen = false;
        lanternSwayScript.ToggleFreezeTargetPos();

        gameObject.layer = 0;

        held = false;
        FMODUnity.RuntimeManager.PlayOneShotAttached(itemDropSound, gameObject);
    }
    void Throw()
    {
        ToggleColliders();
        //gameObject.transform.SetParent(defaultParent);
        parentObject.transform.SetParent(trueDefaultParent);
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;

        lanternSwayScript.constraintsFrozen = false;
        lanternSwayScript.ToggleFreezeTargetPos();
        gameObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);

        throwForce = 0;

        gameObject.layer = 0;

        held = false;
    }

    void ToggleColliders()
    {
        foreach(BoxCollider col in colliders)
        {
            if (col.enabled)
            {
                col.enabled = false;
            }
            else
                col.enabled = true;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "LanternFallTrigger")
        {
            PickUp(null);
        }
    }
}
