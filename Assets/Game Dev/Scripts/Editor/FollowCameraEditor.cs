using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FollowCamera))]
public class FollowCameraEditor : Editor
{

    public override void OnInspectorGUI()
    {
        FollowCamera script = target as FollowCamera;

        // introduction
        EditorGUILayout.LabelField("Follow Camera", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("An orbiting camera which can follow one or several objects");
        EditorGUILayout.Space();

        // controls
        EditorGUILayout.LabelField("Controls", EditorStyles.boldLabel);
        AddKeyValuePair("Mouse", "Look Around");
        AddKeyValuePair("Mouse Wheel", "Zoom in/out");
        if (script.lockCursor) AddKeyValuePair("Escape", "Release mouse");
        EditorGUILayout.Space();

        // targeting
        EditorGUILayout.LabelField("Target", EditorStyles.boldLabel);
        serializedObject.Update();
        if(!script.multi) {
            SerializedProperty prop = serializedObject.FindProperty("following");
            EditorGUILayout.PropertyField(prop, new GUIContent("Following Object"), true);
        } else {
            SerializedProperty prop = serializedObject.FindProperty("followingMulti");
            EditorGUILayout.PropertyField(prop, new GUIContent("Following Objects"), true);
        }
        serializedObject.ApplyModifiedProperties();
        script.multi = EditorGUILayout.Toggle("Following several?", script.multi);
        EditorGUILayout.Space();

        // zooming
        EditorGUILayout.LabelField("Zooming", EditorStyles.boldLabel);
        script.minDistance = Mathf.Min(script.maxDistance, EditorGUILayout.FloatField("Min Distance", script.minDistance));
        script.maxDistance = Mathf.Max(script.minDistance, EditorGUILayout.FloatField("Max Distance", script.maxDistance));
        script.zoomSpeed = EditorGUILayout.FloatField("Zoom Speed", script.zoomSpeed);
        EditorGUILayout.Space();

        // mouse
        EditorGUILayout.LabelField("Mouse Settings", EditorStyles.boldLabel);
        script.lookSpeed = EditorGUILayout.Slider("Look Speed", script.lookSpeed, 0f, 360f);
        script.lookLimit = EditorGUILayout.Slider("Up/Down Limit", script.lookLimit, 0f, 90f);
        script.invertY = EditorGUILayout.Toggle("Invert Y-axis?", script.invertY);
        script.lockCursor = EditorGUILayout.Toggle("Lock mouse cursor?", script.lockCursor);
        script.smoothSpeed = EditorGUILayout.FloatField("Smooth Speed", script.smoothSpeed);
        EditorGUILayout.Space();
    }

    private void AddKeyValuePair(string key, string value)
    {
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField(key, EditorStyles.miniBoldLabel);
            EditorGUILayout.LabelField(value);
        }
        EditorGUILayout.EndHorizontal();
    }


}
