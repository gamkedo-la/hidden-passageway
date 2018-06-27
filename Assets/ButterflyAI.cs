using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyAI : MonoBehaviour {

    public GameObject wingsOpen;
    public GameObject wingsClosed;
    public float flapsPerSecond = 4;
    private int flapCounter = 0;

	// Use this for initialization
	void Start () {
        // do nothing if we don't have meshes to use
        if (!wingsOpen) return;
        if (!wingsClosed) return;

        StartCoroutine("Flappy");
    }

    IEnumerator Flappy()
    {
        for (; ; )
        {
            flapCounter++;
            if (flapCounter % 2 == 0) {
                wingsOpen.SetActive(true);
                wingsClosed.SetActive(false);
            }
            else {
                wingsOpen.SetActive(false);
                wingsClosed.SetActive(true);
            }
            yield return new WaitForSeconds(1f / flapsPerSecond);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
