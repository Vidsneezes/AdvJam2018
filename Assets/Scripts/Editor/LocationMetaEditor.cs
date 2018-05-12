using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LocationMeta))]
public class LocationMetaEditor : Editor
{

    SerializedProperty width;
    SerializedProperty height;
    SerializedProperty mainView;
    SerializedProperty locationId;

    void OnEnable()
    {
        width = serializedObject.FindProperty("unitWidth");
        height = serializedObject.FindProperty("unitHeight");
        mainView = serializedObject.FindProperty("mainDisplay");
        locationId = serializedObject.FindProperty("locationId");

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(locationId);
        EditorGUILayout.PropertyField(width);
        EditorGUILayout.PropertyField(height);
        EditorGUILayout.PropertyField(mainView);

        serializedObject.ApplyModifiedProperties();

    }

    public void OnSceneGUI()
    {
        var t = (target as LocationMeta);
        t.mainDisplay.size = new Vector2(t.unitWidth, t.unitHeight);
    }
}