using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderPosition : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalVector("_Position", transform.position);
    }
}