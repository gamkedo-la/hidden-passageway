using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstatePuzzle : MonoBehaviour {

    bool greenLight = false;
    public Light lanternLight;

    public GameObject[] scribbles;

    Color defaultColor;

    private void Awake()
    {
        defaultColor = lanternLight.color;
        scribbles = GameObject.FindGameObjectsWithTag("WallScribbles");
    }

    // Use this for initialization
    void Start () {
        ToggleScribbleVisibility();
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.L))
        {
            ToggleLanternColor();
        }
		
	}

    void ToggleLanternColor()
    {
        if (lanternLight.color == defaultColor)
        {
            lanternLight.color = Color.green;
            greenLight = true;
            ToggleScribbleVisibility();
        }
        else
        {
            lanternLight.color = defaultColor;
            greenLight = false;
            ToggleScribbleVisibility();
        }
    }

    void ToggleScribbleVisibility()
    {
        if(greenLight)
        {
            foreach(GameObject scribble in scribbles)
            {
                scribble.SetActive(true);
            }
        }else
        {
            foreach (GameObject scribble in scribbles)
            {
                scribble.SetActive(false);
            }
        }
    }
}
