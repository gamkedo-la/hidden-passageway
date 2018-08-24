using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyAI : MonoBehaviour
{

    public GameObject wingsOpen;
    public GameObject wingsClosed;

    // flap (alternate "frames" which are child meshes)
    public float flapsPerSecond = 4f;
    private int flapCounter = 0;

    // float around
    float originalX;
    float originalY;
    float originalZ;

    public float xStrength = 1f;
    public float yStrength = 1.2333f;
    public float zStrength = 3.3221f;

    public float xSpeed = 0.5432f;
    public float ySpeed = 0.1f;
    public float zSpeed = 0.6666f;

    // Use this for initialization
    void Start()
    {
        // do nothing if we don't have meshes to use
        if (!wingsOpen) return;
        if (!wingsClosed) return;

        originalX = transform.position.x;
        originalY = transform.position.y;
        originalZ = transform.position.z;

        StartCoroutine("Flappy");
    }

    // flap every so often
    IEnumerator Flappy()
    {
        for (; ; )
        {
            flapCounter++;
            if (flapCounter % 2 == 0)
            {
                wingsOpen.SetActive(true);
                wingsClosed.SetActive(false);
            }
            else
            {
                wingsOpen.SetActive(false);
                wingsClosed.SetActive(true);
            }
            yield return new WaitForSeconds(1f / flapsPerSecond);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            originalX + (Mathf.Sin(Time.time * xSpeed) * xStrength),
            originalY + (Mathf.Sin(Time.time * ySpeed) * yStrength),
            originalZ + (Mathf.Sin(Time.time * zSpeed) * zStrength)
        );
    }

}
