using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VirtualWorldManager))]
public class VirtualWorldManagerEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        VirtualWorldManager virtualWorldManager = (VirtualWorldManager)target;

        
        EditorGUILayout.HelpBox("Clicando aqui voc� ir� entrar no Seu Quarto", MessageType.Info);

        if (GUILayout.Button("Voltar para o quarto"))
        {
            virtualWorldManager.LeaveRoomAndLoadHomeScene();
        }

        EditorGUILayout.HelpBox("Clicando aqui voc� ir� entrar sala do Trono", MessageType.Info);

        if (GUILayout.Button("Entrar no Sala do Trono"))
        {
            virtualWorldManager.OnEnterThrone();
        }

        EditorGUILayout.HelpBox("Clicando aqui voc� ir� entrar sala do Trono", MessageType.Info);

        if (GUILayout.Button("Entrar no Hall"))
        {
            virtualWorldManager.OnEnterHall();
        }
    }
}
