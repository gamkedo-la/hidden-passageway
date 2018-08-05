using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerPrefsHelper
{
    public static string GetPrefsName(GameObject go)
    {
        return SceneManager.GetActiveScene().name + GetGameObjectHierarchyName(go);
    }
    private static string GetGameObjectHierarchyName(GameObject go)
    {
        string name = "";

        if (go.transform.parent && go.transform.parent.gameObject) {
            name = GetGameObjectHierarchyName(go.transform.parent.gameObject) + "__";
        }

        return name + go.name;
    }
}
