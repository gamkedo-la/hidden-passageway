using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideChildrenIfPlayerIsFar : MonoBehaviour {
    Camera camCache;
    bool childrenShowing = true;
	// Use this for initialization
	void Start () {
        camCache = Camera.main;
        childrenShowing = true;

        for (int i = 0; i < transform.childCount; i++)
        {
            Light glowLight = transform.GetChild(i).gameObject.GetComponent<Light>();
            if (glowLight)
            {
                glowLight.enabled = false; // wasn't clear in webGL anyhow, hurt framerate / flickered
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        float distTo = Vector3.Distance( camCache.transform.position, transform.position );
        bool shouldShow = (distTo < 35.0f);

        if (shouldShow != childrenShowing)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(shouldShow);
            }
            childrenShowing = shouldShow;
        }
	}
}
