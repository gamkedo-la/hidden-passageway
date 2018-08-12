using UnityEngine;
using System.Collections;

public class GamePlayPODemo : PixelScreenLib {
	float ballX = 25;
	float ballY = 20;
	float ballXV = 3.4f;
	float ballYV = 1.4f;

	public override void PerPixelGameBootup() {
	}

	public override void PerGameInput() {
		if(Input.GetKey(KeyCode.LeftArrow) && ballXV > 0.0f) {
			ballXV *= -1.0f;
		}
		if(Input.GetKey(KeyCode.RightArrow) && ballXV < 0.0f) {
			ballXV *= -1.0f;
		}
		
		if(Input.GetKey(KeyCode.UpArrow) && ballYV > 0.0f) {
			ballYV *= -1.0f;
		}
		if(Input.GetKey(KeyCode.DownArrow) && ballYV < 0.0f) {
			ballYV *= -1.0f;
		}
		if(Input.GetKeyDown(KeyCode.Space)) {
			ballX = Random.Range(0.0f, screenWidth);
			ballY = Random.Range(0.0f, screenHeight);
		}
	}

	private void ballBounceAndDraw() {
		ballX += ballXV;
		ballY += ballYV;
		
		if(ballX < 0 && ballXV < 0.0f) {
			ballXV *= -1.0f;
		}
		if(ballX > screenWidth && ballXV > 0.0f) {
			ballXV *= -1.0f;
		}
		if(ballY < 0 && ballYV < 0.0f) {
			ballYV *= -1.0f;
		}
		if(ballY > screenHeight && ballYV > 0.0f) {
			ballYV *= -1.0f;
		}

		drawStickManAt((int)ballX,(int)ballY);
	}

	void drawStickManAt(int atX, int atY) {
		// head... stick
		drawBoxAt(atX-1,atY-5,3,3,yellowCol);
		safeDot(atX,atY-2,yellowCol);
		// arms
		safeDot(atX-2,atY,greenCol);
		safeDot(atX-2,atY+1,greenCol);
		safeDot(atX+2,atY,greenCol);
		safeDot(atX+2,atY+1,greenCol);
		// shoulders
		safeDot(atX-1,atY-1,greenCol);
		safeDot(atX+1,atY-1,greenCol);
		// torso
		safeDot(atX,atY-1,greenCol);
		safeDot(atX,atY,greenCol);
		safeDot(atX,atY+1,greenCol);
		// legs
		safeDot(atX-1,atY+2,greenCol);
		safeDot(atX-1,atY+3,greenCol);
		safeDot(atX-1,atY+4,greenCol);
		safeDot(atX+1,atY+2,greenCol);
		safeDot(atX+1,atY+3,greenCol);
		safeDot(atX+1,atY+4,greenCol);
	}

	void CenterBall() {
		ballX = screenWidth/2;
		ballY = screenHeight/2;
	}

	public override void PerGameStart() {
		CenterBall();
	}

	public override void PerGameExit() {
		CenterBall();
	}

	public override void PerGameDemoMode() {
		ballBounceAndDraw(); // for this game just let the ball bounce

		drawStringCentered(screenWidth/2,screenHeight/8,whiteCol,"PRARIE OVERKILL");
		drawStringCentered(screenWidth/2,screenHeight/8+8,redCol,"9000 LBS OF BUFFALO");
	}

	public override void PerGameDemoModeCoinRequestDisplay() {
		if( flashing ) {
			drawStringCentered(screenWidth/2,screenHeight/2,greenCol,"INSERT TOKEN");
		}
	}

	public override void PerGameTimerDisplay() {
		drawStringCentered(screenWidth/2,screenHeight/2+10,yellowCol,
		                   ""+ timerLeft);
	}

	public override void PerGameLogic() {
		ballBounceAndDraw();
	}
	
}