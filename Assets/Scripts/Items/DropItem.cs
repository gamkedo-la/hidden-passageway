using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour {

	//This is really bad but it does what I want it do. Got to be a better way of doing this.
    //TODO: Have GO spawn inside of appropriate slot and have to the right rotation and pos.
	void Start () {
        WalkControl walker = FindObjectOfType<WalkControl>();
        Transform t = walker.GetComponentInParent<Transform>();
        Transform player = t.GetComponentInParent<Transform>();
        Vector3 pos = new Vector3(player.position.x, player.position.y - 1.0f, player.position.z - 2.0f);

        transform.position = pos;
	}
}
