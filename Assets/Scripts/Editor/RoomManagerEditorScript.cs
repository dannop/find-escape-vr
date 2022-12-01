using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomManager))]
public class RoomManagerEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.HelpBox("This script is responsible for connecting to Photon Server", MessageType.Info);

        RoomManager roomManager = (RoomManager)target;
        
        if (GUILayout.Button("Join School Room"))
        {
            roomManager.OnEnterButtonClickedSchool();
        }

        if (GUILayout.Button("Join Outdoor Room"))
        {
            roomManager.OnEnterButtonClickedOutdoor();
        }

        EditorGUILayout.HelpBox("Clicando aqui você irá entrar no Hall de Jogadores", MessageType.Info);

        if (GUILayout.Button("Entrar no Hall"))
        {
            roomManager.OnEnterHall();
        }
    }
}
