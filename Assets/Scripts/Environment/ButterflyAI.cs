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
    private float originalX;
    private float originalY;
    private float originalZ;

    public float xStrength = 1f;
    public float yStrength = 1.2333f;
    public float zStrength = 3.3221f;

    public float xSpeed = 0.5432f;
    public float ySpeed = 0.1f;
    public float zSpeed = 0.6666f;

    private GameObject tempGO;

    // Use this for initialization
    void Start()
    {
        // do nothing if we don't have meshes to use
        if (!wingsOpen) return;
        if (!wingsClosed) return;

        tempGO = new GameObject();

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

        // look half a second in the future amd face that point so we are looking "ahead"
        tempGO.transform.position = new Vector3(
            originalX + (Mathf.Sin((Time.time + 0.5f) * xSpeed) * xStrength),
            originalY + (Mathf.Sin((Time.time + 0.5f) * ySpeed) * yStrength),
            originalZ + (Mathf.Sin((Time.time + 0.5f) * zSpeed) * zStrength)
        );

        transform.LookAt(tempGO.transform);
    }

}
