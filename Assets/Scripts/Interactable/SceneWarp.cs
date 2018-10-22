using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MouseTipOnLook))]
public class SceneWarp : MonoBehaviour {
    public string sceneName;
    public Transform returnLocation;
    public static string fromScene;

	private void Awake()
	{
	}

	public void triggerAction () {
        fromScene = SceneManager.GetActiveScene().name;
        if(SquashTransition.instance) {
            SquashTransition.instance.startTransition(sceneName);
            SquashTransition.instance = null;
        } else {
            SceneManager.LoadScene(sceneName);
        }
	}
}
