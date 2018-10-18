using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal : MonoBehaviour {

	[SerializeField] BallPedestalPuzzleManager ballPedestalPuzzleManager;
	[SerializeField] bool winState;
	private bool currentState = false;

	private bool checkState = false;

	void OnTriggerEnter(Collider other)
	{
		if (!enabled) {
			return;
		}

		PuzzleBall pb = other.GetComponent<PuzzleBall>();
		if (pb != null) {
			checkState = currentState = true;
			ballPedestalPuzzleManager.CheckWinState();
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (!enabled) {
			return;
		}

		PuzzleBall pb = other.GetComponent<PuzzleBall>();
		if (pb != null) {
			checkState = true;
		}
	}

    void OnTriggerExit(Collider other)
    {
        checkState = false;
    }

	void Update ()
	{
		if (currentState) {
			transform.Rotate(Vector3.up, 10 * Time.deltaTime);
			if (!checkState) {
				currentState = false;
			}
		}
	}

	public bool HasWon ()
	{
		// Debug.Log("has won?" + PlayerPrefsHelper.GetPrefsName(gameObject) + " = " + (currentState?"1":"0") + "=="+(winState?"1":"0"));
		return currentState == winState;
	}

	public void Disable ()
	{
		enabled = false;
	}
}
