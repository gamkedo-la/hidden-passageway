using UnityEngine;
using System.Collections;

public class PixelSprite {
	public bool isAnimating = true; 
	public bool isFacingLeft = false;
	public int drawFrame = 0;

	private Texture2D originalTexture;
	private Color32[] pixelBuffer;

	private int sourceTopLeftX, sourceTopLeftY;
	private int eachWid, eachHei;
	private int animFrames;

	public PixelSprite(Texture2D useTexture,
	                   int perPicWid=-1) {
		originalTexture = useTexture;
		pixelBuffer = originalTexture.GetPixels32();

		// (used to allow these to be passed in and optionally overriden, now we just assume 0,0 top)
		sourceTopLeftX = 0;
		sourceTopLeftY = 0;

		if(perPicWid < 0) {
			eachWid = originalTexture.width;
			isAnimating = false; // single frame, no need to animate it
		} else {
			eachWid = perPicWid;
		}
		eachHei = originalTexture.height;
		animFrames = originalTexture.width / eachWid;
	}

	public void drawImage(PixelScreenLib toSurface,
	                      int xDest, int yDest) {
		if(isAnimating) {
			drawFrame = GameManager.animFrameStep % animFrames;
		}
		toSurface.copyBitmapFromToColorArray(sourceTopLeftX, sourceTopLeftY,
		                                     eachWid, eachHei,
		                                     xDest,yDest,
		                                     pixelBuffer,originalTexture.width,
		                                     drawFrame,
		                                     isFacingLeft);
	}

}