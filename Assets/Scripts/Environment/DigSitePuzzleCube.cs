using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigSitePuzzleCube : MonoBehaviour {

    public float cubeClickDistance = 50f; //distance player must be to a puzzle cube to interact (Seems to currently have no effect)
    public int puzzleIndex;
    public Material mat;
    public RaycastHit rhInfo;
    private GameObject parent;
    private DigSitePuzzle puzzle;
    private bool cubeActivated = false;
 
    void Start () {

        //Get reference to the DigSitePuzzle script component of parent puzzle game object
        parent = this.transform.parent.gameObject;
        puzzle = parent.GetComponent<DigSitePuzzle>();

        //Get reference to the cube's Renderer component and set the default emission color to green
        mat = GetComponent<Renderer>().material;
        mat.SetColor("_EmissionColor", Color.green);

    } //end of Start()
	
	void Update () {

        ReactOnMouseOver();

    } //end of Update()

    public void ReactOnMouseOver() {

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        int mouseMask = LayerMask.GetMask("DigSitePuzzleCube");

        if (Physics.Raycast(mouseRay, out rhInfo, cubeClickDistance, mouseMask) && rhInfo.collider.gameObject == gameObject) {
            gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            InteractCubeOnClick();
        } else if (!cubeActivated) { // end of if mouse is over this cube
            this.gameObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        } // end of else mouse is not over this cube 

    } // end of ReactOnMouseOver()

    public void InteractCubeOnClick() {

        if (Input.GetMouseButtonDown(0)) {
            if (cubeActivated) {

                cubeActivated = false;
                mat.SetColor("_EmissionColor", Color.green);

            } else { //end of if

                cubeActivated = true;
                mat.SetColor("_EmissionColor", Color.blue);
                gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

            } //end of else
        } //end of if left-click

        if (Input.GetMouseButtonDown(1)) {
            if (cubeActivated) {

                cubeActivated = false;
                mat.SetColor("_EmissionColor", Color.green);

            } else if (puzzle.IsCubeUnbreakable(puzzleIndex)) { //end of if

                puzzle.ResetPuzzle();

            } else { //end of else if

                Destroy(this.gameObject);
                puzzle.SetSolutionCheckNeeded();

            }//end of else
        } // end of if right-click

    } // end of RemoveBrickOnClick()

    public void SetIndex(int index) {
        puzzleIndex = index;
    } // end of SetIndex()

} // end of class
 