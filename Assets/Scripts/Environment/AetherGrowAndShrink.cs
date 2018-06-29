using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AetherGrowAndShrink : MonoBehaviour {

    [SerializeField]
    private Vector3 originalScale;
    [SerializeField]
    private float floatStrength = 1;
	// Use this for initialization
	void Start ()
    {
        originalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.localScale = new Vector3(originalScale.x + (Mathf.Sin(Time.time) * floatStrength),
            originalScale.y + (Mathf.Sin(Time.time) * floatStrength),
            originalScale.z + (Mathf.Sin(Time.time) * floatStrength));
    }
}
