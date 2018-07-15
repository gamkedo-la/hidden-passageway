using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AetherVortexPull : MonoBehaviour {
    [SerializeField]
    private float vortexForce = -10.0f;
    [SerializeField]
    private Vector3 expVector;
    [SerializeField]
    private float expRadius = 50;
    [SerializeField]
    private Rigidbody playerRB;
    [SerializeField]
    private bool playerInside;
    [SerializeField]
    private GameObject Vortex;




    // Use this for initialization
    void Start ()
    {
        GameObject player = GameObject.FindWithTag(Tags.Player);
        playerRB = player.GetComponent<Rigidbody>();
        expVector = Vortex.transform.position;
        
        
        

    }

    // Update is called once per frame
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != Tags.Player)
        {
            return;
        }
        playerInside = true;
        StartCoroutine("CD");




    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != Tags.Player)
        {
            return;
        }
        playerInside = false;
        StopAllCoroutines();


    }

    void FixedUpdate ()
    {
		if (playerInside)
        {
           playerRB.AddExplosionForce(vortexForce, expVector, expRadius, 3.0F);
        }

	}
    private IEnumerator CD()
    {
        yield return new WaitForSeconds(1);
        playerInside = false;
        //Vortex.SetActive(false);
        StopCoroutine("CD");
    }
}
