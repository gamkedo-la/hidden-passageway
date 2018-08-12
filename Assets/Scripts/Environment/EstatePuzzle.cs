using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstatePuzzle : MonoBehaviour {

    bool greenLight = false;
    public Light lanternLight;

    //How long the green light lasts.
    float greenLightTimer = 10;

    public ParticleSystem greenFountain;
    bool fountainActivated = false;

    //Counts to 5. Goes up each time the player discovers a puzzle cube. Once the last one is discovered, the green fountain activates.
    int numOfActivates = 0;

    GameObject[] scribbles;
    public GameObject[] alternateByLight;
    public GameObject[] visibleByLight;

    Color defaultColor;

    public EstatePuzzleCube[] cubes;
    int numberCorrect = 0;

    //DELETE THIS LATER. USED FOR TESTING THE SOLUTION
    public GameObject lightUpWhenCorrect;

    public GameObject wardrobe1;
    public GameObject wardrobe2;

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
                if (Input.GetMouseButtonDown(0))
                {
                    rhInfo.collider.gameObject.GetComponent<EstatePuzzleCube>().RotateDown();
                }
            }
            if(rhInfo.collider.gameObject.name == "GreenLanternFountain")
            {
                if (Input.GetMouseButtonDown(0) && fountainActivated)
                {
                    if (!greenLight)
                    {
                        Debug.Log("COLOR CHANGE!!!!");
                        ToggleLanternColor();
                    }
                }
            }
        }

        if (greenLight)
        {
            if(greenLightTimer > 0)
            {
                greenLightTimer -= Time.deltaTime;
            }
            else
            {
                ToggleLanternColor();
                greenLightTimer = 10;
            }
        }

        /*if (Input.GetKeyDown(KeyCode.L))
        {
            ToggleLanternColor();
        }*/

        //USING THIS FOR TESTING
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (TestSolution())
            {
                SolvePuzzle();
            }
        }

        //USE THIS FOR TESTING THE ROTATION OF WARDROBE WHEN FIRST PUZZLE IS COMPLETED
        if (Input.GetKeyDown(KeyCode.P))
        {
            SolvePuzzle();
        }

    }

    void ToggleLanternColor()
    {
        if (lanternLight.color == defaultColor)
        {
            lanternLight.color = Color.green;
            greenLight = true;
            ToggleScribbleVisibility();
            greenFountain.Stop();
        }
        else
        {
            lanternLight.color = defaultColor;
            greenLight = false;
            ToggleScribbleVisibility();
            greenFountain.Play();
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

    void SolvePuzzle()
    {
        wardrobe1.SetActive(false);
        wardrobe2.SetActive(true);
    }

    void FountainNowUsable()
    {
        fountainActivated = true;
        greenFountain.Play();
    }

    //Triggers when player discovers a puzzle cube
    public void FoundPuzzleCube()
    {
        numOfActivates++;
        if(numOfActivates == 5)
        {
            FountainNowUsable();
        }
    }
}
