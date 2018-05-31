using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TriggerComponentEnable))]
class ConnectLineHandleExampleScript : Editor
{
    void OnSceneGUI()
    {
        TriggerComponentEnable connectedObjects = target as TriggerComponentEnable;
        if (connectedObjects.toEnable != null) {
            Handles.DrawLine(connectedObjects.transform.position,
                connectedObjects.toEnable.transform.position);
        }
    }
}