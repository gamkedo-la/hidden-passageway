using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportEffect : MonoBehaviour {

	void Start () {
		// Self destruct when done
		Destroy(gameObject, 5f);
	}

	void Update () {

	}
}
