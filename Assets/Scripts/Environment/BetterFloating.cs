using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterFloating : MonoBehaviour
{
    float originalY;

    public float floatStrength;
    public Vector3 Roto;
    public float rotmult = 1;
    public float overrideFloat;
                                    

    void Start()
    {
        Roto = Vector3.up;
//        rotmult = Random.Range(7, 10);
        this.originalY = this.transform.localPosition.y;
        if (overrideFloat == 0)
        {
            floatStrength = Random.Range(1f, 3f);
        }
        if (overrideFloat > 0 || overrideFloat < 0) 
        {
            floatStrength = overrideFloat;
        }
    }

    void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x,
            originalY + (Mathf.Sin(Time.time) * floatStrength),
            transform.localPosition.z);
        transform.Rotate((Roto * Time.deltaTime * rotmult));
    }
}