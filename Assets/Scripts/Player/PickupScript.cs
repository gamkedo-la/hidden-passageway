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

    [SerializeField] private float ThrowForce = 10;
    [SerializeField] private bool canHold = true;
    [SerializeField] private Transform guide;
    [SerializeField] private float rotationForce;
    private GameObject item;
    private Collider itemCollider;
    private GameObject exception;

    private void Start()
    {
        exception = GameObject.Find ("Handtorch");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log("Click");
            if (!canHold)
            {
                ThrowDrop();
            }
            else
            {
                Pickup();
            }
        }//mause If

        if (!canHold && item)
        {
            item.transform.position = guide.position;
        }

    }//update

    //We can use trigger or Collision
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == Tags.Item || col.gameObject.tag == Tags.KeyItem)
        {
            // Debug.Log("Collision Detected With Item");
            if (!item)
            {
                // if we don't have anything holding
                item = col.gameObject;
            }
        }
    }

    //We can use trigger or Collision
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == Tags.Item || col.gameObject.tag == Tags.KeyItem)
        {
            // Debug.Log("TriggerExit Detected With Item");
            if (canHold)
            {
                item = null;
            }
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
        {
            // Debug.Log("No item to pick up");
            return;
        }

        // Parent an item to the guide
        item.transform.SetParent(guide);

        // Turn off gravity and rotation
        Rigidbody itemRb = item.GetComponent<Rigidbody>();
        itemRb.useGravity = false;
        itemRb.freezeRotation = true;

        //add cam rotation to keep it straight
        item.transform.localRotation = transform.rotation;
        //get it to center on the guides position
        item.transform.position = guide.position;

        canHold = false;
        itemCollider = item.GetComponent<Collider>();
        if (item == exception)
        {
            itemCollider.enabled = true;
        }
        else
        {
            itemCollider.enabled = false;
        }
    }

    private void ThrowDrop()
    {
        if (!item)
        {
            return;
        }
        if (item != exception)
        {
            itemCollider.enabled = true;
        }

        // Enable gravity and rotation
        Rigidbody itemRb = item.GetComponent<Rigidbody>();
        itemRb.useGravity = true;
        itemRb.freezeRotation = false;

        // blank out item field to open up space for the next
        item = null;

        //Apply force on throwing
        if (ThrowForce != 0.0f) {
            guide.GetChild(0).gameObject.GetComponent<Rigidbody>().velocity = transform.forward * ThrowForce;
        }

        //Unparent item
        guide.GetChild(0).parent = null;
        canHold = true;
        itemCollider.enabled = true;
    }
}
