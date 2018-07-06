using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Cheats))]
public class CheatsInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Cheats myTarget = (Cheats)target;

        if (GUILayout.Button("Add 1000 money"))
        {
            myTarget.Add1000OfAllMoney();
        }
        //myTarget.experience = EditorGUILayout.IntField("Experience", myTarget.experience);
        //EditorGUILayout.LabelField("Level", myTarget.Level.ToString());
    }
}
