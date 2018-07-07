using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigSitePuzzleTracker : MonoBehaviour {

    public GameObject[] allPuzzles;
    public List<GameObject> solvedPuzzles;

	void Start () {
        allPuzzles = GameObject.FindGameObjectsWithTag("DigSitePuzzle");
	}

    public void MarkSolved(string puzzleName) {
        Debug.Log("Soliution for  object " + puzzleName + " received");
        foreach (GameObject puzzle in allPuzzles) {
            if (puzzle.name == puzzleName) {
                solvedPuzzles.Add(puzzle);
            } // end of if
        } // end of foreach
    } // end of MarkSolved()
}
