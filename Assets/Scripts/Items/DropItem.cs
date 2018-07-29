using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DropItem : MonoBehaviour {

	//This is really bad but it does what I want it do. Got to be a better way of doing this.
    //TODO: Have GO spawn inside of appropriate slot and have to the right rotation and pos.
	void Start () {
        WalkControl walker = FindObjectOfType<WalkControl>();
        Assert.IsNotNull(walker, "Where's the walker");
        Transform t = walker.GetComponentInParent<Transform>();
        Assert.IsNotNull(t, "Can't get transform. Might have something to due with walker");
        Transform player = t.GetComponentInParent<Transform>();
        Assert.IsNotNull(player, "No t means no player transform");
        Vector3 pos = new Vector3(player.position.x, player.position.y - 1.0f, player.position.z - 2.0f);

        if(pos != null)
        {
            transform.position = pos;
        }  
	}
}
