using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AetherFadeShaderCompanion : MonoBehaviour
{


    public GameObject maze;
    public bool inverter;
    public float radius = 0.5f;
    public float softness = 0.5f;

    void Update()
    {
        Shader.SetGlobalVector("_GLOBALMaskPosition", transform.position);
        Shader.SetGlobalFloat("_GLOBALMaskRadius", radius);
        Shader.SetGlobalFloat("_GLOBALMaskSoftness", softness);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (inverter)
        {
            if (col.gameObject == maze)
            {
                softness = 0.2f;
                radius = 2f;
            }

        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (inverter)
        {
            if (col.gameObject == maze)
            {
                softness = -0.2f;
                radius = 2f;
            }

        }
    }
}