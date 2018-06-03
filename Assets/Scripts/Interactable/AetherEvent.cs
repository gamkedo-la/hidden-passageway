using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AetherEvent : MonoBehaviour {
    /// <summary>
    /// This script is made for basic collision based event system
    /// ie. player/item is here - thing happens.
    /// rudimentary since I don't know the messaging system and have just the basic idea of transfering info between scripts.
    /// </summary>
    /// 
    /// <Usage>
    /// Plop this puppy on the object with the collider you will be using as the keyhole
    /// direct the guide transform with an empty child object for location
    /// </Usage>
    /// 
    /// <Todo>
    /// Allow for tweaking from outside scripts such as singleton controller
    /// 
    /// </Todo>
    // Use this for initialization

    public bool CanTrigger;
    public bool Finished;
    public bool createItemType;
    public bool openDoorType;
    public bool activateCutscene;
    public GameObject keyItem;
    public GameObject keyHole;
    public Transform guide;
    public GameObject Passageway;
    public GameObject spawnItem;

    




    void Awake()
    {

    }

    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}



    void OnTriggerEnter(Collider col) //Trigger collision fires off KeyTriggered method and logs what it hit
    {
        if (col.gameObject == keyItem && (CanTrigger == true))
            KeyTriggered();
            Debug.Log("Collision Detected With keyItem" + keyItem);
    }

    void KeyTriggered () //Method to trigger either of the 3 types of effects, or all of them.
    {
        if (createItemType == true)
        {

        }
        if (openDoorType)
        {

        }
    }
}
