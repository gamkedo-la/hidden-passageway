using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigSitePuzzleCube : MonoBehaviour {

    [SerializeField] private float cubeClickDistance = 50.0f; //distance in WUs player must be to a puzzle cube to interact

	// Use this for initialization
	void Start () {
		
	} //end of Start()
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0)) {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rhInfo;
            int mouseMask = LayerMask.GetMask("DigSitePuzzleCube");

            if (Physics.Raycast(mouseRay, out rhInfo, cubeClickDistance, mouseMask)) {
                Debug.Log("Mouse ray hit: " + rhInfo.collider.gameObject.name + " at " + rhInfo.point);
                Destroy(rhInfo.collider.gameObject);
            } else {
                Debug.Log("Mouse ray hit nothing");
            }

        } //end of if mouse inpout
	} //end of Update()

} // end of class
 