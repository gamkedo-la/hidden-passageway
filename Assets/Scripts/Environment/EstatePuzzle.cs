using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstatePuzzle : MonoBehaviour {

    bool greenLight = false;
    public Light lanternLight;

    GameObject[] scribbles;
    public GameObject[] alternateByLight;
    public GameObject[] visibleByLight;

    Color defaultColor;

    public EstatePuzzleCube[] cubes;
    int numberCorrect = 0;

    //DELETE THIS LATER. USED FOR TESTING THE SOLUTION
    public GameObject lightUpWhenCorrect;

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

        //Allows the player to interact with the puzzles by looking at them and pressing a key.
        RaycastHit rhInfo;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rhInfo, 4.0f))
        {
            if(rhInfo.collider.gameObject.tag == "EstatePuzzleCube")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    rhInfo.collider.gameObject.GetComponent<EstatePuzzleCube>().RotateDown();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            ToggleLanternColor();
        }

        //USING THIS FOR TESTING
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (TestSolution())
            {
                lightUpWhenCorrect.SetActive(true);
            }
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
            foreach(GameObject obj in alternateByLight)
            {
                obj.SetActive(false);
            }
            foreach(GameObject obj in visibleByLight)
            {
                obj.GetComponentInChildren<Renderer>().enabled = true;
            }
        }else
        {
            foreach (GameObject scribble in scribbles)
            {
                scribble.SetActive(false);
            }
            foreach (GameObject obj in alternateByLight)
            {
                obj.SetActive(true);
            }
            foreach (GameObject obj in visibleByLight)
            {
                obj.GetComponentInChildren<Renderer>().enabled = false;
            }
        }
    }

    bool TestSolution()
    {
        numberCorrect = 0;
        foreach(EstatePuzzleCube cube in cubes)
        {
            if(cube.solution[cube.currentSelection] == true)
            {
                numberCorrect++;
            }
        }

        if (numberCorrect == cubes.Length)
        {
            return true;
        }
        else
            return false;
    }
}
