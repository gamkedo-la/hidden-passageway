using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetDoorBoolsForAnims : StateMachineBehaviour {
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetBool("openDoor"))
        {
            animator.SetBool("doorOpened", true);
            animator.SetBool("openDoor", false);
        }
        else if(animator.GetBool("closeDoor"))
        {
            animator.SetBool("doorOpened", false);
            animator.SetBool("closeDoor", false);
        }
    }

}
