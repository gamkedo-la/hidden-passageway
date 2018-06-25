using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

	[SerializeField] Transform destination;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag != "Player")
		{
			return;
		}

		other.gameObject.transform.position = destination.position;
		other.gameObject.transform.rotation = destination.rotation;
	}

	void OnDrawGizmosSelected()
	{
        if (destination != null)
		{
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, destination.position);
        }
    }
}
