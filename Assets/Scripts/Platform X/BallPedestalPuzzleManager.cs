using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPedestalPuzzleManager : MonoBehaviour {

	[SerializeField] AbstractActivateable toEnable;
	[SerializeField] AbstractActivateable toEnable2;

	[SerializeField] Pedestal[] pedestals;
	[SerializeField] PuzzleBall[] balls;

	bool hasWon = false;

	public void CheckWinState()
	{
		if (hasWon) {
			return;
		}

		hasWon = true;
		foreach (Pedestal pedestal in pedestals) {
			hasWon = hasWon && pedestal.HasWon();
		}

		// Debug.Log("Win state:" + (hasWon ? "=1" : "=0"));

		if (hasWon) {
			toEnable.SendMessage("Activate");
			if (toEnable2 != null) {
				toEnable2.SendMessage("Activate");
			}

			DisablePuzzle();
		}
	}

	void DisablePuzzle()
	{
		foreach (Pedestal pedestal in pedestals) {
			pedestal.SendMessage("Disable");
		}
		foreach (PuzzleBall ball in balls) {
			ball.SendMessage("Disable");
		}
	}
}
