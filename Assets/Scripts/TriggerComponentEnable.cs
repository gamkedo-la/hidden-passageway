using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerComponentEnable : MonoBehaviour {
	public string displayText = "Touch Book";
	public GameObject toEnable;
    public bool wasUsed = false;

    public void triggerAction()
    {
        if(wasUsed == false)
        {
            wasUsed = true;
            toEnable.SendMessage("Activate");
        }
    }
}
