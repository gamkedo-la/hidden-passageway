using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SlideToPos))]
class DestinationShow : Editor
{
    void OnSceneGUI()
    {
        SlideToPos connectedObjects = target as SlideToPos;
        if (connectedObjects.endPos != null)
        {
            Handles.DrawLine(connectedObjects.transform.position,
                connectedObjects.endPos.transform.position);
        }
    }
}