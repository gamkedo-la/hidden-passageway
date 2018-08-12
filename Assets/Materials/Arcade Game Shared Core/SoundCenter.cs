using UnityEngine;
using System.Collections;

public class SoundCenter : MonoBehaviour {
	public static SoundCenter instance;

	public AudioClip coinGet;
	public AudioClip coinSpend;
	public AudioClip billGet;
	public AudioClip adultTalk;

	void Awake() {
		if(instance) {
			Destroy(instance.gameObject);
		}
		instance = this;
	}

	public void PlayClipOn(AudioClip clip, Vector3 pos, float atVol = 1.0f,
	                       Transform attachToParent = null) {
		GameObject tempGO = new GameObject("TempAudio"); // create the temp object
		tempGO.transform.position = pos; // set its position
		if(attachToParent != null) {
			tempGO.transform.parent = attachToParent.transform;
		}
		AudioSource aSource = tempGO.AddComponent<AudioSource>() as AudioSource; // add an audio source
		aSource.clip = clip; // define the clip
		aSource.volume = atVol;
		aSource.pitch = Random.Range(0.85f,1.15f);
		// set other aSource properties here, if desired
		aSource.Play(); // start the sound
		Destroy(tempGO, clip.length/aSource.pitch); // destroy object after clip duration
	}
}
