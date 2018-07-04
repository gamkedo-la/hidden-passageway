using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigSitePuzzleCube : MonoBehaviour {

    public float cubeClickDistance = 50.0f; //distance player must be to a puzzle cube to interact
    public int puzzleIndex;
    private GameObject parent;
    private DigSitePuzzle puzzle;
    //public Renderer renderer;
    public Material mat;
    RaycastHit rhInfo;

    // Use this for initialization
    void Start () {
        parent = this.transform.parent.gameObject;
        puzzle = parent.GetComponent<DigSitePuzzle>();
        //renderer = GetComponent<Renderer>();
        mat = GetComponent<Renderer>().material;
        mat.SetColor("_EmissionColor", Color.green);

    } //end of Start()
	
	// Update is called once per frame
	void Update () {

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        int mouseMask = LayerMask.GetMask("DigSitePuzzleCube");

        if (Physics.Raycast(mouseRay, out rhInfo, cubeClickDistance, mouseMask) && rhInfo.collider.gameObject == gameObject) {
            
            gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            RemoveBrickOnClick();

        } else {
            this.gameObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        }
	} //end of Update()

    public void SetIndex(int index) {
        puzzleIndex = index;
    }

    public void RemoveBrickOnClick() {
        if (Input.GetMouseButtonUp(0)) {
            if (rhInfo.collider.gameObject == gameObject) {

                if (!puzzle.isCubeUnbreakable(puzzleIndex)) {
                    Destroy(rhInfo.collider.gameObject);
                } else { //end if
                    mat.SetColor("_EmissionColor", Color.red);
                    this.gameObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                    mat.SetColor("_EmissionColor", Color.green);
                } //end else

            } //end if
        } // end of if mouse input
    } // end of RemoveBrickOnClick()

} // end of class
 