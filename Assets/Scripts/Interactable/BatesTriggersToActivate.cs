using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatesTriggersToActivate : MonoBehaviour {

    public EstatePuzzle mainPuzzleScript;
    string thisName;

	// Use this for initialization
	void Start () {
        thisName = gameObject.name;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            if (thisName == "CubeTrigger")
            {
                mainPuzzleScript.FoundPuzzleCube();
                gameObject.SetActive(false);
            }
            else if(thisName == "LockInTrigger")
            {
                //mainPuzzleScript.wardrobe2.SetActive(false);
                //mainPuzzleScript.wardrobe1.SetActive(true);
                mainPuzzleScript.blockedDoorways[0].SetActive(false);
                //mainPuzzleScript.blockedDoorways[1].SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}
