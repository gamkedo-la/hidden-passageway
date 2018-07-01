using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AetherDebugController : MonoBehaviour {


    [SerializeField] private bool devModeOn;
    [SerializeField] private bool fullBright;
    [SerializeField] private bool flight;
    [SerializeField] private bool noClip;
    [SerializeField] private GameObject player;
    [SerializeField] private Light mainLight;
    [SerializeField] public WalkControl walkControl;
    [SerializeField] private Collider col;
    public LightShadows sTypeNone = 0;
    public LightShadows sTypeSoft;
    public float flightSpeed = 8f;
    private Quaternion nilRot;
    private Rigidbody playerRB;
    private float before;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
        walkControl = player.GetComponent<WalkControl>();
        GameObject lightObject = GameObject.Find("Directional Light");
        mainLight = lightObject.GetComponent<Light>();
        col = player.GetComponent<Collider>();
        sTypeSoft = mainLight.shadows;
        nilRot = player.transform.rotation;
        playerRB = player.GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update()
    {
        if (devModeOn)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                flyMode();
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                noClipMode();
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
                    flightSpeed = 16f;
                }
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    flightSpeed = 8f;
                }
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
        }
        if (flight == false)
        {
            walkControl.enabled = true;
            playerRB.useGravity = true;

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
}
