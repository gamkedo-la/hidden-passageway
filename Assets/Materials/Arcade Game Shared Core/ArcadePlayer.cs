using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArcadePlayer : MonoBehaviour {
    public static PlayableGame playingNow = null;

	Coroutine prevMsgReset = null;

	Rigidbody rb;
	public static int ticketNum = 0;
	public int bills = 0;
	public int tokens = 0;

	public float zoomFOV = 45.0f;
	private float baseFOV;
	private float camTilt;

	IEnumerator resetMessage() {
		yield return new WaitForSeconds(1.25f);
		tokenBillsChange(0,0);
	}

	string pluralString(int count, string rootWord) {
		if(count == 1) {
			return count+" "+rootWord;
		} else {
			return count+" "+rootWord+"s";
		}
	}

	public bool tokenBillsChange(int billDelta, int tokenDelta) {
		bool hadEnoughTokens = false;

		if(playingNow != null) {
			return hadEnoughTokens;
		}

		string disclaimer = "";

		if(prevMsgReset != null) {
			StopCoroutine(prevMsgReset);
			prevMsgReset = null;
		}

		if(bills + billDelta < 0) {
			disclaimer = "\nNeed bills!";
			prevMsgReset = StartCoroutine( resetMessage() );
		} else if(tokens + tokenDelta < 0) {
			disclaimer = "\nNeed tokens!";
			prevMsgReset = StartCoroutine( resetMessage() );
		} else {
			bills += billDelta;
			tokens += tokenDelta;

			if(billDelta < 0) {
				disclaimer = "\nSpent "+pluralString(-billDelta,"Bill");
			}
			else if(billDelta > 0) {
				disclaimer = "\nGot "+pluralString(billDelta,"Bill");
			}

			/*if(tokenDelta < 0) {
				SoundCenter.instance.PlayClipOn( SoundCenter.instance.coinSpend,
				                                transform.position, 1.0f);
				disclaimer += "\nSpent "+pluralString(-tokenDelta,"Token");
				hadEnoughTokens = true;
			}
			else if(tokenDelta > 0) {
				SoundCenter.instance.PlayClipOn( SoundCenter.instance.coinGet,
				                                transform.position, 1.0f);
				disclaimer += "\nGot "+pluralString(tokenDelta,"Token");
			}*/

			if( disclaimer.Length > 0 ) {
				prevMsgReset = StartCoroutine( resetMessage() );
			} else {
				disclaimer = "";//"\nSpacebar to Use";
			}
		}

		/* Debug.Log("Games Tried: " + ticketNum + "\nBills: $" + bills +
                  "\nTokens: " + tokens + disclaimer);*/

		return hadEnoughTokens;
	}

	void hideMouse() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void showMouse() {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	// Use this for initialization
	void Start() {
		camTilt = 342.5f;
		ticketNum = 0; // forget how many games tried
		baseFOV = Camera.main.fieldOfView;
		hideMouse();
		rb = GetComponent<Rigidbody>();
		tokenBillsChange(0,5);
	}

	void LookToward(Transform thatObject) {
		Vector3 rotDiff = thatObject.position-transform.position;
		rotDiff.y = 0.0f;
		transform.rotation = Quaternion.Slerp(transform.rotation,
		                                      Quaternion.LookRotation(rotDiff),
		                                      Time.deltaTime);
	}

	void TakePositionFor(PlayableGame whichGame) {
		Vector3 sameHeightFix = whichGame.standHere.position;
		sameHeightFix.y = transform.position.y;
		transform.position = Vector3.Slerp(transform.position,
		                                   sameHeightFix,
		                                   Time.deltaTime);
	}

	IEnumerator StartGameInAMoment(PlayableGame playScript) {
        Debug.Log("reached StartGameInAMoment");
		yield return new WaitForSeconds(0.05f); // keeps spacebar from counting as input on this game
		playScript.playerHere = gameObject;
		if(prevMsgReset != null) {
			StopCoroutine(prevMsgReset);
		}
		playingNow = playScript;
		playingNow.gameScreen.GameStart();
        Debug.Log("" + playingNow.gameName.Replace("\\n", "\n") + "\nSPACE TO QUIT\n" +
                  playingNow.gameInstructions.Replace("\\n", "\n"));
	}

    void FixedUpdate()
    {
        float cameraK = 0.7f;
        float targetTilt;
        float tiltK = 0.9f;
        if (playingNow != null)
        {
            LookToward(playingNow.transform);
            TakePositionFor(playingNow);
            Camera.main.fieldOfView *= cameraK;
            Camera.main.fieldOfView += zoomFOV * (1.0f - cameraK);

            /*targetTilt = 0.0f;
            camTilt = tiltK * camTilt + (1.0f - tiltK) * targetTilt;
            Camera.main.transform.localRotation = Quaternion.Euler(camTilt, 0.0f, 0.0f);*/
            Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.rotation,
                                                                      Quaternion.LookRotation(transform.forward),
                                                                      tiltK);
        }
        else
        {
            Camera.main.fieldOfView *= cameraK;
            Camera.main.fieldOfView += baseFOV * (1.0f - cameraK);
            targetTilt = 346.0f;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey( KeyCode.Escape )) {
			showMouse();
			Application.Quit();
		}

		if(playingNow != null) {
            if(Input.GetMouseButtonDown(0) || 
               Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftArrow)
               || Input.GetKeyDown(KeyCode.RightArrow) || playingNow.gameScreen.isPlaying == false) {
				// playingNow.gameScreen.isPlaying = false; // nah, leave it running but cease input!

				playingNow.playerHere = null; // freeing up player assignment AFTER the shuffle

				playingNow = null;
				tokenBillsChange(0,0);
			}
			return;
		}

		if(Input.GetKeyDown(KeyCode.Space)) {
			RaycastHit rhInfo;
			if( Physics.Raycast(transform.position, transform.forward, out rhInfo, 3.0f)) {
				int hadTokens = tokens;
                Debug.Log(rhInfo.collider.name);
				PlayableGame playScript = rhInfo.collider.GetComponent<PlayableGame>();

				TokenInteraction tiScript = rhInfo.collider.GetComponent<TokenInteraction>();
				if(tiScript == null) {
					return;
				}
				float distFromFront;

				if(playScript) {
					if(playScript.playerHere != null) {
						distFromFront = 500.0f;
					} else {
						distFromFront = Vector3.Distance(transform.position,
							playScript.gameScreen.myCab.standHere.position);
					}
				} else {
					distFromFront = 0.0f;
				}

                Debug.Log("ArcadePlayer ray hit: " + rhInfo.collider.name+
                          " pS:" +(playScript!=null)+
                          " tS:" + (tiScript!= null) +
                          " distFromFront:" + distFromFront);

				if(distFromFront < 1.5f) {
					bool hadTheMoney = false;
					if(playScript && playScript.gameScreen.isPlaying) {
						hadTheMoney = true;
						Debug.Log("no tokens needed, already in play");
					} else if(tiScript) {
						hadTheMoney = tiScript.tokenExchange(this);
					} else {
						Debug.Log("Touched: " + rhInfo.collider.name);
					}

					if(playScript && hadTheMoney) {
						StartCoroutine(StartGameInAMoment(playScript));
					}
				}
			} else {
				Debug.Log ("Not close enough");
			}
		}

		/*float mouseOrHorizKeys =
			Input.GetAxis("Horizontal") + Input.GetAxis("Mouse X");
		mouseOrHorizKeys = Mathf.Clamp(mouseOrHorizKeys, -1.0f, 1.0f);

		transform.Rotate(Vector3.up, mouseOrHorizKeys * Time.deltaTime * 85.0f);*/
	}
}
