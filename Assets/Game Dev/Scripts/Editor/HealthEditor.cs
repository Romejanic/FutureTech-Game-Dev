using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Health))]
public class HealthEditor : Editor
{

    public override void OnInspectorGUI()
    {
        Health script = target as Health;

        // introduction at beginning of script
        EditorGUILayout.LabelField("Health", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Adds health to a character and allows them to die.");
        EditorGUILayout.Space();

        // health
        EditorGUILayout.LabelField("Health Meter", EditorStyles.boldLabel);
        if(Application.isPlaying) {
            AddKeyValuePair("Current Health", script.GetCurrentHealth().ToString(), false);
        }
        script.maxHealth = Mathf.Max(0f, EditorGUILayout.FloatField("Max Health", script.maxHealth));
        EditorGUILayout.Space();

        // events
        serializedObject.Update();
        EditorGUILayout.LabelField("Events", EditorStyles.boldLabel);
        // on die
        SerializedProperty prop = serializedObject.FindProperty("onDieEvent");
        EditorGUILayout.PropertyField(prop);
        // on hurt
        prop = serializedObject.FindProperty("onHurtEvent");
        EditorGUILayout.PropertyField(prop);
        // on heal
        prop = serializedObject.FindProperty("onHealEvent");
        EditorGUILayout.PropertyField(prop);
        serializedObject.ApplyModifiedProperties();
    }

    private void AddKeyValuePair(string key, string value, bool bold = true)
    {
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField(key, bold ? EditorStyles.boldLabel : EditorStyles.label);
            EditorGUILayout.LabelField(value);
        }
        EditorGUILayout.EndHorizontal();
    }

}
