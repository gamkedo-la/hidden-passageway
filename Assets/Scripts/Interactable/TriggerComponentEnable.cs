using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MouseTipOnLook))]
public class TriggerComponentEnable : MonoBehaviour {

    [Header("Trigger On Click")]
    public AbstractActivateable toEnable;

    // optional triggers fired at the SAME TIME
    // for when you need to move two things at once
    // for sequences, use callNext field instead
    [Header("Optional Simultaneous Triggers")]
    public AbstractActivateable toEnable2;
    public AbstractActivateable toEnable3;

    [Header("Trigger Settings")]
    public bool canBeReversed = false;

    public void triggerAction()
    {
        if (!enabled) {
            return;
        }

        if(toEnable.isDone == false)
        {
            toEnable.SendMessage("Activate");
            if (toEnable2) toEnable2.SendMessage("Activate");
            if (toEnable3) toEnable3.SendMessage("Activate");

        }
        else if(canBeReversed) {

            AbstractActivateable endOfChain = toEnable;
            while(endOfChain.callNext != null) {
                endOfChain = endOfChain.callNext;
            }
            endOfChain.SendMessage("Reverse");

            if (toEnable2)
            {
                endOfChain = toEnable2;
                while (endOfChain.callNext != null)
                {
                    endOfChain = endOfChain.callNext;
                }
                endOfChain.SendMessage("Reverse");
            }

            if (toEnable3)
            {
                endOfChain = toEnable3;
                while (endOfChain.callNext != null)
                {
                    endOfChain = endOfChain.callNext;
                }
                endOfChain.SendMessage("Reverse");
            }

        }
    }

    public bool canBeUsed() {
        return enabled && ((toEnable.isDone == false) || canBeReversed);
    }
}
