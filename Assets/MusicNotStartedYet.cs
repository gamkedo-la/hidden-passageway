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
        mover.songEvt.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(mover.gameObject));

        if (SceneManager.GetActiveScene().name != "MainHub" ||
            SceneWarp.fromScene != null)
        {
            StartSong();
        }
	}

    void StartSong()
    {
        if (songStartedYet == false)
        {
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
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            StartSong();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            StopSong();
        }
	}
}
