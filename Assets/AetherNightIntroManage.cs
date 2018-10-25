using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AetherNightIntroManage : MonoBehaviour {
    public AetherGameManager AGM;
    private EstView EV;
	
    // Use this for initialization
	void Start () {
        EV = GetComponent<EstView>();	
	}

    public void ReturnToDay()
    {
        if (AGM.Night)
        {
            AGM.MakeDay();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (EV.wpNow == 4)
        {
            if (AGM.Night == false)
            {
                AGM.MakeNight();
            }
        } else
        {
            if (AGM.Night)
            {
                AGM.MakeDay();
            }
        }
	}
}
