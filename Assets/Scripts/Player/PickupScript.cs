using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour {
    /// <Todo>
    /// Rotation input and test rotation
    /// gamepad controls for player and this script
    /// generalizing script further to pass on to the other level designers
    /// sleep ;-;
    /// 
    /// 
    /// </Todo>


    [SerializeField]
    private float ThrowForce = 10;
    public bool canHold = true;
    public bool hideCol;
    public GameObject item;
    public Collider itemCol;
    public Transform guide;
    public float rotationForce;
    public GameObject exception;

    private void Start()
    {
        exception = GameObject.Find ("Handtorch");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Click");
            if (!canHold)
                throw_drop();
            else
                Pickup();
        }//mause If

        if (!canHold && item)
            item.transform.position = guide.position;

    }//update

    //We can use trigger or Collision
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "item"|| col.gameObject.tag == "keyItem")
            //Debug.Log("Collision Detected With Item");
            if (!item) // if we don't have anything holding
                item = col.gameObject;
    }

    //We can use trigger or Collision
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "item" || col.gameObject.tag == "keyItem")
        {
            if (canHold)
                item = null;
        }
    }

    // This area is for rotation of the item independently of the camera
    private void Rotate()
    {
        transform.Rotate(Vector3.right * rotationForce * Time.deltaTime);
    }
    private void Pickup()
    {
        if (!item)
            return;


        //parent an item to the guide
        item.transform.SetParent(guide);

        //turn off gravity
        item.GetComponent<Rigidbody>().useGravity = false;

        //add cam rotation to keep it straight
        item.transform.localRotation = transform.rotation;
        //get it to center on the guides position
        item.transform.position = guide.position;

        canHold = false;
        itemCol = item.GetComponent<Collider>();
        if (item == exception)
        {
            itemCol.enabled = true;
        }
        else
        {
            itemCol.enabled = false;
        }
    }

    // throwing or dropping method
    private void throw_drop()
    {

        if (!item)
            return;
        if (!(item == exception))
        {
            itemCol.enabled = true;
        }

        //Set our Gravity to true again.
        item.GetComponent<Rigidbody>().useGravity = true;
        // blank out item field to open up space for the next
        item = null;
        //Apply force on throwing
        guide.GetChild(0).gameObject.GetComponent<Rigidbody>().velocity = transform.forward * ThrowForce;

        //Unparent item
        guide.GetChild(0).parent = null;
        canHold = true;
        itemCol.enabled = true;

    }
}