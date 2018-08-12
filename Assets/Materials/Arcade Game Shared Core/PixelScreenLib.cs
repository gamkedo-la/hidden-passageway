using UnityEngine;
using System.Collections;

public class PixelScreenLib : GameManager {
	public Texture2D paintThis;
	protected Color32[] screenBuffer;

	public Color bgCol = new Color(0.3f, 0.3f, 0.3f);
	protected bool disableAutoScreenClear = false;
	protected Color whiteCol = new Color(1.0f, 1.0f, 1.0f);
	protected Color blackCol = new Color(0.0f, 0.0f, 0.0f);
	protected Color redCol = new Color(1.0f, 0.0f, 0.0f);
	protected Color yellowCol = new Color(1.0f, 1.0f, 0.0f);
	protected Color greenCol = new Color(0.0f, 1.0f, 0.0f);
	protected Color cyanCol = new Color(0.0f, 1.0f, 1.0f);

	protected int screenWidth; // cache
	protected int screenHeight; // cache

	protected Color sharedColor;

	// Use this for initialization
	void Awake () {
		// paintThis = Instantiate(GetComponent<Renderer>().material.mainTexture) as Texture2D;
    	// GetComponent<Renderer>().material.mainTexture = paintThis;
		
		screenWidth = paintThis.width;
		screenHeight = paintThis.width;

		// leaving this between frames, only getting it here at start
		screenBuffer = paintThis.GetPixels32();

		sharedColor = Color.white;
		eraseTexture();
		PerPixelGameBootup();
		StartCoroutine( LogicUpdate() );
	}

	public virtual void PerPixelGameBootup() {
		Debug.Log (myCab.gameName +
		           ": Each pixel game should override PerPixelGameBootup");
	}

	private void showPixelBuffer() {
		paintThis.SetPixels32(screenBuffer);
		paintThis.Apply();
	}
	
	private void eraseTexture() {
		sharedColor = bgCol;
		for(int x=0;x<screenWidth;x++) {
			for(int y=0;y<screenHeight;y++) {
				dot(x, y);
			}
		}
		showPixelBuffer();
	}

	protected Color32 getBitmapColor(int atX, int atY, Color32[] src, int srcWid, int srcHei) {
		int srcPixelIndex = atX + (srcHei-1-atY)*srcWid;
		if(srcPixelIndex >= 0 && srcPixelIndex < src.Length) {
			return src[srcPixelIndex];
		} else {
			return blackCol;
		}
	}

	public void copyBitmapFromToColorArray(int sX, int sY, // top left xy of corner in source
	                                          int sW, int sH, // width and height from source
	                                          int dX, int dY, // destination x and y
	                                          Color32[] src, // bitmap data
	                                          int srcWid, // how many pixels wide the image is
	                                          int animMult = 0, // which anim (horizontal strip) to use
	                                          bool flipH = false) { // whether to mirror right-to-left
		int animSX = sX + sW*animMult;
		for(int x=animSX; x < animSX + sW; x++) {
			for(int y=sY; y < sY + sH; y++) {
				int srcPixelIndex = (flipH ? srcWid-1-x : x) + ((sH+sY-1)-y)*srcWid;
				if(src[srcPixelIndex].a > 32) { // skip invisible alpha
					int drawX = dX+(x-animSX);
					int drawY = dY+(y-sY);
					if((drawX < 0 || drawX>=screenWidth || drawY < 0 || drawY>=screenHeight) == false){
						screenBuffer[drawX + drawY*screenWidth] = src[srcPixelIndex];
					}
				}
			}
		}
	}

	protected void drawStringCentered(int fX, int fY, Color useCol, string thisText) {
		drawString(fX - (int)(thisText.Length * 2.15f), fY, useCol, thisText);
	}

	protected void drawString(int fX, int fY, Color useCol, string thisText) {
		sharedColor = useCol;
		string upperText = thisText.ToUpper();

		int skipBy;

		for(int i=0;i<thisText.Length;i++) {
			skipBy = 4; // default for 3 pixel wide text
			switch(upperText[i]) {
			case '?':dotGrid(fX,fY,
				                 1,1,1,
				                 0,0,1,
				                 0,1,1,
				                 0,0,0,
				                 0,1,0);
				break;
			case ':':dotGrid(fX,fY,
				                 0,0,0,
				                 0,1,0,
				                 0,0,0,
				                 0,1,0,
				                 0,0,0);
				break;
			case '\'':dotGrid(fX,fY,
				                 0,1,0,
				                 0,1,0,
				                 0,0,0,
				                 0,0,0,
				                 0,0,0);
				break;
			case '.':dotGrid(fX,fY,
				                  0,0,0,
				                  0,0,0,
				                  0,0,0,
				                  0,0,0,
				                  0,1,0);
				break;
			case ',':dotGrid(fX,fY,
				                 0,0,0,
				                 0,0,0,
				                 0,0,0,
				                 0,0,1,
				                 0,1,0);
				break;
			case '!':dotGrid(fX,fY,
				                 0,1,0,
				                 0,1,0,
				                 0,1,0,
				                 0,0,0,
				                 0,1,0);
				break;
			case '0':
			case 'O':dotGrid(fX,fY,
				        1,1,1,
				        1,0,1,
				        1,0,1,
				        1,0,1,
				        1,1,1);
				break;
			case '1':
			case 'I':dotGrid(fX,fY,
				        1,0,0,
				        1,0,0,
				        1,0,0,
				        1,0,0,
				        1,0,0);
				skipBy = 2; break;
			case '2': dotGrid(fX,fY,
				        1,1,1,
				        0,0,1,
				        1,1,1,
				        1,0,0,
				        1,1,1);
				break;
			case '3': dotGrid(fX,fY,
				        1,1,1,
				        0,0,1,
				        0,1,1,
				        0,0,1,
				        1,1,1);
				break;
			case '4': dotGrid(fX,fY,
				        1,0,1,
				        1,0,1,
				        1,1,1,
				        0,0,1,
				        0,0,1);
				break;
			case '5': dotGrid(fX,fY,
				        1,1,1,
				        1,0,0,
				        1,1,1,
				        0,0,1,
				        1,1,1);
				break;
			case '6': dotGrid(fX,fY,
				        1,1,1,
				        1,0,0,
				        1,1,1,
				        1,0,1,
				        1,1,1);
				break;
			case '7': dotGrid(fX,fY,
				        1,1,1,
				        0,0,1,
				        0,0,1,
				        0,0,1,
				        0,0,1);
				break;
			case '8': dotGrid(fX,fY,
				        1,1,1,
				        1,0,1,
				        1,1,1,
				        1,0,1,
				        1,1,1);
				break;
			case '9': dotGrid(fX,fY,
				        1,1,1,
				        1,0,1,
				        1,1,1,
				        0,0,1,
				        0,0,1);
				break;
			case 'A': dotGrid(fX,fY,
				                  1,1,1,
				                  1,0,1,
				                  1,1,1,
				                  1,0,1,
				                  1,0,1);
				break;
			case 'B': dotGrid(fX,fY,
				                  1,1,0,
				                  1,0,1,
				                  1,1,0,
				                  1,0,1,
				                  1,1,0);
				break;
			case 'C': dotGrid(fX,fY,
				                  1,1,1,
				                  1,0,0,
				                  1,0,0,
				                  1,0,0,
				                  1,1,1);
				break;
			case 'D': dotGrid(fX,fY,
				                  1,1,0,
				                  1,0,1,
				                  1,0,1,
				                  1,0,1,
				                  1,1,0);
				break;
			case 'E': dotGrid(fX,fY,
				                  1,1,1,
				                  1,0,0,
				                  1,1,0,
				                  1,0,0,
				                  1,1,1);
				break;
			case 'F': dotGrid(fX,fY,
				                  1,1,1,
				                  1,0,0,
				                  1,1,0,
				                  1,0,0,
				                  1,0,0);
				break;
			case 'G': dotGrid(fX,fY,
				                  1,1,1,
				                  1,0,0,
				                  1,0,0,
				                  1,0,1,
				                  1,1,1);
				break;
			case 'H': dotGrid(fX,fY,
				                  1,0,1,
				                  1,0,1,
				                  1,1,1,
				                  1,0,1,
				                  1,0,1);
				break;
			// skipping I, caught in same case as '1'
			case 'J': dotGrid(fX,fY,
				                  0,0,1,
				                  0,0,1,
				                  0,0,1,
				                  1,0,1,
				                  1,1,1);
				break;
			case 'K': dotGrid(fX,fY,
				                  1,0,1,
				                  1,0,1,
				                  1,1,0,
				                  1,0,1,
				                  1,0,1);
				break;
			case 'L': dotGrid(fX,fY,
				                  1,0,0,
				                  1,0,0,
				                  1,0,0,
				                  1,0,0,
				                  1,1,1);
				break;
			case 'M': dotGridWide(fX,fY,
				                  1,0,0,0,1,
				                  1,1,0,1,1,
				                  1,0,1,0,1,
				                  1,0,0,0,1,
				                  1,0,0,0,1);
				skipBy = 6;
				break;
			case 'N': dotGridWide(fX,fY,
				                      1,0,0,1,0,
				                      1,1,0,1,0,
				                      1,0,1,1,0,
				                      1,0,0,1,0,
				                      1,0,0,1,0);
				skipBy = 5;
				break;
				// no 'O' same case as '0'
			case 'P': dotGrid(fX,fY,
				                  1,1,0,
				                  1,0,1,
				                  1,1,0,
				                  1,0,0,
				                  1,0,0);
				break;
			case 'Q': dotGridWide(fX,fY,
				                      1,1,1,1,1,
				                      1,0,0,0,1,
				                      1,0,1,0,1,
				                      1,1,1,1,1,
				                      0,0,0,1,0);
				skipBy = 6;
				break;
			case 'R': dotGrid(fX,fY,
				                  1,1,0,
				                  1,0,1,
				                  1,1,0,
				                  1,0,1,
				                  1,0,1);
				break;
			case 'S': dotGrid(fX,fY,
				                  1,1,1,
				                  1,0,0,
				                  1,1,1,
				                  0,0,1,
				                  1,1,1);
				break;
			case 'T': dotGrid(fX,fY,
				                  1,1,1,
				                  0,1,0,
				                  0,1,0,
				                  0,1,0,
				                  0,1,0);
				break;
			case 'U': dotGrid(fX,fY,
				                  1,0,1,
				                  1,0,1,
				                  1,0,1,
				                  1,0,1,
				                  1,1,1);
				break;
			case 'V': dotGrid(fX,fY,
				                  1,0,1,
				                  1,0,1,
				                  1,0,1,
				                  1,0,1,
				                  0,1,0);
				break;
			case 'W': dotGridWide(fX,fY,
				                      1,0,0,0,1,
				                      1,0,0,0,1,
				                      1,0,1,0,1,
				                      1,1,0,1,1,
				                      1,0,0,0,1);
				skipBy = 6;
				break;
			case 'X': dotGrid(fX,fY,
				                  1,0,1,
				                  1,0,1,
				                  0,1,0,
				                  1,0,1,
				                  1,0,1);
				break;
			case 'Y': dotGrid(fX,fY,
				                  1,0,1,
				                  1,0,1,
				                  0,1,0,
				                  0,1,0,
				                  0,1,0);
				break;
			case 'Z': dotGrid(fX,fY,
				                  1,1,1,
				                  0,0,1,
				                  0,1,0,
				                  1,0,0,
				                  1,1,1);
				break;
			}
			fX += skipBy; 
		}
	}

	void dotGrid(int atX,int atY,
	             int at00, int at10, int at20,
	             int at01, int at11, int at21,
	             int at02, int at12, int at22,
	             int at03, int at13, int at23,
	             int at04, int at14, int at24) {
		if(atX >= screenWidth-3) { // avoid out of array accesses
			return;
		}
		if(at00==1) screenBuffer[ (atX) + ((atY) * screenWidth) ] = sharedColor;
		if(at10==1) screenBuffer[ (atX+1) + ((atY) * screenWidth) ] = sharedColor;
		if(at20==1) screenBuffer[ (atX+2) + ((atY) * screenWidth) ] = sharedColor;
		if(at01==1) screenBuffer[ (atX) + ((atY+1) * screenWidth) ] = sharedColor;
		if(at11==1) screenBuffer[ (atX+1) + ((atY+1) * screenWidth) ] = sharedColor;
		if(at21==1) screenBuffer[ (atX+2) + ((atY+1) * screenWidth) ] = sharedColor;
		if(at02==1) screenBuffer[ (atX) + ((atY+2) * screenWidth) ] = sharedColor;
		if(at12==1) screenBuffer[ (atX+1) + ((atY+2) * screenWidth) ] = sharedColor;
		if(at22==1) screenBuffer[ (atX+2) + ((atY+2) * screenWidth) ] = sharedColor;
		if(at03==1) screenBuffer[ (atX) + ((atY+3) * screenWidth) ] = sharedColor;
		if(at13==1) screenBuffer[ (atX+1) + ((atY+3) * screenWidth) ] = sharedColor;
		if(at23==1) screenBuffer[ (atX+2) + ((atY+3) * screenWidth) ] = sharedColor;
		if(at04==1) screenBuffer[ (atX) + ((atY+4) * screenWidth) ] = sharedColor;
		if(at14==1) screenBuffer[ (atX+1) + ((atY+4) * screenWidth) ] = sharedColor;
		if(at24==1) screenBuffer[ (atX+2) + ((atY+4) * screenWidth) ] = sharedColor;
	}

	void dotGridWide(int atX,int atY,
	                 int at00, int at10, int at20, int at30, int at40,
	                 int at01, int at11, int at21, int at31, int at41,
	                 int at02, int at12, int at22, int at32, int at42,
	                 int at03, int at13, int at23, int at33, int at43,
	                 int at04, int at14, int at24, int at34, int at44) {
		if(atX >= screenWidth-5) { // avoid out of array accesses
			return;
		}
		if(at00==1) screenBuffer[ (atX) + ((atY) * screenWidth) ] = sharedColor;
		if(at10==1) screenBuffer[ (atX+1) + ((atY) * screenWidth) ] = sharedColor;
		if(at20==1) screenBuffer[ (atX+2) + ((atY) * screenWidth) ] = sharedColor;
		if(at30==1) screenBuffer[ (atX+3) + ((atY) * screenWidth) ] = sharedColor;
		if(at40==1) screenBuffer[ (atX+4) + ((atY) * screenWidth) ] = sharedColor;
		if(at01==1) screenBuffer[ (atX) + ((atY+1) * screenWidth) ] = sharedColor;
		if(at11==1) screenBuffer[ (atX+1) + ((atY+1) * screenWidth) ] = sharedColor;
		if(at21==1) screenBuffer[ (atX+2) + ((atY+1) * screenWidth) ] = sharedColor;
		if(at31==1) screenBuffer[ (atX+3) + ((atY+1) * screenWidth) ] = sharedColor;
		if(at41==1) screenBuffer[ (atX+4) + ((atY+1) * screenWidth) ] = sharedColor;
		if(at02==1) screenBuffer[ (atX) + ((atY+2) * screenWidth) ] = sharedColor;
		if(at12==1) screenBuffer[ (atX+1) + ((atY+2) * screenWidth) ] = sharedColor;
		if(at22==1) screenBuffer[ (atX+2) + ((atY+2) * screenWidth) ] = sharedColor;
		if(at32==1) screenBuffer[ (atX+3) + ((atY+2) * screenWidth) ] = sharedColor;
		if(at42==1) screenBuffer[ (atX+4) + ((atY+2) * screenWidth) ] = sharedColor;
		if(at03==1) screenBuffer[ (atX) + ((atY+3) * screenWidth) ] = sharedColor;
		if(at13==1) screenBuffer[ (atX+1) + ((atY+3) * screenWidth) ] = sharedColor;
		if(at23==1) screenBuffer[ (atX+2) + ((atY+3) * screenWidth) ] = sharedColor;
		if(at33==1) screenBuffer[ (atX+3) + ((atY+3) * screenWidth) ] = sharedColor;
		if(at43==1) screenBuffer[ (atX+4) + ((atY+3) * screenWidth) ] = sharedColor;
		if(at04==1) screenBuffer[ (atX) + ((atY+4) * screenWidth) ] = sharedColor;
		if(at14==1) screenBuffer[ (atX+1) + ((atY+4) * screenWidth) ] = sharedColor;
		if(at24==1) screenBuffer[ (atX+2) + ((atY+4) * screenWidth) ] = sharedColor;
		if(at34==1) screenBuffer[ (atX+3) + ((atY+4) * screenWidth) ] = sharedColor;
		if(at44==1) screenBuffer[ (atX+4) + ((atY+4) * screenWidth) ] = sharedColor;
	}

	void dot(int atX,int atY) {
		screenBuffer[ atX + (atY * screenWidth) ] = sharedColor;
	}

	protected void setBufferPixel(int atX,int atY,Color withColor) {
		screenBuffer[ atX + (atY * screenWidth) ] = withColor;
	}

	public void safeDot(int atX,int atY,Color withColor) {
		if(atX < 0 || atY < 0 || atX >= screenWidth || atY >= screenHeight) {
			return;
		}
		screenBuffer[ atX + (atY * screenWidth) ] = withColor;
	}

	public void drawBoxAt(int leftSideX, int topSideY, int dimW, int dimH, Color32 useCol) {
		int left = leftSideX;
		int right = left+dimW;
		int top = topSideY;
		int bot = topSideY+dimH;
		if(left < 0) {
			left = 0;
		}
		if(top < 0) {
			top = 0;
		}
		if(right > screenWidth) {
			right = screenWidth;
		}
		if(bot > screenHeight) {
			bot = screenHeight;
		}
		for(int xp=left; xp <right; xp++) {
			for(int yp=top; yp <bot; yp++) {
				// paintThis.SetPixel(xp,yp,useCol);
				setBufferPixel(xp,yp,useCol);
			}
		}
	}
	
	IEnumerator LogicUpdate() {

		while(true) {
			if(disableAutoScreenClear==false) {
				eraseTexture();
			}

			GameLogic();

			showPixelBuffer();
			yield return new WaitForSeconds(0.06f);
		}
	}

	/*
	public void lightGun() { // reminder: needs mouse unlocked
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		if(Physics.Raycast(ray, out hit, 300.0f)) {
			Vector3 myPt = transform.InverseTransformPoint(hit.point);
			if(Mathf.Abs(myPt.z) <= 0.2f) {
				myPt += Vector3.one*0.5f;
				paintLayer((int)(myPt.x*screenWidth), (int)(myPt.y*screenHeight), 30, redCol);
			}
		}
	}
	 */

}