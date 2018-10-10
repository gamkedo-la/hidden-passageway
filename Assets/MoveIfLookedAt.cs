using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIfLookedAt : MonoBehaviour {
    public Transform moveThis;
    public Transform fromHere;
    public Transform toHere;	

	// Update is called once per frame
	void FixedUpdate () {
        RaycastHit rhInfo;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rhInfo, 4.0f))
        {
            Transform whichTo;
            if (rhInfo.collider == gameObject.GetComponent<Collider>())
            {
                whichTo = toHere;
            }
            else
            {
                whichTo = fromHere;
            }
            moveThis.position = Vector3.Lerp(moveThis.position, whichTo.position, 0.1f);
            moveThis.rotation = Quaternion.Slerp(moveThis.rotation, whichTo.rotation, 0.1f);
        }
	}
}
