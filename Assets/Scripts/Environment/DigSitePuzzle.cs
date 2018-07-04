using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigSitePuzzle : MonoBehaviour {

    public GameObject puzzleCubePrefab;
    private int gridIndex;
    private int gridCols = 7;
    private int gridRows = 7;

    public bool[] cubeState;

    // Use this for initialization
    void Start () {
   
        cubeState = new bool[gridCols * gridRows];

        CreateCubeGrid(7,7);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void CreateCubeGrid(int cols, int rows) {

        for (int y = 0; y < rows; y++) {
            for (int x = 0; x < cols; x++) {

                gridIndex = GetGridIndexFromColAndRow(x, y);

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
                if (Random.Range(0f,1f) > 0.5) {
                    cubeState[gridIndex] = true;
                    
                } else {
                    cubeState[gridIndex] = false;
                }

            } // end of for x loop
        } // end of for y loop

    } // end of CreateCubeGrid()

    public bool isCubeUnbreakable(int index) {
        if (cubeState[index]) {
            return true;
        } else {
            return false;
        }
       
    }

    public int GetGridIndexFromColAndRow(int col, int row) {
        return col + (gridCols * row);
    }


} // end of class
