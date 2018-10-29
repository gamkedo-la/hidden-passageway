using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatCodes : MonoBehaviour
{
	void Update ()
	{
		// Check Cheat Code for Levels 0-9
		CheckCheatCode( KeyCode.Alpha0, 0, KeyCode.End );
		CheckCheatCode( KeyCode.Alpha1, 1, KeyCode.End );
		CheckCheatCode( KeyCode.Alpha2, 2, KeyCode.End );
		CheckCheatCode( KeyCode.Alpha3, 3, KeyCode.End );
		CheckCheatCode( KeyCode.Alpha4, 4, KeyCode.End );
		CheckCheatCode( KeyCode.Alpha5, 5, KeyCode.End );
		CheckCheatCode( KeyCode.Alpha6, 6, KeyCode.End );
		CheckCheatCode( KeyCode.Alpha7, 7, KeyCode.End );
		CheckCheatCode( KeyCode.Alpha8, 8, KeyCode.End );
		CheckCheatCode( KeyCode.Alpha9, 9, KeyCode.End );

		// Check Cheat Code for Levels 10-17
		CheckCheatCode( KeyCode.Alpha0, 10, KeyCode.Home );
		CheckCheatCode( KeyCode.Alpha1, 11, KeyCode.Home );
		CheckCheatCode( KeyCode.Alpha2, 12, KeyCode.Home );
		CheckCheatCode( KeyCode.Alpha3, 13, KeyCode.Home );
		CheckCheatCode( KeyCode.Alpha4, 14, KeyCode.Home );
		CheckCheatCode( KeyCode.Alpha5, 15, KeyCode.Home );
		CheckCheatCode( KeyCode.Alpha6, 16, KeyCode.Home );
		CheckCheatCode( KeyCode.Alpha7, 17, KeyCode.Home );
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
