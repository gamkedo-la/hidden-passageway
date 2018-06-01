using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterFloating : MonoBehaviour
{
    float originalY;

    public float floatStrength;
    public Vector3 Roto;
    public float rotmult = 1;
                                    

    void Start()
    {
        Roto = Vector3.up;
//        rotmult = Random.Range(7, 10);
        floatStrength = Random.Range(1f, 3f);
        this.originalY = this.transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x,
            originalY + (Mathf.Sin(Time.time) * floatStrength),
            transform.position.z);
        transform.Rotate((Roto * Time.deltaTime * rotmult));
    }
}