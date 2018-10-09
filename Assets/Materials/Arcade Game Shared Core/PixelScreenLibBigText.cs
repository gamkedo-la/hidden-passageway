using UnityEngine;
using System.Collections;

public class PixelScreenLibBigText : PixelScreenLib {
	protected int MP = 4;

	protected void MP_drawStringCentered(int fX, int fY, Color useCol, string thisText) {
		MP_drawString(fX - (int)(thisText.Length * 2.15f), fY, useCol, thisText);
	}

	protected void MP_drawString(int fX, int fY, Color useCol, string thisText) {
		sharedColor = useCol;
		string upperText = thisText.ToUpper();

		int skipBy;

		for(int i=0;i<thisText.Length;i++) {
			skipBy = 4; // default for 3 pixel wide text
			switch(upperText[i]) {
			case '?':dotGrid_MP(fX,fY,
				1,1,1,
				0,0,1,
				0,1,1,
				0,0,0,
				0,1,0);
				break;
			case ':':dotGrid_MP(fX,fY,
				0,0,0,
				0,1,0,
				0,0,0,
				0,1,0,
				0,0,0);
				break;
                case '-':
                    dotGrid_MP(fX, fY,
                   0, 0, 0,
                   0, 0, 0,
                   1, 1, 1,
                   0, 0, 0,
                   0, 0, 0);
                    break;
                case '(':
                    dotGrid_MP(fX, fY,
                   0, 0, 1,
                   0, 1, 0,
                   0, 1, 0,
                   0, 1, 0,
                   0, 0, 1);
                    break;
                case ')':
                    dotGrid_MP(fX, fY,
                   1, 0, 0,
                   0, 1, 0,
                   0, 1, 0,
                   0, 1, 0,
                   1, 0, 0);
                    break;
                case '\"':
                    dotGrid_MPWide(fX, fY,
                          0, 1, 0, 1, 0,
                          0, 1, 0, 1, 0,
                          0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0);
                    skipBy = 5;
                    break;
			case '\'':dotGrid_MP(fX,fY,
				0,1,0,
				0,1,0,
				0,0,0,
				0,0,0,
				0,0,0);
				break;
			case '.':dotGrid_MP(fX,fY,
				0,0,0,
				0,0,0,
				0,0,0,
				0,0,0,
				0,1,0);
				break;
			case ',':dotGrid_MP(fX,fY,
				0,0,0,
				0,0,0,
				0,0,0,
				0,0,1,
				0,1,0);
				break;
			case '!':dotGrid_MP(fX,fY,
				0,1,0,
				0,1,0,
				0,1,0,
				0,0,0,
				0,1,0);
				break;
			case '0':
			case 'O':dotGrid_MP(fX,fY,
				1,1,1,
				1,0,1,
				1,0,1,
				1,0,1,
				1,1,1);
				break;
			case '1':
			case 'I':dotGrid_MP(fX,fY,
				1,0,0,
				1,0,0,
				1,0,0,
				1,0,0,
				1,0,0);
				skipBy = 2; break;
			case '2': dotGrid_MP(fX,fY,
				1,1,1,
				0,0,1,
				1,1,1,
				1,0,0,
				1,1,1);
				break;
			case '3': dotGrid_MP(fX,fY,
				1,1,1,
				0,0,1,
				0,1,1,
				0,0,1,
				1,1,1);
				break;
			case '4': dotGrid_MP(fX,fY,
				1,0,1,
				1,0,1,
				1,1,1,
				0,0,1,
				0,0,1);
				break;
			case '5': dotGrid_MP(fX,fY,
				1,1,1,
				1,0,0,
				1,1,1,
				0,0,1,
				1,1,1);
				break;
			case '6': dotGrid_MP(fX,fY,
				1,1,1,
				1,0,0,
				1,1,1,
				1,0,1,
				1,1,1);
				break;
			case '7': dotGrid_MP(fX,fY,
				1,1,1,
				0,0,1,
				0,0,1,
				0,0,1,
				0,0,1);
				break;
			case '8': dotGrid_MP(fX,fY,
				1,1,1,
				1,0,1,
				1,1,1,
				1,0,1,
				1,1,1);
				break;
			case '9': dotGrid_MP(fX,fY,
				1,1,1,
				1,0,1,
				1,1,1,
				0,0,1,
				0,0,1);
				break;
			case 'A': dotGrid_MP(fX,fY,
				1,1,1,
				1,0,1,
				1,1,1,
				1,0,1,
				1,0,1);
				break;
			case 'B': dotGrid_MP(fX,fY,
				1,1,0,
				1,0,1,
				1,1,0,
				1,0,1,
				1,1,0);
				break;
			case 'C': dotGrid_MP(fX,fY,
				1,1,1,
				1,0,0,
				1,0,0,
				1,0,0,
				1,1,1);
				break;
			case 'D': dotGrid_MP(fX,fY,
				1,1,0,
				1,0,1,
				1,0,1,
				1,0,1,
				1,1,0);
				break;
			case 'E': dotGrid_MP(fX,fY,
				1,1,1,
				1,0,0,
				1,1,0,
				1,0,0,
				1,1,1);
				break;
			case 'F': dotGrid_MP(fX,fY,
				1,1,1,
				1,0,0,
				1,1,0,
				1,0,0,
				1,0,0);
				break;
			case 'G': dotGrid_MP(fX,fY,
				1,1,1,
				1,0,0,
				1,0,0,
				1,0,1,
				1,1,1);
				break;
			case 'H': dotGrid_MP(fX,fY,
				1,0,1,
				1,0,1,
				1,1,1,
				1,0,1,
				1,0,1);
				break;
				// skipping I, caught in same case as '1'
			case 'J': dotGrid_MP(fX,fY,
				0,0,1,
				0,0,1,
				0,0,1,
				1,0,1,
				1,1,1);
				break;
			case 'K': dotGrid_MP(fX,fY,
				1,0,1,
				1,0,1,
				1,1,0,
				1,0,1,
				1,0,1);
				break;
			case 'L': dotGrid_MP(fX,fY,
				1,0,0,
				1,0,0,
				1,0,0,
				1,0,0,
				1,1,1);
				break;
			case 'M': dotGrid_MPWide(fX,fY,
				1,0,0,0,1,
				1,1,0,1,1,
				1,0,1,0,1,
				1,0,0,0,1,
				1,0,0,0,1);
				skipBy = 6;
				break;
			case 'N': dotGrid_MPWide(fX,fY,
				1,0,0,1,0,
				1,1,0,1,0,
				1,0,1,1,0,
				1,0,0,1,0,
				1,0,0,1,0);
				skipBy = 5;
				break;
				// no 'O' same case as '0'
			case 'P': dotGrid_MP(fX,fY,
				1,1,0,
				1,0,1,
				1,1,0,
				1,0,0,
				1,0,0);
				break;
			case 'Q': dotGrid_MPWide(fX,fY,
				1,1,1,1,1,
				1,0,0,0,1,
				1,0,1,0,1,
				1,1,1,1,1,
				0,0,0,1,0);
				skipBy = 6;
				break;
			case 'R': dotGrid_MP(fX,fY,
				1,1,0,
				1,0,1,
				1,1,0,
				1,0,1,
				1,0,1);
				break;
			case 'S': dotGrid_MP(fX,fY,
				1,1,1,
				1,0,0,
				1,1,1,
				0,0,1,
				1,1,1);
				break;
			case 'T': dotGrid_MP(fX,fY,
				1,1,1,
				0,1,0,
				0,1,0,
				0,1,0,
				0,1,0);
				break;
			case 'U': dotGrid_MP(fX,fY,
				1,0,1,
				1,0,1,
				1,0,1,
				1,0,1,
				1,1,1);
				break;
			case 'V': dotGrid_MP(fX,fY,
				1,0,1,
				1,0,1,
				1,0,1,
				1,0,1,
				0,1,0);
				break;
			case 'W': dotGrid_MPWide(fX,fY,
				1,0,0,0,1,
				1,0,0,0,1,
				1,0,1,0,1,
				1,1,0,1,1,
				1,0,0,0,1);
				skipBy = 6;
				break;
			case 'X': dotGrid_MP(fX,fY,
				1,0,1,
				1,0,1,
				0,1,0,
				1,0,1,
				1,0,1);
				break;
			case 'Y': dotGrid_MP(fX,fY,
				1,0,1,
				1,0,1,
				0,1,0,
				0,1,0,
				0,1,0);
				break;
			case 'Z': dotGrid_MP(fX,fY,
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

	void dotGrid_MP(int atX,int atY,
	             int at00, int at10, int at20,
	             int at01, int at11, int at21,
	             int at02, int at12, int at22,
	             int at03, int at13, int at23,
	             int at04, int at14, int at24) {
		if(atX >= screenWidth-3) { // avoid out of array accesses
			return;
		}
		if(at00==1) { fastDrawBoxAt(atX,atY); }
		if(at10==1) { fastDrawBoxAt(atX+1,atY); }
		if(at20==1) { fastDrawBoxAt(atX+2,atY); }
		if(at01==1) { fastDrawBoxAt(atX,atY+1); }
		if(at11==1) { fastDrawBoxAt(atX+1,atY+1); }
		if(at21==1) { fastDrawBoxAt(atX+2,atY+1); }
		if(at02==1) { fastDrawBoxAt(atX,atY+2); }
		if(at12==1) { fastDrawBoxAt(atX+1,atY+2); }
		if(at22==1) { fastDrawBoxAt(atX+2,atY+2); }
		if(at03==1) { fastDrawBoxAt(atX,atY+3); }
		if(at13==1) { fastDrawBoxAt(atX+1,atY+3); }
		if(at23==1) { fastDrawBoxAt(atX+2,atY+3); }
		if(at04==1) { fastDrawBoxAt(atX,atY+4); }
		if(at14==1) { fastDrawBoxAt(atX+1,atY+4); }
		if(at24==1) { fastDrawBoxAt(atX+2,atY+4); }
	}

	void dotGrid_MPWide(int atX,int atY,
		int at00, int at10, int at20, int at30, int at40,
		int at01, int at11, int at21, int at31, int at41,
		int at02, int at12, int at22, int at32, int at42,
		int at03, int at13, int at23, int at33, int at43,
		int at04, int at14, int at24, int at34, int at44) {
		if(atX >= screenWidth-5) { // avoid out of array accesses
			return;
		}
		if(at00==1) { fastDrawBoxAt(atX,atY); }
		if(at10==1) { fastDrawBoxAt(atX+1,atY); }
		if(at20==1) { fastDrawBoxAt(atX+2,atY); }
		if(at30==1) { fastDrawBoxAt(atX+3,atY); }
		if(at40==1) { fastDrawBoxAt(atX+4,atY); }
		if(at01==1) { fastDrawBoxAt(atX,atY+1); }
		if(at11==1) { fastDrawBoxAt(atX+1,atY+1); }
		if(at21==1) { fastDrawBoxAt(atX+2,atY+1); }
		if(at31==1) { fastDrawBoxAt(atX+3,atY+1); }
		if(at41==1) { fastDrawBoxAt(atX+4,atY+1); }
		if(at02==1) { fastDrawBoxAt(atX,atY+2); }
		if(at12==1) { fastDrawBoxAt(atX+1,atY+2); }
		if(at22==1) { fastDrawBoxAt(atX+2,atY+2); }
		if(at32==1) { fastDrawBoxAt(atX+3,atY+2); }
		if(at42==1) { fastDrawBoxAt(atX+4,atY+2); }
		if(at03==1) { fastDrawBoxAt(atX,atY+3); }
		if(at13==1) { fastDrawBoxAt(atX+1,atY+3); }
		if(at23==1) { fastDrawBoxAt(atX+2,atY+3); }
		if(at33==1) { fastDrawBoxAt(atX+3,atY+3); }
		if(at43==1) { fastDrawBoxAt(atX+4,atY+3); }
		if(at04==1) { fastDrawBoxAt(atX,atY+4); }
		if(at14==1) { fastDrawBoxAt(atX+1,atY+4); }
		if(at24==1) { fastDrawBoxAt(atX+2,atY+4); }
		if(at34==1) { fastDrawBoxAt(atX+3,atY+4); }
		if(at44==1) { fastDrawBoxAt(atX+4,atY+4); }
	}

	public void fastDrawBoxAt(int leftSideX, int topSideY) {
		drawBoxAt(MP*leftSideX, MP*topSideY, MP, MP, sharedColor);
	}
}