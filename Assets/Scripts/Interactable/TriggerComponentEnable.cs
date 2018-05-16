using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MouseTipOnLook))]
public class TriggerComponentEnable : MonoBehaviour {
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
