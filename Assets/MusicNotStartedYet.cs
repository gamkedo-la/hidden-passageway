using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicNotStartedYet : MonoBehaviour {
    [FMODUnity.EventRef]
    public string songEvtName;
    private bool songStartedYet = false;

    public static MusicNotStartedYet instance;
    public MusicPlayerEventMover mover;

    private void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
        mover.songEvt = FMODUnity.RuntimeManager.CreateInstance(songEvtName);
        // mover.songEvt.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(mover.gameObject));

        /*if (SceneManager.GetActiveScene().name != "MainHub" ||
            SceneWarp.fromScene != null)
        {*/
         StartSong();
        //}
	}

    public void StartSong()
    {
        FMOD.Studio.PLAYBACK_STATE stateNow;
        mover.songEvt.getPlaybackState(out stateNow);
        if (stateNow != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            StopSong();
            mover.songEvt = FMODUnity.RuntimeManager.CreateInstance(songEvtName);
            mover.songEvt.start();
            songStartedYet = true;
        }
    }

    public void StopSong()
    {
        mover.songEvt.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        mover.songEvt.release();
        songStartedYet = false;
    }
	
	// Update is called once per frame
    /*void Update () {
        if (Input.anyKey)
        {
            StartSong();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            mover.songEvt = FMODUnity.RuntimeManager.CreateInstance(songEvtName);
            songStartedYet = false;
            StartSong();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            songStartedYet = false;
            StartSong();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            StopSong();
        }
	}*/
}
