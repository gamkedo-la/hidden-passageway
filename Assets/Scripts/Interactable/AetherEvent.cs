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
    /// Finish the three main trigger items
    /// </Todo>
    // Use this for initialization

    public bool CanTrigger;
    public bool Finished;
    public bool createItemType;
    public bool openDoorType;
    public bool meshShowType;
    public bool keyItemShowType;
    public bool particleType;
    public bool activateCutscene;
    public GameObject keyItem;
    public GameObject keyHole;
    public Transform guide;
    public GameObject Passageway;
    public GameObject spawnItem;
    public GameObject showItem;
    public ParticleSystem ps;
    public AetherDoorOpen dc;
    public MeshRenderer mr;

    




    void Awake()
    {

    }

    void Start ()
    {
        
        
        if (openDoorType)
        {
            dc = Passageway.GetComponent<AetherDoorOpen>();
        }
        if (particleType)
        {
            ps = GetComponent<ParticleSystem>();
        }
        if (meshShowType)
        {
            mr = GetComponent<MeshRenderer>();
            //if (!(mr)) 
            //{
            //    Debug.Log("Mesh show type event doesn't have a Mesh Renderer!");
            //}
            //else
            //{
            //    mr = GetComponent<MeshRenderer>();
            //}
        }


    }

    // Update is called once per frame
    void Update ()
    {
		
	}



    void OnTriggerEnter(Collider col) //Trigger collision fires off KeyTriggered method and logs what it hit
    {
        if (col.gameObject == keyItem && (CanTrigger == true))
        {
            KeyTriggered();
            // Debug.Log("Collision Detected With keyItem" + keyItem);
        }
    }

    void KeyTriggered () //Method to trigger either of the 3 types of effects, or all of them.
    {
        CanTrigger = false;
        Finished = true;
        if (createItemType == true)
        {
            Instantiate(spawnItem, guide);
            Debug.Log("Spawning"+spawnItem);
        }
        if (openDoorType)
        {
            dc.StartMoving = true;
            Debug.Log("Opening door"+Passageway);
        }
        if (activateCutscene)
        {
            Debug.Log("Activating cutscene");
        }
        if (particleType == true)
        {
            ps.enableEmission = true;
            StartCoroutine("TilDeath");
            Debug.Log("Emitting Particle");
        }
        if (meshShowType == true)
        {
            mr.enabled = true;
            Debug.Log("Mesh shown");
        }
        if (keyItemShowType == true)
        {
            showItem.active = true;
            Debug.Log("activated" + showItem);
        }
    }
    private IEnumerator TilDeath()
    {
        Debug.Log("Coroutine started");
        yield return new WaitForSeconds(5);
        ps.enableEmission = false;
        Debug.Log("Coroutine finished");
        StopCoroutine("TilDeath");
    }
}
