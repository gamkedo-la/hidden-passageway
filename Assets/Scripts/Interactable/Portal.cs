using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

	[SerializeField] GameObject particles;
	[SerializeField] Transform destination;
	Transform player;

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
		particles.transform.position = destination.position;
		Instantiate(particles, player.position, player.rotation, player);
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
