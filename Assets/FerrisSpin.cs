using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FerrisSpin : MonoBehaviour {
	void Update () {
        transform.Rotate(Vector3.right, 10.0f * Time.deltaTime);
	}
}
