using UnityEngine;
using System.Collections;

public class GamePlayPO : PixelScreenLib {

	enum direction {W, NW, N, NE, E, SE, S, SW, COUNT};

	public Texture2D playerImg;
		private PixelSprite playerPOSprite;
	public Texture2D playerImg2;
	private int stepCounter = 0;
	private PixelSprite playerPOSprite2;
		private float pX;
		private float pY;
		private direction playerFacing;
	
	public Texture2D buffaloImg;
	public Texture2D buffaloImg2;
		private PixelSprite buffaloSprite;
	private PixelSprite buffaloSprite2;
		private float bX;
		private float bY;
		private direction buffaloFacing;
	private int bDeadCount;
	private bool buffaloLookingLeft = false;

		private float l1X;
		private float l1Y;
		private direction laser1Facing; 
		private bool laser1Ready;

	public Texture2D treeImg;
		private PixelSprite treeSprite;
		private float tX;
		private float tY;

	bool isWalking = false;

	float ballX = 25;
	float ballY = 20;
	float ballXV = 3.4f;
	float ballYV = 1.4f;

	public override void PerPixelGameBootup() { // happens once per game universe
		playerPOSprite = new PixelSprite(playerImg, 16);
			playerPOSprite.isAnimating = false;
		playerPOSprite2 = new PixelSprite(playerImg2, 16);
		playerPOSprite2.isAnimating = false;

		buffaloSprite = new PixelSprite(buffaloImg, 32);
			buffaloSprite.isAnimating = false;
		buffaloSprite2 = new PixelSprite(buffaloImg2, 32);
		buffaloSprite2.isAnimating = false;

		treeSprite = new PixelSprite(treeImg);
			treeSprite.isAnimating = false;
	}

	private direction turnLeft(direction fromDir) {
		int asInt = (int)fromDir;
		asInt--;
		if(asInt < 0) {
			asInt += (int)direction.COUNT;
		}
		return (direction)asInt;
	}

	private direction turnRight(direction fromDir) {
		int asInt = (int)fromDir;
		asInt++;
		if(asInt >= (int)direction.COUNT) {
			asInt -= (int)direction.COUNT;
		}
		return (direction)asInt;
	}

	private int dirToXO(direction inDir) {
		switch (inDir){
		case direction.NW:
		case direction.W:
		case direction.SW:
			return -1;
		case direction.N:
		case direction.S:
			return 0;
		case direction.NE:
		case direction.E:
		case direction.SE:
			return 1;
		}
		return 0;
	}
	private int dirToYO(direction inDir) {
		switch (inDir){
		case direction.NW:
		case direction.N:
		case direction.NE:
			return -1;
		case direction.W:
		case direction.E:
			return 0;
		case direction.SE:
		case direction.S:
		case direction.SW:
			return 1;
		}
		return 0;
	}
	private float wrapX(float forX) {
		if(forX < 0) {
			return forX + screenWidth;
		}
		if(forX > screenWidth+1) {
			return forX - screenWidth;
		}
		return forX;
	}
	private float wrapY(float forY) {
		if(forY < 0) {
			return forY + screenHeight;
		}
		if(forY > screenWidth+1) {
			return forY - screenHeight;
		}
		return forY;
	}

	private direction dirFlip(direction fromDir) {
		int dirInt = (int)fromDir;
		dirInt += 4;
		if(dirInt >= (int)direction.COUNT) {
			dirInt -= (int)direction.COUNT;
		}
		return (direction)dirInt;
	}

	private direction dirAway(float sx,float sy, float tx, float ty) {
		float dx = sx - tx;
		float dy = sy - ty;
		float dist = Mathf.Sqrt(dx * dx + dy * dy);
		float nx = dx / dist;
		float ny = dy / dist;
		if(nx < -0.8f) {
			return direction.W;
		}
		if(nx > 0.8f) {
			return direction.E;
		}
		if(ny < -0.8f) {
			return direction.N;
		}
		if(ny > 0.8f) {
			return direction.S;
		}
		if(nx < 0.0f && ny < 0.0f) {
			return direction.NW;
		}
		if(nx > 0.0f && ny < 0.0f) {
			return direction.NE;
		}
		if(nx < 0.0f && ny > 0.0f) {
			return direction.SW;
		}
		if(nx > 0.0f && ny > 0.0f) {
			return direction.SE;
		}
		return direction.N;
	}

	public override void PerGameFakeAIInput() {
		if(Random.Range(0, 100) < 7) {
			if(buffaloSprite.drawFrame != 2) {
				playerFacing = dirFlip(dirAway(pX, pY, bX, bY));
				isWalking = true;
			} else {
				isWalking = false;
			}
		}

		if(Random.Range(0, 100) < 3) {
			playerFacing = turnLeft(playerFacing);
		}
		if(Random.Range(0, 100) < 3) {
			playerFacing = turnRight(playerFacing);
		}

		if(Random.Range(0, 100) < 10) {
			FireLaser();
		}
	}

	public override void PerGameInput() {

		// arrow keys set position frame 1 and then move after
	
		if(Input.GetKeyDown(KeyCode.LeftArrow)) { 
			playerFacing = turnLeft(playerFacing);
		}
		if(Input.GetKeyDown(KeyCode.UpArrow)) { 
			isWalking = true;
		}
		if(Input.GetKeyDown(KeyCode.RightArrow)) { 
			playerFacing = turnRight(playerFacing);
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)) { 
			isWalking = false;
		}

		if(Input.GetKeyDown(KeyCode.Space)) {
			FireLaser();
		}
	}

	private void moveBuffalo() {
		float buffaloSpeed = 3.0f;
		if(buffaloSprite.drawFrame != 2) {
			float buffOffX = dirToXO(buffaloFacing) * buffaloSpeed;
			if(buffOffX < 0.0f) {
				buffaloLookingLeft = true;
			}
			if(buffOffX > 0.0f) {
				buffaloLookingLeft = false;
			}
			bX += buffOffX;
			bY += dirToYO(buffaloFacing) * buffaloSpeed;
			if(Random.Range(0, 100) < 4) {
				buffaloFacing = (direction)Random.Range(0, (int)direction.COUNT);
			}
		}

		bX = wrapX(bX);
		bY = wrapY(bY);

		if(buffaloSprite.drawFrame == 1) {
			buffaloSprite.drawFrame = 0;
		} else if(buffaloSprite.drawFrame == 0) {
			buffaloSprite.drawFrame = 1;
		}
		buffaloSprite2.drawFrame = buffaloSprite.drawFrame;
	}

	private void Draw() {
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

		int bdx = (int)Mathf.Abs(bX - l1X);
		int bdy = (int)Mathf.Abs(bY - l1Y);
		if(bdx < 16 && bdy < 16 && buffaloSprite.drawFrame != 2 && !laser1Ready && isPlaying) {
			buffaloSprite.drawFrame = 2;
			addToScore( (int)Random.Range(280,320) );
			laser1Ready = true;
			bDeadCount = 20;
		}
		bdx = (int)Mathf.Abs(bX - pX);
		bdy = (int)Mathf.Abs(bY - pY);
		if(bdx < 25 && bdy < 25 && buffaloSprite.drawFrame != 2) {
			buffaloFacing = dirAway(bX,bY, pX,pY);
		}

		if(bDeadCount > 0) {
			bDeadCount--;
			if(bDeadCount == 0) {
				buffaloSprite.drawFrame = 0;
				bX = Random.Range(0, screenWidth);
				bY = Random.Range(0, screenHeight);
			}
		}

		int offsetX = -8;
		int offsetY = -16;

		if(isPlaying) {
			if(isWalking == false || stepCounter-- > 0) {
				playerPOSprite.drawImage(this, (int)pX + offsetX, (int)pY + offsetY);
			} else {
				if(stepCounter < -2) {
					stepCounter = 2;
				}
				playerPOSprite2.drawImage(this, (int)pX + offsetX, (int)pY + offsetY);
			}
		}

		if(buffaloLookingLeft) {
			buffaloSprite2.drawImage(this, (int)bX - 16, (int)bY - 16);
		} else {
			buffaloSprite.drawImage(this, (int)bX - 16, (int)bY - 16);
		}

		treeSprite.drawImage(this, (int)11, (int)15);
		treeSprite.drawImage(this, (int)21, (int)22);
		treeSprite.drawImage(this, (int)30, (int)8);

		treeSprite.drawImage(this, (int)75, (int)89);
		treeSprite.drawImage(this, (int)86, (int)78);

		treeSprite.drawImage(this, (int)4, (int)63);

		treeSprite.drawImage(this, (int)99, (int)10);

		if(!laser1Ready && isPlaying) {
			drawBoxAt((int)l1X - 1, (int)l1Y - 1, 3, 3, whiteCol);
		}

		drawStringCentered(screenWidth-15,screenHeight-17,yellowCol, "best");
		drawStringCentered(screenWidth-15,screenHeight-10,yellowCol,
			""+ highScore);
		drawStringCentered(15,screenHeight-17,yellowCol, "meat");
		drawStringCentered(15, screenHeight - 10, yellowCol,
			"" + score);
	}

	private void FireLaser(){
		if (laser1Ready){
			l1X = pX;
			l1Y = pY;
			laser1Facing = playerFacing;
			laser1Ready = false;
		}
	}

	void CenterPlayer() {
		pX = screenWidth/2;
		pY = screenHeight/2;
	}

	public override void PerGameStart() { // happens every time cabinet is started
		CenterPlayer();
		bDeadCount = 0;
		bX = Random.Range(0, screenWidth);
		bY = Random.Range(0, screenHeight);
		laser1Ready = true;
		buffaloSprite.drawFrame = 0;
	}

	public override void PerGameExit() { // ever time game over
		CenterPlayer();
	}

	public override void PerGameDemoMode() {
		moveBuffalo();
		Draw();

		drawStringCentered(screenWidth/2,screenHeight/8+40,whiteCol,"PRAIRIE OVERKILL");
		drawStringCentered(screenWidth/2,screenHeight/8+48,greenCol,"9000 LBS OF BUFFALO");
	}

	public override void PerGameDemoModeCoinRequestDisplay() {
		if( flashing ) {
			drawStringCentered(screenWidth/2,screenHeight/2+15,redCol,"INSERT TOKEN");
		}
	}

	public override void PerGameTimerDisplay() {
		drawStringCentered(screenWidth/2,screenHeight-17,yellowCol, "time");
		drawStringCentered(screenWidth/2,screenHeight-10,yellowCol,
			""+ timerLeft);
	}

	public override void PerGameLogic() {
		moveBuffalo();

		if (isWalking){ // Player Movement
			pX += dirToXO(playerFacing);
			pY += dirToYO(playerFacing);
		}
		float shotSpeed = 8.0f;
		if(!laser1Ready){
			l1X += dirToXO(laser1Facing)*shotSpeed;
			l1Y += dirToYO(laser1Facing)*shotSpeed;
		}
		if(l1X < 0 || l1X > screenWidth || l1Y < 0 || l1Y > screenHeight)
		{
			laser1Ready = true;
		}

		pX = wrapX(pX);
		pY = wrapY(pY);

		playerPOSprite.drawFrame = (int)playerFacing;
		playerPOSprite2.drawFrame = (int)playerFacing;

		Draw();
	}
	
}