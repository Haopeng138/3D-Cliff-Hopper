using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BaseTileSO))]
public class BaseTileSOEditor : Editor
{
    private SerializedProperty tileWeightsProp;
    private GUIStyle boxStyle;

    private void OnEnable()
    {
        tileWeightsProp = serializedObject.FindProperty("tileWeights");
    }

    private void initBoxStyle(){
        if (boxStyle != null) return;
        
        boxStyle = new GUIStyle(GUI.skin.box);
        boxStyle.normal.background = MakeTex(2, 2, new Color(0.8f, 0.8f, 0.8f, 1f));
        boxStyle.padding = new RectOffset(4, 4, 4, 4);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();


        EditorGUILayout.PropertyField(serializedObject.FindProperty("tileGO"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("isSafe"));

        EditorGUI.indentLevel++;
        EditorGUILayout.BeginVertical();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("tileStride"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("offset"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("tileRotation"));
        EditorGUILayout.EndVertical();
        EditorGUI.indentLevel--;
        
        EditorGUILayout.Separator();

        initBoxStyle();
        Rect backgroundRect = EditorGUILayout.BeginVertical(boxStyle);
        
        EditorGUI.indentLevel++;

        EditorGUILayout.BeginHorizontal();
       
        //float labelWidth = (EditorGUILayout.GetControlRect().width - 20) / 2; // Subtract the button width
         float buttonWidth = 20;
        float labelWidth = (EditorGUIUtility.currentViewWidth - buttonWidth - 40) / 2; // Subtract button width and spacing
        EditorGUILayout.LabelField("Tile Options", EditorStyles.boldLabel, GUILayout.Width(labelWidth));
        EditorGUILayout.LabelField("Weight", EditorStyles.boldLabel, GUILayout.Width(labelWidth));
        
        if (GUILayout.Button("+", GUILayout.Width(20)))
        {
            tileWeightsProp.InsertArrayElementAtIndex(tileWeightsProp.arraySize);
        }
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < tileWeightsProp.arraySize; i++)
        {
            SerializedProperty tileWeightProp = tileWeightsProp.GetArrayElementAtIndex(i);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(tileWeightProp.FindPropertyRelative("tile"), GUIContent.none);
            EditorGUILayout.PropertyField(tileWeightProp.FindPropertyRelative("weight"), GUIContent.none);
            if (GUILayout.Button("-", GUILayout.Width(20)))
            {
                tileWeightsProp.DeleteArrayElementAtIndex(i);
                i--;
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

        EditorGUI.indentLevel--;

        serializedObject.ApplyModifiedProperties();
    }

    private Texture2D MakeTex(int width, int height, Color color)
    {
        Color[] pixels = new Color[width * height];
        for (int i = 0; i < pixels.Length; ++i)
        {
            pixels[i] = color;
        }

        Texture2D texture = new Texture2D(width, height);
        texture.SetPixels(pixels);
        texture.Apply();

        return texture;
    }
}
