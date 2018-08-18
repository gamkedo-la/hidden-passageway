using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherVane : MonoBehaviour {
    public Transform vane;
    float angleTarget = 45.0f;
	// Use this for initialization
	void Start () {
        StartCoroutine(ChangeDir());
	}

    IEnumerator ChangeDir()
    {
        while (true)
        {
            angleTarget = Random.Range(0.0f, 360.0f);
            yield return new WaitForSeconds( Random.Range(4.0f, 8.5f));
        }
    }
	
	// Update is called once per frame
	void Update () {
        Quaternion goal = Quaternion.AngleAxis(angleTarget - Mathf.Sin(Time.time * 0.4f) * 4.0f + Mathf.Cos(Time.time * 1.1f) * 9.0f, Vector3.up);
        float spinRate = Time.deltaTime * 0.8f * Quaternion.Angle(vane.transform.rotation,
                                                                   goal);
        vane.transform.rotation = Quaternion.RotateTowards(vane.transform.rotation,
                                                           goal,
                                                           spinRate);
	}
}
