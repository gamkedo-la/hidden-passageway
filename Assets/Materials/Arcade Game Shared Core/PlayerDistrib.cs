using UnityEngine;
using System.Collections;

public class PlayerDistrib : MonoBehaviour {
	public GameObject[] playersList;
	public PlayableGame[] cabinetsList;
	public static PlayerDistrib instance;

	public GameObject player; // to never get reassigned by code

	public void ForgetIfTried() {
		for(int i = 0; i < cabinetsList.Length; i++) {
			cabinetsList[i].gameScreen.triedThisYet = false;
		}
	}

	public string ScoresReport() {
		string toRet = "";
		for(int i = 0; i < cabinetsList.Length; i++) {
			toRet += cabinetsList[i].gameName + ": " +
				cabinetsList[i].gameScreen.getHighScore() + "\n";
		}
		return toRet;
	}

	public void WipeHighScores() {
		for(int i = 0; i < cabinetsList.Length; i++) {
			cabinetsList[i].gameScreen.wipeHighScore();
		}
	}

	public void Shuffle() {

		for(int i = 0; i < cabinetsList.Length; i++) {
			if(cabinetsList[i].playerHere && cabinetsList[i].playerHere != player) {
				cabinetsList[i].playerHere = null;
			}
		}

		for(int i = 0; i < playersList.Length; i++) {

			/*if(Random.Range(0, 40) < 5) {
				transform.position += Vector3.right * 1000.0f; // way outside of playfield
				continue;
			}*/

			int assignmentCab;
			do {
				assignmentCab = Random.Range(0,cabinetsList.Length);
			} while(cabinetsList[assignmentCab].playerHere != null);

			cabinetsList[assignmentCab].gameScreen.GameStart();

			cabinetsList[assignmentCab].playerHere = playersList[i];
			float wasY = playersList[i].transform.position.y;
			Vector3 newStand = cabinetsList[assignmentCab].standHere.position;
			newStand.y = wasY;
			playersList[i].transform.position = newStand +
				cabinetsList[assignmentCab].standHere.forward * 0.5f;
			Quaternion goalRot = cabinetsList[assignmentCab].standHere.rotation;
			goalRot *= Quaternion.AngleAxis(-90.0f, Vector3.right);
			playersList[i].transform.rotation = goalRot;
		}
	}

	// Use this for initialization
	void Start() {
		if(instance) {
			Destroy(instance);
		}
		instance = this;
		Shuffle();
	}
}
