using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MouseTipOnLook))]
public class SceneWarp : MonoBehaviour {
    public string sceneName;
    public string mediumName;
    public Transform returnLocation;
    public static string fromScene;
    public static string onMedium;
    public float rotX = 0.0f;
    public float rotY = -90.0f;

	public void triggerAction () {
        fromScene = SceneManager.GetActiveScene().name;
        if(mediumName != null && mediumName.Length > 1) {
            onMedium = mediumName;
        }

        if(SquashTransition.instance) {
            SquashTransition.instance.startTransition(sceneName);
            WalkControl.instance.areFeetLocked = true;
            if (fromScene == "MainHub")
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform eachChild = transform.GetChild(i);
                    eachChild.gameObject.layer = LayerMask.NameToLayer("MediaForeground");
                    for (int ii = 0; ii < eachChild.childCount; ii++)
                    {
                        eachChild.GetChild(ii).gameObject.layer = LayerMask.NameToLayer("MediaForeground");
                    }
                }
                StartCoroutine(spinToCam());
            }
            SquashTransition.instance = null;
        } else {
            SceneManager.LoadScene(sceneName);
        }
	}

    IEnumerator spinToCam()
    {
        while (true)
        {
            Vector3 lookFocus = Camera.main.transform.position + Camera.main.transform.forward * 0.58f;
            float referenceFramerate = 30.0f;
            float fadeFactor = 0.05f;
            float blend = 1.0f - Mathf.Pow(1.0f - fadeFactor, Time.deltaTime * referenceFramerate);
            
            transform.position = Vector3.Lerp(transform.position, lookFocus, fadeFactor);
            transform.rotation= Quaternion.Slerp(transform.rotation,
                                                 Quaternion.LookRotation(transform.position - Camera.main.transform.position)
                                                 * Quaternion.AngleAxis(30.0f, Vector3.up)
                                                 * Quaternion.AngleAxis(rotX+180.0f, Vector3.forward)
                                                 * Quaternion.AngleAxis(rotY,Vector3.right)
                                                 ,
                                                 fadeFactor);
            // transform.Rotate(Camera.main.transform.up, 2.0f);
            yield return new WaitForEndOfFrame();
        }
    }
}
