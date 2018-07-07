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
            DestroyCubeOnClick();
        } else { // end of if mouse is over this cube
            this.gameObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        } // end of else mouse is not over this cube 

    } // end of ReactOnMouseOver()

    public void DestroyCubeOnClick() {
        if (Input.GetMouseButtonUp(0)) {

            if (!puzzle.IsCubeUnbreakable(puzzleIndex)) {
                    
                Destroy(rhInfo.collider.gameObject);
                puzzle.SetSolutionCheckNeeded();

            } else { //end of if cube is breakable

                //Briefly changes highlight color to red if clicking unbreakable cube.  hardly noticable as is.
                mat.SetColor("_EmissionColor", Color.red);
                this.gameObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", Color.green);

            } //end of else cube is unbreakable

        } // end of if mouse input
    } // end of RemoveBrickOnClick()

    public void SetIndex(int index) {
        puzzleIndex = index;
    } // end of SetIndex()

} // end of class
 