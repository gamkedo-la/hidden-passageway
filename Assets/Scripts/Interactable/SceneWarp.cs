using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MouseTipOnLook))]
public class SceneWarp : MonoBehaviour {
    public string sceneName;
    public Transform returnLocation;
    public static string fromScene;

    public static SceneWarp instance;

	private void Awake()
	{
        instance = this;
	}

	public void triggerAction () {
        fromScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
	}
}
