using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WalkControl : MonoBehaviour {
	private Rigidbody rb;
	private bool onGround=true;
    private Vector3 prevValidPosition;
    public bool areFeetLocked = false;
    public float jumpForce = 5.0f;
    public float walkSpeed = 6.0f;
    public float strafeSpeed = 4.0f;
    private float powerUp = 1.0f;

    public static WalkControl instance;

	// Use this for initialization
	void Start () {
        instance = this;
        rb = GetComponent<Rigidbody>();
		Cursor.lockState = CursorLockMode.Locked;

        if(SceneWarp.fromScene != null && SceneWarp.fromScene.Length > 0) {
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
	
	// Update is called once per frame
	void Update () {
        RaycastHit rhInfo;

        prevValidPosition = transform.position;

        if(Input.GetKeyDown(KeyCode.Q)) {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene( SceneManager.GetActiveScene().name );
            return;
        }

        if (Input.GetKeyUp(KeyCode.Escape)) {
			if(Cursor.lockState == CursorLockMode.Locked) {
				Cursor.lockState = CursorLockMode.None;
			} else {
				Cursor.lockState = CursorLockMode.Locked;
			}
		}

        if (ViewControl.instance.paperView.enabled)
        {
            return; // reading, stand still
        }

        if (Cursor.lockState == CursorLockMode.Locked) {
            if (areFeetLocked == false)
            {
                transform.position += transform.forward * Time.deltaTime * walkSpeed *
                    Input.GetAxisRaw("Vertical");
                transform.position += transform.right * Time.deltaTime * strafeSpeed *
                    Input.GetAxisRaw("Horizontal");

                if (onGround && Input.GetKeyDown(KeyCode.Space))
                {
                    rb.velocity = Vector3.up * jumpForce;
                    onGround = false;
                }
            }
			
			transform.Rotate(Vector3.up, Time.deltaTime * 65.0f * Input.GetAxis("Mouse X"));

		} else if(Input.GetMouseButtonDown(0)) {
			Cursor.lockState = CursorLockMode.Locked;
		}

        if (Physics.Raycast(transform.position, Vector3.down, out rhInfo, 3.0f))
        {
            if (rhInfo.collider.gameObject.layer == LayerMask.NameToLayer("Water"))
            {
                transform.position = prevValidPosition; // undoing position if over water
            }
        }
    }
    ///This is powerup code for Aether. Just increases jump height
    ///No idea why this isn't working right, the debug fires, but the jump force refuses to change.
    void OnTriggerEnter(Collider col) //PowerupCode for the Aether - ties into Powerup tag. Just increases jump height.
    {
        if (col.gameObject.tag == "Powerup")
            jumpForce += powerUp;
            Debug.Log("Player picked up powerup adding +" + powerUp + " to a total of " + jumpForce);
    }

    void OnCollisionStay(Collision facts) {
		onGround = true; // currently not distinguishing ground from wall/ceiling/etc.
	}

	void OnTriggerStay(Collider other) {
		if(other.gameObject.layer == LayerMask.NameToLayer("Water")) {
			if(rb.velocity.y < 0.0f) {
				rb.velocity = Vector3.zero;
			}
			rb.AddForce(Vector3.up * Time.deltaTime * 1000.0f);
		}
	}
}
