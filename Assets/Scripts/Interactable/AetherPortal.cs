using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AetherPortal : MonoBehaviour {

	[SerializeField] GameObject particles;
	[SerializeField] Transform destination;
	Transform player;
    [SerializeField] private bool particletype;

	void Awake()
	{
		GameObject playerGO = GameObject.FindWithTag(Tags.Player);
		if (playerGO)
		{
			player = playerGO.transform;
		}
        if (particletype == false)
        {
            particles = null;
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
        if (!(particles))
        {
            particles.transform.position = destination.position;
    		Instantiate(particles, player.position, player.rotation, player);
        }

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
