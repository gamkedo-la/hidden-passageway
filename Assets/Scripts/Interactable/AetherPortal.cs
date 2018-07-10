using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AetherPortal : MonoBehaviour {

	[SerializeField] Transform destination;
	private Transform player;

	void Awake()
	{
		GameObject playerGO = GameObject.FindWithTag(Tags.Player);
		if (playerGO)
		{
			player = playerGO.transform;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag != Tags.Player)
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
