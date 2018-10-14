using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class WalkControl : MonoBehaviour {
	[HideInInspector]
    public Rigidbody rb;
	private bool onGround=true;
    private Vector3 prevValidPosition;
    public bool areFeetLocked = false;
    public float jumpForce = 5.0f;
    public float walkSpeed = 6.0f;
    public float strafeSpeed = 4.0f;
    public float speedFalloffAmt = 0.9f; // friction only for lateral motion

    public float suchLowYMustHaveFallenThroughFloor = -150.0f;
    Vector3 lastKnownSafelyOnGround = Vector3.zero;

	private Vector3 forward, right;

    //private float powerUp = 1.0f;

    public static WalkControl instance;

	// Use this for initialization
	void Start () {
        lastKnownSafelyOnGround = transform.position;
        instance = this;
        rb = GetComponent<Rigidbody>();
		Cursor.lockState = CursorLockMode.Locked;

        if(SceneWarp.fromScene != null && SceneWarp.fromScene.Length > 0) {
            Debug.Log("FROM SCENE: " + SceneWarp.fromScene);
            GameObject[] warpGOs = GameObject.FindGameObjectsWithTag("Teleporter");
            for (int i = 0; i < warpGOs.Length;i++) {
                SceneWarp swScript = warpGOs[i].GetComponent<SceneWarp>();
                if(swScript.sceneName == SceneWarp.fromScene) {
                    transform.position = swScript.returnLocation.position;
                    Vector3 focusFixedAtEyeHeight = swScript.transform.position;
                    focusFixedAtEyeHeight.y = transform.position.y;
                    transform.LookAt(focusFixedAtEyeHeight);
                    break;
                }
            }
        }
	}

	void FixedUpdate()
	{
        if (ArcadePlayer.playingNow != null)
        {
            return;
        }
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (ViewControl.instance.paperView.enabled)
        {
            rb.velocity = Vector3.zero;
            return; // reading, stand still
        }
        if (areFeetLocked)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            if (areFeetLocked == false)
            {
				if (onGround)
				{
					forward = transform.forward;
					right = transform.right;
				}
                Vector3 lateralDecay = rb.velocity;
                lateralDecay.x *= speedFalloffAmt;
                lateralDecay.z *= speedFalloffAmt;
                rb.velocity = lateralDecay;
                float scaleForCompatibilityWithOlderTuning = 4.0f; // added to keep pre-physics walk tuning numbers
                rb.velocity += forward * Time.deltaTime * walkSpeed * scaleForCompatibilityWithOlderTuning *
                    Input.GetAxisRaw("Vertical");
                rb.velocity += right * Time.deltaTime * strafeSpeed * scaleForCompatibilityWithOlderTuning *
                    Input.GetAxisRaw("Horizontal");

                if (onGround && Input.GetKeyDown(KeyCode.Space))
                {
                    onGround = false;
                    rb.velocity += Vector3.up * jumpForce;
                }
            }

            transform.Rotate(Vector3.up, Time.deltaTime * 65.0f * Input.GetAxis("Mouse X"));

        }
        else if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
	}

	// Update is called once per frame
	void Update () {
        if (ArcadePlayer.playingNow != null)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        RaycastHit rhInfo;

        prevValidPosition = transform.position;

        /*if(Input.GetKeyDown(KeyCode.Q)) {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene( SceneManager.GetActiveScene().name );
            return;
        }*/

        /*if (Input.GetKeyUp(KeyCode.Escape)) { // now handled by exit widget
			if(Cursor.lockState == CursorLockMode.Locked) {
				Cursor.lockState = CursorLockMode.None;
			} else {
				Cursor.lockState = CursorLockMode.Locked;
			}
		}*/

        if (ViewControl.instance.paperView.enabled)
        {
            return; // reading, stand still
        }

        if (Physics.Raycast(transform.position, Vector3.down, out rhInfo, 3.0f))
        {
			if (rhInfo.collider != null)
			{
				if (rhInfo.collider.gameObject.layer == LayerMask.NameToLayer("Water"))
				{
					transform.position = prevValidPosition; // undoing position if over water
				}
			}
        }

		//
        if (Physics.Raycast(transform.position, Vector3.down, out rhInfo, 1.2f))
        {
			if (rhInfo.collider != null)
			{
				onGround = true;
				lastKnownSafelyOnGround = transform.position;
				forward = Vector3.Cross(transform.right, rhInfo.normal).normalized;
				right = Vector3.Cross(-transform.forward, rhInfo.normal).normalized;
				if (Input.GetAxisRaw("Vertical") == 0f && Input.GetAxisRaw("Horizontal") == 0f)
				{
					//Magic number (1.041f) comes from the following line:
					//Debug.Log(Vector3.Distance(transform.position, rhInfo.point));
					transform.position = new Vector3(rhInfo.point.x, rhInfo.point.y + 1.045f, rhInfo.point.z);
					rb.velocity = Vector3.zero;
				}
			}
        }
        else
        {
            onGround = false;
            if (transform.position.y < suchLowYMustHaveFallenThroughFloor)
            {
                Debug.Log("Fell through or off world edge, resetting to last ground touch");
                Debug.Log("If this shouldn't have happened or fell too far, set lastKnownSafelyOnGround");
                transform.position = lastKnownSafelyOnGround;
            }
        }
    }
    ///This is powerup code for Aether. Just increases jump height
    ///No idea why this isn't working right, the debug fires, but the jump force refuses to change.
    //void OnTriggerEnter(Collider col) //PowerupCode for the Aether - ties into Powerup tag. Just increases jump height.
    //{
    //    if (col.gameObject.tag == "Powerup")
    //        jumpForce += powerUp;
    //        Debug.Log("Player picked up powerup adding +" + powerUp + " to a total of " + jumpForce);
    //}

    /*
    void OnCollisionStay(Collision facts) {
		onGround = true; // currently not distinguishing ground from wall/ceiling/etc.
	}*/

	void OnTriggerStay(Collider other) {
		if(other.gameObject.layer == LayerMask.NameToLayer("Water")) {
			if(rb.velocity.y < 0.0f) {
                Vector3 fixedY = rb.velocity;
                fixedY.y = 0.0f;
                rb.velocity = fixedY;
			}
			rb.AddForce(Vector3.up * Time.deltaTime * 1000.0f);
		}
	}
}
