﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MouseTipOnLook))]
public class TriggerComponentEnable : MonoBehaviour {
    public SlideToPos toEnable;

    public bool canBeReversed = false;

    public void triggerAction()
    {
        if(toEnable.isDone == false)
        {
            toEnable.SendMessage("Activate");
        } else if(canBeReversed) {
            toEnable.SendMessage("Reverse");
        }
    }

    public bool canBeUsed() {
        return (toEnable.isDone == false) || canBeReversed;
    }
}