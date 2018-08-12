using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public bool isPlaying;
	public float playTime = 4.0f;
	protected float endOfPlayTime;
	protected int timerLeft = 0;
	public PlayableGame myCab;
	protected bool flashing;

	public bool triedThisYet = false;

	public static int animFrameStep;
	
	protected int score = 0;
	protected int highScore = 0;

	public int getHighScore() {
		return highScore;
	}

	public void wipeHighScore() {
		highScore = 0;
		InstantLoseFromTimeDrain();
	}

	protected void clearScore() {
		score = 0;
	}

	public void InstantLoseFromTimeDrain() {
		endOfPlayTime = Time.time;
	}

	IEnumerator stepAllAnims() {
		while(true) {
			animFrameStep++;
			yield return new WaitForSeconds(0.2f);
		}
	}
	
	protected void addToScore(int scoreDelta) {
		score += scoreDelta;
		if(score > highScore) {
			highScore = score;
		}
	}

	public void SetCab(PlayableGame cabinet) {
		myCab = cabinet;
	}

	void Awake () {		
		isPlaying = false;
		StartCoroutine( stepAllAnims() );
	}

	public void Update() {
		if(isPlaying && myCab.playerHere != null) {
			if(ArcadePlayer.playingNow && ArcadePlayer.playingNow.gameScreen == this) {
				PerGameInput();
			} else {
				PerGameFakeAIInput();
			}
		}
	}

	public void GameStart() {
		if(ArcadePlayer.playingNow && ArcadePlayer.playingNow.gameScreen == this) {
			if(triedThisYet == false) {
				ArcadePlayer.ticketNum++;
				triedThisYet = true;
			}
		}

		if(isPlaying == false) {
			//Debug.Log("starting play for: " + myCab.gameName);
			endOfPlayTime = Time.time + playTime;
			clearScore();
			PerGameStart();
			isPlaying = true;
		} else {
			//Debug.Log("Resuming Play for: " + myCab.gameName);
		}
	}

	public void GameLogic() {
		flashing = ((int)(Time.time * 2.0f)%2==1);
		if(isPlaying) {
			timerLeft = (int)(endOfPlayTime-Time.time);
			if(timerLeft<=0) {
				timerLeft = 0; // guarding against some rounding error going negative
				PerGameExit();

				isPlaying = false; // will return to PerGameDemoMode

				if(myCab.playerHere && myCab.playerHere != PlayerDistrib.instance.player) {
					GameStart(); // reset!
				}
			} else {
				PerGameLogic();
				PerGameTimerDisplay();
			}
		} else {
			PerGameDemoMode();
			PerGameDemoModeCoinRequestDisplay();
		}
	}

	public virtual void PerGameStart() {
		Debug.Log (myCab.gameName +
		           ": Each game should override PerGameStart");
	}

	public virtual void PerGameExit() {
		Debug.Log (myCab.gameName +
		           ": Each game should override PerGameExit");
	}

	public virtual void PerGameLogic() {
		Debug.Log (myCab.gameName +
		           ": Each game should override PerGameLogic");
	}
	
	public virtual void PerGameTimerDisplay() {
		Debug.Log (myCab.gameName +
		           ": Each game should override PerGameLogic");
	}

	public virtual void PerGameDemoMode() {
		Debug.Log (myCab.gameName +
		           ": Each game should override PerGameDemoMode");
	}

	public virtual void PerGameDemoModeCoinRequestDisplay() {
		Debug.Log (myCab.gameName +
		           ": Each game should override PerGameDemoModeCoinRequestDisplay");
	}

	public virtual void PerGameInput() {
		Debug.Log (myCab.gameName +
		           ": Each game should override PerGameInput");
	}

	public virtual void PerGameFakeAIInput() {
		/*Debug.Log (myCab.gameName +
			": Each game should override PerGameFakeAIInput"); */
	}

}