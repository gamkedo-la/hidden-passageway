using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatesTriggersToActivate : MonoBehaviour {

    public EstatePuzzle mainPuzzleScript;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            mainPuzzleScript.FoundPuzzleCube();
            gameObject.SetActive(false);
        }
    }
}
