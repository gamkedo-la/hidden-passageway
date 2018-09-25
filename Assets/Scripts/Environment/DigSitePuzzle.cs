using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigSitePuzzle : MonoBehaviour {

    public GameObject puzzleCubePrefab;
    public List<GameObject> breakableCubes;
    public bool[] cubeState;
    private int gridIndex;
    public int gridCols = 7;
    public int gridRows = 7;
    private GameObject parent;
    private DigSitePuzzleTracker puzzleTracker;
    public bool solutionCheckNeeded = false;
    public List<int>[] clues;
    public GameObject[] numberPlates;
    public GameObject doorToOpen;
    private SlideToPos doorControl;
    public float percentBreakableCube = 40f;
    public enum puzzlePatternName { RANDOM, BOX, CROSS, KEYS, CAT, FACE, SUN, TEMPLE, KEY, EYE, CROOK, COLUMN, CHALICE, 
                                    PATTERN3X3_1, PATTERN3X3_2, PATTERN4X4_1, PATTERN4X4_2};
    public puzzlePatternName thisPattern;
    public bool startsCompleted = false;
    public bool cluesHidden = false;

    private int[] pattern3x3_1 = new int[]   {0, 0, 0, 
                                              1, 1, 1, 
                                              0, 0, 0 };

    private int[] pattern3x3_2 = new int[]   {0, 1, 0,
                                              1, 0, 1,
                                              0, 1, 0 };

    private int[] pattern4x4_1 = new int[]   {1, 1, 1, 1,
                                              0, 1, 0, 1,
                                              1, 1, 1, 1,
                                              0, 1, 0, 0};

    private int[] pattern4x4_2 = new int[]   {1, 0, 1, 1,
                                              1, 1, 0, 0,
                                              0, 1, 0, 1,
                                              0, 0, 0, 1};

    private int[] boxPattern = new int[]   {0, 0, 0, 0, 0, 0, 0,
                                            0, 1, 1, 1, 1, 1, 0,
                                            0, 1, 0, 0, 0, 1, 0,
                                            0, 1, 0, 0, 0, 1, 0,
                                            0, 1, 0, 0, 0, 1, 0,
                                            0, 1, 1, 1, 1, 1, 0,
                                            0, 0, 0, 0, 0, 0, 0 };

    private int[] crossPattern = new int[] {1, 0, 0, 0, 0, 0, 1,
                                            0, 1, 0, 0, 0, 1, 0,
                                            0, 0, 1, 0, 1, 0, 0,
                                            0, 0, 0, 1, 0, 0, 0,
                                            0, 0, 1, 0, 1, 0, 0,
                                            0, 1, 0, 0, 0, 1, 0,
                                            1, 0, 0, 0, 0, 0, 1 };

    private int[] keysPattern = new int[] { 1, 1, 1, 0, 0, 0, 0,
                                            1, 0, 1, 1, 1, 1, 1,
                                            1, 1, 1, 0, 1, 0, 1,
                                            0, 0, 0, 0, 0, 0, 0,
                                            0, 0, 0, 0, 1, 1, 1,
                                            1, 1, 1, 1, 1, 0, 1,
                                            1, 0, 1, 0, 1, 1, 1 };

    private int[] catPattern = new int[] {  0, 1, 0, 0, 0, 1, 0,
                                            0, 1, 1, 0, 1, 1, 0,
                                            0, 1, 1, 1, 1, 1, 0,
                                            1, 1, 0, 1, 0, 1, 1,
                                            1, 1, 1, 1, 1, 1, 1,
                                            1, 1, 1, 0, 1, 1, 1,
                                            0, 1, 0, 1, 0, 1, 0 };

    private int[] facePattern = new int[] { 0, 1, 1, 1, 1, 1, 0,
                                            1, 1, 0, 1, 0, 1, 1,
                                            1, 1, 1, 1, 1, 1, 1,
                                            1, 1, 1, 0, 1, 1, 1,
                                            0, 1, 1, 1, 1, 1, 0,
                                            0, 1, 0, 0, 0, 1, 0,
                                            0, 0, 1, 1, 1, 0, 0 };

    private int[] sunPattern = new int[] {  0, 0, 0, 0, 0, 0, 0,
                                            0, 1, 0, 1, 0, 1, 0,
                                            0, 0, 1, 1, 1, 0, 0,
                                            0, 1, 1, 1, 1, 1, 0,
                                            0, 0, 1, 1, 1, 0, 0,
                                            0, 1, 0, 1, 0, 1, 0,
                                            0, 0, 0, 0, 0, 0, 0 };

    private int[] templePattern = new int[] {   0, 0, 0, 0, 0, 0, 0,
                                                0, 0, 0, 0, 0, 0, 0,
                                                0, 0, 0, 0, 0, 0, 0,
                                                0, 0, 0, 1, 0, 0, 0,
                                                0, 0, 1, 1, 1, 0, 0,
                                                0, 1, 1, 1, 1, 1, 0,
                                                1, 1, 1, 1, 1, 1, 1 };

    private int[] keyPattern = new int[] {   0, 0, 0, 0, 0, 0, 0,
                                             0, 0, 0, 0, 0, 0, 0,
                                             1, 1, 1, 0, 0, 0, 0,
                                             1, 0, 1, 1, 1, 1, 1,
                                             1, 1, 1, 0, 1, 0, 1,
                                             0, 0, 0, 0, 0, 0, 0,
                                             0, 0, 0, 0, 0, 0, 0 };

    private int[] eyePattern = new int[] {   0, 0, 0, 0, 0, 0, 0,
                                             0, 0, 1, 1, 1, 0, 0,
                                             0, 1, 1, 0, 1, 1, 0,
                                             1, 0, 1, 1, 1, 0, 1,
                                             0, 1, 0, 0, 0, 1, 0,
                                             0, 0, 1, 1, 1, 0, 0,
                                             0, 0, 0, 0, 0, 0, 0 };

    private int[] crookPattern = new int[] {   0, 0, 1, 1, 1, 1, 0,
                                               0, 1, 0, 0, 0, 1, 0,
                                               0, 1, 0, 0, 0, 1, 0,
                                               0, 0, 0, 0, 1, 0, 0,
                                               0, 0, 0, 1, 0, 0, 0,
                                               0, 0, 0, 1, 0, 0, 0,
                                               0, 0, 1, 1, 1, 0, 0 };

    private int[] columnPattern = new int[] {   0, 1, 1, 1, 1, 1, 0,
                                                0, 0, 1, 1, 1, 0, 0,
                                                0, 0, 1, 1, 1, 0, 0,
                                                0, 0, 1, 1, 1, 0, 0,
                                                0, 0, 1, 1, 1, 0, 0,
                                                0, 0, 1, 1, 1, 0, 0,
                                                0, 1, 1, 1, 1, 1, 0 };

    private int[] chalicePattern = new int[] {   1, 1, 1, 1, 1, 1, 1,
                                                 1, 1, 1, 1, 1, 1, 1,
                                                 0, 1, 1, 1, 1, 1, 0,
                                                 0, 0, 1, 1, 1, 0, 0,
                                                 0, 0, 0, 1, 0, 0, 0,
                                                 0, 0, 1, 1, 1, 0, 0,
                                                 0, 1, 1, 1, 1, 1, 0 };

    void Start () {

        parent = this.transform.parent.gameObject;
        puzzleTracker = parent.GetComponent<DigSitePuzzleTracker>();
        doorControl = doorToOpen.GetComponent<SlideToPos>();

        cubeState = new bool[gridCols * gridRows];
        //numberPlates = new GameObject[10];
        CreateCubeGrid(gridCols,gridRows);
        
    }
	
	void Update () {
        if (solutionCheckNeeded) {
            CheckForSolution();
            Debug.Log("Calling CheckForSolution()");
            solutionCheckNeeded = false;
        }
    }
    
    public void CreateCubeGrid(int cols, int rows) {

        Debug.Log(cols + " x " + rows);
        //Temporarily resets puzzle gameObject rotation to (0,0,0), 
        //so that the cubes are created in the correct position, regardless 
        //of the placement orientation of the puzzle prefab in the world.
        Vector3 gridFrameRotation = transform.rotation.eulerAngles;
        Vector3 tempGridFrameRotation = gridFrameRotation;
        tempGridFrameRotation = new Vector3(0,0,0);
        transform.rotation = Quaternion.Euler(tempGridFrameRotation);

        for (int y = rows - 1; y >= 0; y--) {
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

                switch (thisPattern) {
                    case puzzlePatternName.RANDOM:

                        //Randomize cube state
                        if (Random.Range(0f, 1f) > (percentBreakableCube / 100)) {
                            cubeState[gridIndex] = true;
                        } else { //end if
                            cubeState[gridIndex] = false;
                            breakableCubes.Add(tempGO);
                        } // end else
                        break;

                    case puzzlePatternName.BOX:

                        //Patterned cube state
                        if (boxPattern[gridIndex] == 1) {
                            cubeState[gridIndex] = true;
                        } else { //end if
                            cubeState[gridIndex] = false;
                            breakableCubes.Add(tempGO);
                        }
                        break;

                    case puzzlePatternName.CROSS:

                        //Patterned cube state
                        if (crossPattern[gridIndex] == 1) {
                            cubeState[gridIndex] = true;
                        } else { //end if
                            cubeState[gridIndex] = false;
                            breakableCubes.Add(tempGO);
                        }
                        break;

                    case puzzlePatternName.KEYS:

                        //Patterned cube state
                        if (keysPattern[gridIndex] == 1) {
                            cubeState[gridIndex] = true;
                        } else { //end if
                            cubeState[gridIndex] = false;
                            breakableCubes.Add(tempGO);
                        }
                        break;

                    case puzzlePatternName.CAT:

                        //Patterned cube state
                        if (catPattern[gridIndex] == 1) {
                            cubeState[gridIndex] = true;
                        } else { //end if
                            cubeState[gridIndex] = false;
                            breakableCubes.Add(tempGO);
                        }
                        break;

                    case puzzlePatternName.FACE:

                        //Patterned cube state
                        if (facePattern[gridIndex] == 1) {
                            cubeState[gridIndex] = true;
                        } else { //end if
                            cubeState[gridIndex] = false;
                            breakableCubes.Add(tempGO);
                        }
                        break;

                    case puzzlePatternName.SUN:

                        //Patterned cube state
                        if (sunPattern[gridIndex] == 1) {
                            cubeState[gridIndex] = true;
                        } else { //end if
                            cubeState[gridIndex] = false;
                            breakableCubes.Add(tempGO);
                        }
                        break;

                    case puzzlePatternName.TEMPLE:

                        //Patterned cube state
                        if (templePattern[gridIndex] == 1) {
                            cubeState[gridIndex] = true;
                        } else { //end if
                            cubeState[gridIndex] = false;
                            breakableCubes.Add(tempGO);
                        }
                        break;

                    case puzzlePatternName.KEY:

                        //Patterned cube state
                        if (keyPattern[gridIndex] == 1) {
                            cubeState[gridIndex] = true;
                        } else { //end if
                            cubeState[gridIndex] = false;
                            breakableCubes.Add(tempGO);
                        }
                        break;

                    case puzzlePatternName.EYE:

                        //Patterned cube state
                        if (eyePattern[gridIndex] == 1) {
                            cubeState[gridIndex] = true;
                        } else { //end if
                            cubeState[gridIndex] = false;
                            breakableCubes.Add(tempGO);
                        }
                        break;

                    case puzzlePatternName.CROOK:

                        //Patterned cube state
                        if (crookPattern[gridIndex] == 1) {
                            cubeState[gridIndex] = true;
                        } else { //end if
                            cubeState[gridIndex] = false;
                            breakableCubes.Add(tempGO);
                        }
                        break;

                    case puzzlePatternName.COLUMN:

                        //Patterned cube state
                        if (columnPattern[gridIndex] == 1) {
                            cubeState[gridIndex] = true;
                        } else { //end if
                            cubeState[gridIndex] = false;
                            breakableCubes.Add(tempGO);
                        }
                        break;

                    case puzzlePatternName.CHALICE:

                        //Patterned cube state
                        if (chalicePattern[gridIndex] == 1) {
                            cubeState[gridIndex] = true;
                        } else { //end if
                            cubeState[gridIndex] = false;
                            breakableCubes.Add(tempGO);
                        }
                        break;

                    case puzzlePatternName.PATTERN3X3_1:

                        //Patterned cube state
                        if (pattern3x3_1[gridIndex] == 1) {
                            cubeState[gridIndex] = true;
                        } else { //end if
                            cubeState[gridIndex] = false;
                            breakableCubes.Add(tempGO);
                        }
                        break;

                    case puzzlePatternName.PATTERN3X3_2:

                        //Patterned cube state
                        if (pattern3x3_2[gridIndex] == 1) {
                            cubeState[gridIndex] = true;
                        } else { //end if
                            cubeState[gridIndex] = false;
                            breakableCubes.Add(tempGO);
                        }
                        break;

                    case puzzlePatternName.PATTERN4X4_1:

                        //Patterned cube state
                        if (pattern4x4_1[gridIndex] == 1) {
                            cubeState[gridIndex] = true;
                        } else { //end if
                            cubeState[gridIndex] = false;
                            breakableCubes.Add(tempGO);
                        }
                        break;

                    case puzzlePatternName.PATTERN4X4_2:

                        //Patterned cube state
                        if (pattern4x4_2[gridIndex] == 1) {
                            cubeState[gridIndex] = true;
                        } else { //end if
                            cubeState[gridIndex] = false;
                            breakableCubes.Add(tempGO);
                        }
                        break;

                }

            } // end of for x loop
        } // end of for y loop

        if (!cluesHidden) {
            CreateRowColClues(gridCols, gridRows);
        }

        //Sets the puzzle gameObject rotation (along with all the new cubes) back to the intended rotation.
        transform.rotation = Quaternion.Euler(gridFrameRotation);

    } // end of CreateCubeGrid()

    public void ResetPuzzle() {

        //Destroy all existing cubes and number plates in puzzle before creating a new one.
        Transform[] children = gameObject.GetComponentsInChildren<Transform>(true);
        foreach (Transform item in children) {

            if (item.gameObject != this.gameObject
                && item.gameObject.name != "3x3_Puzzle_Frame"
                && item.gameObject.name != "7x7_Puzzle_Frame"
                && item.gameObject.name != "4x4_Puzzle_Frame"
                && item.gameObject.name != "10x10_Puzzle_Frame") {
                GameObject.Destroy(item.gameObject);
            } // end of if
               
        } // end of foreach

        CreateCubeGrid(gridCols, gridRows);

    } // end of ResetPuzzle()

    public bool IsCubeUnbreakable(int index) {

        if (cubeState[index]) {
            return true;
        } else { // end if cube state is true (unbreakable)
            return false;
        } // end else cube state is false (breakable)

    } // end isCubeUnbreakable

    public int ColRowToGridIndex(int col, int row) {
        return col + (gridCols * (gridRows - row)) - gridRows;
    }  // end of ColRowToGridIndex()

    public void CheckForSolution() {

        //Remove destroyed cubes from list
        breakableCubes.RemoveAll(GameObject => GameObject == null);

        Debug.Log(breakableCubes);
        if (breakableCubes.Count == 0) {
            puzzleTracker.MarkSolved(gameObject.name);
            Debug.Log("Solution Found. Calling MarkSolved() for object " + gameObject.name);
            doorControl.Activate();
        } // end of if

    } // end of CheckForSolution()

    public void SetSolutionCheckNeeded() {
        solutionCheckNeeded = true;
    } // end of SetSolutionCheckNeeded()

    public void CreateRowColClues(int rows, int cols) {

        int consecutiveCubes = 0;

        clues = new List<int>[rows + cols];
        for (int i = 0; i < clues.Length; i++) {
            clues[i] = new List<int>();
        }

        for (int y = 0; y < rows; y++) {
            for (int x = 0; x < cols; x++) {
                //int lastClueIndex = clues[y].Count - 1;
                if (IsCubeUnbreakable(ColRowToGridIndex(x, y))) {
                    consecutiveCubes++;
                    if (x == cols - 1) {
                        clues[y].Add(consecutiveCubes);
                        consecutiveCubes = 0;
                    } // end of if last cube in row
                } else if (consecutiveCubes != 0) { //end of if cube is unbreakable
                    clues[y].Add(consecutiveCubes);
                    consecutiveCubes = 0;
                }

                if (clues[y].Count < 1 && x == cols - 1){
                    clues[y].Add(0);  // end of else if
                }

            } // end of for x
        } //end of for y

        for (int x = 0; x < cols; x++) {
            for (int y = 0; y < rows; y++) {
                if (IsCubeUnbreakable(ColRowToGridIndex(x, y))) {
                    consecutiveCubes++;
                    if (y == rows - 1) {
                        clues[x + rows].Add(consecutiveCubes);
                        consecutiveCubes = 0;
                    } //end of if last cube in col
                } else if (consecutiveCubes != 0) { //end of if cube is unbreakable
                    clues[x + rows].Add(consecutiveCubes);
                    consecutiveCubes = 0;
                } // end of else if

                if (clues[x + rows].Count < 1 && y == rows - 1) {
                    clues[x + rows].Add(0);  // end of else if
                }

            } // end of for y
        } // end of for x

        //Create number plates for each row
        for (int y = 0; y < rows; y++) {
            //Debug.Log("Number of clues in row y" + (y+1) +": " + clues[y].Count);
            for (int i = 0; i < clues[y].Count; i++) {
                Debug.Log("Clue " + i+1 + " in row y" + (y + 1) + ": " + clues[y][i]);

                GameObject tempPlateGO = GameObject.Instantiate(numberPlates[clues[y][(clues[y].Count - 1) - i]]);

                tempPlateGO.transform.position = gameObject.transform.position + new Vector3(0.4f * (-0.4f * i) - 1,  (0.4f * y) - 0.2f , 0f);

                //Child new plate to this gameObject and set local scale to (.1, .1, .1)
                tempPlateGO.transform.SetParent(gameObject.transform);
                Vector3 scale = tempPlateGO.transform.localScale;
                scale.Set(0.1f, 0.1f, 0.1f);
                tempPlateGO.transform.localScale = scale;

                //Rename new plate
                tempPlateGO.name = "Plate y" + (y+1) + " " + (i+1);
            }
        }

        //Create number plates for each col
        for (int x = 0; x < cols; x++) {
            //Debug.Log("Number of clues in col x" + (x + 1) + ": " + clues[x + rows].Count);
            for (int i = 0; i < clues[x + rows].Count; i++) {
                //Debug.Log("Clue " + i + 1 + " in col x" + (x + 1) + ": " + clues[x + rows][i]);

                GameObject tempPlateGO = GameObject.Instantiate(numberPlates[clues[x + rows][i]]);
           
                tempPlateGO.transform.position = gameObject.transform.position + new Vector3(0.4f * x, (0.4f * i) + ((gridRows + 1) * .4f), 0f);

                //Child new plate to this gameObject and set local scale to (.1, .1, .1)
                tempPlateGO.transform.SetParent(gameObject.transform);
                Vector3 scale = tempPlateGO.transform.localScale;
                scale.Set(0.1f, 0.1f, 0.1f);
                tempPlateGO.transform.localScale = scale;

                //Rename new plate
                tempPlateGO.name = "Plate x" + (x + 1) + " " + (i + 1);

            }
        }


    }
} // end of class
