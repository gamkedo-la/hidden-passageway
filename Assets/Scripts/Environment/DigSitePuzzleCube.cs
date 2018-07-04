using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigSitePuzzleCube : MonoBehaviour {

    public float cubeClickDistance = 50.0f; //distance player must be to a puzzle cube to interact
    public int puzzleIndex;
    private GameObject parent;
    private DigSitePuzzle puzzle;

	// Use this for initialization
	void Start () {
        parent = this.transform.parent.gameObject;
        puzzle = parent.GetComponent<DigSitePuzzle>();
    } //end of Start()
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0)) {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rhInfo;
            int mouseMask = LayerMask.GetMask("DigSitePuzzleCube");

            if (Physics.Raycast(mouseRay, out rhInfo, cubeClickDistance, mouseMask)) {
                Debug.Log("Mouse ray hit: " + rhInfo.collider.gameObject.name + " at " + rhInfo.point);

                Debug.Log(puzzleIndex);
                //Debug.Log(puzzle.isCubeUnbreakable(puzzleIndex));
                if (puzzle.isCubeUnbreakable(puzzleIndex)) {
                    Destroy(rhInfo.collider.gameObject);
                } else {
                   //Debug.Log("Cube Unbreakable");
                }   

            } else {
               //Debug.Log("Mouse ray hit nothing");
            }

        } //end of if mouse inpout
	} //end of Update()

    public void SetIndex(int index) {
        puzzleIndex = index;
    }

} // end of class
 