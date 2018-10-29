using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatCodes : MonoBehaviour
{
	void LateUpdate ()
	{
		// Check Cheat Code for Levels 0-9
		CheckCheatCode( KeyCode.Alpha0, 0, KeyCode.LeftBracket );
		CheckCheatCode( KeyCode.Alpha1, 1, KeyCode.LeftBracket );
		CheckCheatCode( KeyCode.Alpha2, 2, KeyCode.LeftBracket );
		CheckCheatCode( KeyCode.Alpha3, 3, KeyCode.LeftBracket );
		CheckCheatCode( KeyCode.Alpha4, 4, KeyCode.LeftBracket );
		CheckCheatCode( KeyCode.Alpha5, 5, KeyCode.LeftBracket );
		CheckCheatCode( KeyCode.Alpha6, 6, KeyCode.LeftBracket );
		CheckCheatCode( KeyCode.Alpha7, 7, KeyCode.LeftBracket );
		CheckCheatCode( KeyCode.Alpha8, 8, KeyCode.LeftBracket );
		CheckCheatCode( KeyCode.Alpha9, 9, KeyCode.LeftBracket );

		// Check Cheat Code for Levels 10-17
		CheckCheatCode( KeyCode.Alpha0, 10, KeyCode.RightBracket );
		CheckCheatCode( KeyCode.Alpha1, 11, KeyCode.RightBracket );
		CheckCheatCode( KeyCode.Alpha2, 12, KeyCode.RightBracket );
		CheckCheatCode( KeyCode.Alpha3, 13, KeyCode.RightBracket );
		CheckCheatCode( KeyCode.Alpha4, 14, KeyCode.RightBracket );
		CheckCheatCode( KeyCode.Alpha5, 15, KeyCode.RightBracket );
		CheckCheatCode( KeyCode.Alpha6, 16, KeyCode.RightBracket );
		CheckCheatCode( KeyCode.Alpha7, 17, KeyCode.RightBracket );
	}

	private void CheckCheatCode(KeyCode mainKey, int level, KeyCode modKey )
	{
		// Check if we are holding mod key, if not return
		if ( !Input.GetKey( modKey ) )
			return;

		if ( Input.GetKeyDown( mainKey ) )
		{
			MusicNotStartedYet.instance.StopSong( );
			SceneManager.LoadScene( level );
		}
	}
}
