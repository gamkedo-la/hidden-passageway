using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractActivateable : MonoBehaviour {

    public bool isDone = false;

	public AbstractActivateable callNext;
    [HideInInspector] public AbstractActivateable callPrev;

	public abstract void Activate();
	public abstract void Reverse();

    private void Awake()
	{
        if (callNext != null) {
            callNext.callPrev = this;
        }
	}

}
