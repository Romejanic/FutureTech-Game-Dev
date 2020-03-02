using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NoClip))]
public class NoClipEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        NoClip script = target as NoClip;

        // introduction at beginning of script
        EditorGUILayout.LabelField("No Clip Camera", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("A free-moving camera which does not collide with objects");
        EditorGUILayout.Space();

        // debug option (if in play mode)
        if(Application.isPlaying) {
            Vector2 rotation = script.getRotation();
            EditorGUILayout.LabelField("Current Rotation", EditorStyles.boldLabel);
            AddKeyValuePair("X", rotation.x.ToString());
            AddKeyValuePair("Y", rotation.y.ToString());
            EditorGUILayout.Space();
        }

        // controls
        EditorGUILayout.LabelField("Controls", EditorStyles.boldLabel);
        AddKeyValuePair("WASD/Arrow Keys", "Move");
        AddKeyValuePair("Mouse", "Look Around");
        AddKeyValuePair("Shift", "Move faster");
        AddKeyValuePair("Q/E", "Move up and down");
        EditorGUILayout.Space();

        // movement
        EditorGUILayout.LabelField("Movement Settings", EditorStyles.boldLabel);
        script.moveSpeed = EditorGUILayout.FloatField("Move Speed", script.moveSpeed);
        script.shiftMultiplier = EditorGUILayout.FloatField("Shift Multiplier", script.shiftMultiplier);
        script.smoothSpeed = EditorGUILayout.FloatField("Smooth Speed", script.smoothSpeed);
        EditorGUILayout.Space();

        // 
        EditorGUILayout.LabelField("Mouse Settings", EditorStyles.boldLabel);
        script.lookSpeed = EditorGUILayout.Slider("Look Speed", script.lookSpeed, 0f, 360f);
        script.lookLimit = EditorGUILayout.Slider("Up/Down Limit", script.lookLimit, 0f, 360f);
        script.invertY = EditorGUILayout.Toggle("Invert Y-axis?", script.invertY);
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
