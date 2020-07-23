using UnityEngine;
using UnityEditor;
using System.Text;

public class RoughnessConverter : EditorWindow
{
    private Texture2D metallicTexture;
    private Texture2D roughnessTexture;

    void OnGUI()
    {
        // title
        EditorGUILayout.LabelField("Roughness Converter 3000", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Dealing with Unity being annoying since 2020");
        EditorGUILayout.Space();
        // fields
        this.metallicTexture = EditorGUILayout.ObjectField("Metallic Texture", this.metallicTexture, typeof(Texture2D), false) as Texture2D;
        this.roughnessTexture = EditorGUILayout.ObjectField("Roughness Texture", this.roughnessTexture, typeof(Texture2D), false) as Texture2D;
        EditorGUILayout.Space();
        // error checking
        bool readableFlag = true;
        if(this.metallicTexture != null) {
            readableFlag = this.metallicTexture.isReadable && readableFlag;
        }
        if(this.roughnessTexture != null) {
            readableFlag = this.roughnessTexture.isReadable && readableFlag;
        }
        if(!readableFlag) {
            EditorGUILayout.HelpBox("One or more of your textures are not readable! Please go to their import settings and make sure they are marked as readable", MessageType.Error);
        }
        // convert button
        EditorGUI.BeginDisabledGroup(this.roughnessTexture == null || !readableFlag);
        if(GUILayout.Button("Convert")) {
            this.ConvertTextures();
        }
        EditorGUI.EndDisabledGroup();
    }

    void ConvertTextures()
    {
        string path = this.roughnessTexture.name + "_genMetallic.png";
        Texture2D tex = new Texture2D(roughnessTexture.width, roughnessTexture.height, TextureFormat.RGBA32, true);
        for(int y = 0; y < tex.height; y++) {
            for(int x = 0; x < tex.width; x++) {
                float metallic = this.getMetallicValue(x, y);
                float smoothness = this.getSmoothnessValue(x, y);
                Color outColor = new Color(metallic, metallic, metallic, smoothness);
                tex.SetPixel(x, y, outColor);
            }
        }
        tex.Apply();
        System.IO.File.WriteAllBytes(Application.dataPath + "/" + path, tex.EncodeToPNG());
        AssetDatabase.Refresh();
    }

    float getMetallicValue(int x, int y)
    {
        return this.metallicTexture != null ? this.metallicTexture.GetPixel(x, y).r : 1f;
    }

    float getSmoothnessValue(int x, int y)
    {
        return Mathf.Clamp01(1f - this.roughnessTexture.GetPixel(x, y).r);
    }

    //==========================================================

    [MenuItem("Window/Game Dev Workshop/Roughness Converter")]
    public static void OpenWindow()
    {
        EditorWindow.GetWindow<RoughnessConverter>().Show();
    }

}
