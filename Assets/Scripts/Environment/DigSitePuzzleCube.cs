using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigSitePuzzleCube : MonoBehaviour {

    public float cubeClickDistance = 7f; //distance player must be to a puzzle cube to interact (Seems to currently have no effect)
    public int puzzleIndex;
    public Material mat;
    public RaycastHit rhInfo;
    private GameObject parent;
    private DigSitePuzzle puzzle;
    private bool cubeActivated = false;

    private static Camera mainCamCached;
    private Material matCached;
 
    void Start () {
        if (mainCamCached == null)
        {
            GameObject camGO = GameObject.Find("TransitionCam");
            mainCamCached = camGO.GetComponent<Camera>();
        }
        matCached = gameObject.GetComponent<Renderer>().material;
        //Get reference to the DigSitePuzzle script component of parent puzzle game object
        parent = this.transform.parent.gameObject;
        puzzle = parent.GetComponent<DigSitePuzzle>();

        //Get reference to the cube's Renderer component and set the default emission color to green
        mat = GetComponent<Renderer>().material;
        mat.SetColor("_EmissionColor", Color.green);

        matCached.DisableKeyword("_EMISSION");

        if (puzzle.startsCompleted && !puzzle.IsCubeUnbreakable(puzzleIndex)) {
            Destroy(this.gameObject);
        }

    } //end of Start()
	
	void Update () {

        //If puzzle is not solved yet, react to Mouse over.
        if (puzzle.puzzleSolved == false) {
            ReactOnMouseOver();
        } else { //else if puzzle is already solved, change color to yellow.
            mat.SetColor("_EmissionColor", Color.yellow);
            matCached.EnableKeyword("_EMISSION");
        }

    } //end of Update()

    public void ReactOnMouseOver() {

        if (Vector3.Distance(mainCamCached.transform.position, transform.position) > cubeClickDistance)
        {
            return; // avoid majority of raycast checks
        }

           Ray mouseRay = new Ray(mainCamCached.transform.position, mainCamCached.transform.forward);
        int mouseMask = LayerMask.GetMask("DigSitePuzzleCube");

        if (Physics.Raycast(mouseRay, out rhInfo, cubeClickDistance, mouseMask) && rhInfo.collider.gameObject == gameObject) {
            matCached.EnableKeyword("_EMISSION");
            InteractCubeOnClick();
        } else if (!cubeActivated) { // end of if mouse is over this cube
            matCached.DisableKeyword("_EMISSION");
        } // end of else mouse is not over this cube 

    } // end of ReactOnMouseOver()

    public void InteractCubeOnClick() {

        if (Input.GetButtonDown("Fire1")) {
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/MainHub/OtherSwitch", gameObject);
            if (cubeActivated) {

                cubeActivated = false;
                mat.SetColor("_EmissionColor", Color.green);

            } else { //end of if

                cubeActivated = true;
                mat.SetColor("_EmissionColor", Color.blue);
                matCached.EnableKeyword("_EMISSION");

            } //end of else
        } //end of if left-click

        if (Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.E)){
            if (cubeActivated) {

                cubeActivated = false;
                mat.SetColor("_EmissionColor", Color.green);
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/MainHub/OtherSwitch", gameObject);

            } else if (puzzle.IsCubeUnbreakable(puzzleIndex)) { //end of if
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/MainHub/ScrapLook", gameObject);
                puzzle.ResetPuzzle();

            } else { //end of else if
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/DigSite/Dig", gameObject);
                Destroy(this.gameObject);
                puzzle.SetSolutionCheckNeeded();

            }//end of else
        } // end of if right-click or E press

        //Cheat key. Press C on a puzzle to immediately solve it.
        /*if (Input.GetKeyDown(KeyCode.C)) {
            puzzle.startsCompleted = true;
            puzzle.ResetPuzzle();
            puzzle.SetSolutionCheckNeeded();
        } */ //end if

    } // end of RemoveBrickOnClick()

    public void SetIndex(int index) {
        puzzleIndex = index;
    } // end of SetIndex()

} // end of class
 