using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AetherDebugController : MonoBehaviour {


    [SerializeField] private bool devModeOn;
    [SerializeField] private bool fullBright;
    [SerializeField] private bool flight;
    [SerializeField] private bool noClip;
    [SerializeField] public GameObject player;
    private Light mainLight;
    public WalkControl walkControl;
    private Collider col;
    public LightShadows sTypeNone = 0;
    public LightShadows sTypeSoft;
    public float flightSpeed = 8f;
    private Quaternion nilRot;
    private Rigidbody playerRB;
    private float before;
    private AetherGameManager agm;
	// Use this for initialization
	void Start ()
    {
        // player = GameObject.FindGameObjectWithTag(Tags.Player);
        walkControl = player.GetComponent<WalkControl>();
        GameObject lightObject = GameObject.Find("Directional Light");
        mainLight = lightObject.GetComponent<Light>();
        col = player.GetComponent<Collider>();
        sTypeSoft = mainLight.shadows;
        nilRot = player.transform.rotation;
        playerRB = player.GetComponent<Rigidbody>();
        GameObject AGMGO = GameObject.Find("GameManager");
        agm = AGMGO.GetComponent<AetherGameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (devModeOn)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                flyMode();
                noClipMode();
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                TimeShift();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                fullBrightMode();
            }
            if (flight)
            {

                player.transform.position += player.transform.forward * Time.deltaTime * flightSpeed *
                Input.GetAxisRaw("Vertical");
                player.transform.position += player.transform.right * Time.deltaTime * flightSpeed *
                Input.GetAxisRaw("Horizontal");
                player.transform.Rotate(Vector3.up, Time.deltaTime * 65.0f * Input.GetAxis("Mouse X"));
                if (Input.GetKey(KeyCode.Q))
                {
                    player.transform.position += player.transform.up * Time.deltaTime * flightSpeed;
                }
                if (Input.GetKey(KeyCode.E))
                {
                    player.transform.position += player.transform.up * Time.deltaTime * flightSpeed *-2;
                }
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    flightSpeed = 28f;
                }
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    flightSpeed = 12f;
                }
                /*if (Input.GetKeyUp(KeyCode.Tab))
                {
                    if (Cursor.lockState == CursorLockMode.Locked)
                    {
                        Cursor.lockState = CursorLockMode.None;
                    }
                    else
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                    }
                }*/

            }



        }
    }

    void flyMode()
    {
        flight = !flight;
        if(flight == true)
        {
            walkControl.enabled = false;
            playerRB.useGravity = false;
            playerRB.velocity = new Vector3(0, 0, 0);
        }
        if (flight == false)
        {
            walkControl.enabled = true;
            playerRB.useGravity = true;
            playerRB.velocity = new Vector3(0, 0, 0);

        }
    }

    void noClipMode()
    {
        noClip = !noClip;
        if (noClip == true)
        {
            col.enabled = false;
        }
        if (noClip == false)
        {
            col.enabled = true;
        }
    }

    void fullBrightMode()
    {
        fullBright = !fullBright;
        if (fullBright == true) 
        {
            before = mainLight.intensity;
            mainLight.shadows = sTypeNone;
            mainLight.intensity = 1f;
        }
        if (fullBright == false)
        {

            mainLight.shadows = sTypeSoft;
            mainLight.intensity = before;
        }
    }

    void TimeShift()
    {
        agm.SendMessage("ChangeTime");
        Debug.Log("DEBUG - Time changed");
    }
}
