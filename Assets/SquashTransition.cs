using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SquashTransition : MonoBehaviour {
    bool isSquashing = false;
    string toScene = "";
    public static SquashTransition instance;
    public RenderTexture renderTexture;
    private Camera transitionCam;
    public bool expandInstead = false;
    public float speedScale = 1.0f;
    private Material squashMat;
    private float startYSquish;

	// Use this for initialization
	void Start () {
        instance = this;
        transitionCam = transform.parent.GetComponentInChildren<Camera>();
        squashMat = gameObject.GetComponent<Renderer>().material;
        startYSquish = transform.localScale.y;
	}

    public void startTransition(string forScene) {
        if(isSquashing == false) {
            transitionCam.targetTexture = renderTexture;
            GameObject menuGO = GameObject.Find("MenuHub");
            menuGO.SetActive(false);
            GameObject aimerGO = GameObject.Find("Aimer");
            aimerGO.SetActive(false);

            GameObject mouseLookGO = GameObject.Find("MouseLockTip");
            if (mouseLookGO)
            {
                mouseLookGO.SetActive(false);
            }


            isSquashing = true;
            toScene = forScene;

            GameObject playerGO = GameObject.Find("Player");

            WalkControl walkToTurnOff = playerGO.GetComponent<WalkControl>();
            ViewControl lookToTurnOff = Camera.main.GetComponent<ViewControl>();

            if (walkToTurnOff)
            {
                walkToTurnOff.enabled = false;
            }
            if (lookToTurnOff)
            {
                lookToTurnOff.enabled = false;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (isSquashing)
        {
            if (expandInstead == false)
            {
                Vector3 moreSquished = transform.localScale;
                float referenceFramerate = 30.0f;
                float fadeFactor = 0.88f * speedScale;
                float blend = 1.0f - Mathf.Pow(1.0f - fadeFactor, Time.deltaTime * referenceFramerate);
                moreSquished.y *= blend;

                if (moreSquished.y < 0.01f)
                {
                    moreSquished.y = 0.005f;
                    transform.localScale = moreSquished;
                    SceneManager.LoadScene(toScene);
                }
                else
                {
                    transform.localScale = moreSquished;
                }
            }
        }
	}

    void FixedUpdate() {
        if(isSquashing) {
            Vector3 moreSquished = transform.localScale;

            if (expandInstead)
            {
                Color changedAlpha = squashMat.color;
                float targetY = 4.0f;
                changedAlpha.a = 1.0f-((moreSquished.y-startYSquish) / (targetY-startYSquish));
                squashMat.color = changedAlpha;
                moreSquished.x *= 1.009f; //// was 1.4f
                moreSquished.y *= 1.009f;
                transform.Rotate(Vector3.forward, 0.4f);

                if (moreSquished.y > targetY)
                {
                    SceneManager.LoadScene(toScene);
                }
            }
            else
            {
                moreSquished.x *= 1.05f;
            }

            transform.localScale = moreSquished;
        }
    }
}
