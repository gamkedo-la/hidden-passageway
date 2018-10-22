using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SquashTransition : MonoBehaviour {

    public WalkControl walkToTurnOff;
    public ViewControl lookToTurnOff;
    bool isSquashing = false;
    string toScene = "";
    public static SquashTransition instance;

	// Use this for initialization
	void Start () {
        instance = this;
	}

    public void startTransition(string forScene) {
        if(isSquashing == false) {
            isSquashing = true;
            toScene = forScene;
            walkToTurnOff.enabled = false;
            lookToTurnOff.enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(isSquashing) {
            Vector3 moreSquished = transform.localScale;
            float referenceFramerate = 30.0f;
            float fadeFactor = 0.88f;
            float blend = 1.0f - Mathf.Pow(1.0f - fadeFactor, Time.deltaTime * referenceFramerate);
            moreSquished.y *= blend;

            transform.localScale = moreSquished;
            if(moreSquished.y < 0.005f) {
                SceneManager.LoadScene(toScene);
            }
        }
	}

    void FixedUpdate() {
        if(isSquashing) {
            Vector3 moreSquished = transform.localScale;

            moreSquished.x *= 1.07f;

            transform.localScale = moreSquished;
        }
    }
}
