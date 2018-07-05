using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigSitePuzzle : MonoBehaviour {

    public GameObject puzzleCubePrefab;
    public bool[] cubeState;
    private int gridIndex;
    private int gridCols = 7;
    private int gridRows = 7;

    void Start () {

        cubeState = new bool[gridCols * gridRows];
        CreateCubeGrid(7,7);

    }
	
	void Update () {
		
	}
    
    public void CreateCubeGrid(int cols, int rows) {

        //Temporarily resets puzzle gameObject rotation to (0,0,0), 
        //so that the cubes are created in the correct position, regardless 
        //of the placement orientation of the puzzle prefab in the world.
        Vector3 gridFrameRotation = transform.rotation.eulerAngles;
        Vector3 tempGridFrameRotation = gridFrameRotation;
        tempGridFrameRotation = new Vector3(0,0,0);
        transform.rotation = Quaternion.Euler(tempGridFrameRotation);

        for (int y = 0; y < rows; y++) {
            for (int x = 0; x < cols; x++) {

                gridIndex = ColRowToGridIndex(x, y);
            
                //Create new cube at next position
                GameObject tempGO = GameObject.Instantiate(puzzleCubePrefab);
                tempGO.transform.position = gameObject.transform.position + new Vector3(0.4f * x, 0.4f * y, 0f);

                //Child new cube to this gameObject and set local scale to (1, 1, 1)
                tempGO.transform.SetParent(gameObject.transform);
                Vector3 scale = tempGO.transform.localScale;
                scale.Set(1, 1, 1);
                tempGO.transform.localScale = scale;
                
                //Rename new cube
                tempGO.name = "Cube " + gridIndex;

                //Set grid index on new cube
                tempGO.GetComponent<DigSitePuzzleCube>().SetIndex(gridIndex);

                //Randomize cube state
                if (Random.Range(0f,1f) > 0.6) {
                    cubeState[gridIndex] = true;
                    
                } else { //end if
                    cubeState[gridIndex] = false;
                } // end else

            } // end of for x loop
        } // end of for y loop

        //Sets the puzzle gameObject rotation (along with all the new cubes) back to the intended rotation.
        transform.rotation = Quaternion.Euler(gridFrameRotation);

    } // end of CreateCubeGrid()

    public bool isCubeUnbreakable(int index) {

        if (cubeState[index]) {
            return true;
        } else { // end if cube state is true (unbreakable)
            return false;
        } // end else cube state is false (breakable)

    } // end isCubeUnbreakable

    public int ColRowToGridIndex(int col, int row) {
        return col + (gridCols * row);
    }  // end of ColRowToGridIndex()

} // end of class
