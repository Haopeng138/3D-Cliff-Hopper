using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BaseTileSO))]
public class BaseTileSOEditor : Editor
{
    
    private SerializedProperty tileGOProp;
    private SerializedProperty tileScaffoldGOProp;
    private SerializedProperty scaffoldHeightProp;
    private SerializedProperty tileStrideProp;
    private SerializedProperty localOffsetProp; 
    private SerializedProperty tileRotationProp;
    private SerializedProperty canBeReplacedProp;
    private SerializedProperty tileWeightsProp;
    private GUIStyle boxStyle;

    private void OnEnable()
    {
        tileGOProp = serializedObject.FindProperty("tileGO");
        tileScaffoldGOProp = serializedObject.FindProperty("tileScaffoldGO");
        scaffoldHeightProp = serializedObject.FindProperty("scaffoldHeight");
        tileStrideProp = serializedObject.FindProperty("nextTileStride");
        localOffsetProp = serializedObject.FindProperty("localOffset");
        tileRotationProp = serializedObject.FindProperty("tileRotation");
        canBeReplacedProp = serializedObject.FindProperty("canBeReplaced");
        tileWeightsProp = serializedObject.FindProperty("tileWeights");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Tile");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(tileGOProp, label: GUIContent.none);
        EditorGUILayout.PropertyField(canBeReplacedProp, label: new GUIContent("Replaceable?"));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("Scaffold");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(tileScaffoldGOProp, GUIContent.none);
        EditorGUILayout.PropertyField(scaffoldHeightProp, new GUIContent("Height"));
        EditorGUILayout.EndHorizontal();

        EditorGUI.indentLevel++;
        EditorGUILayout.BeginVertical();
        EditorGUILayout.PropertyField(tileStrideProp);
        EditorGUILayout.PropertyField(localOffsetProp);
        EditorGUILayout.PropertyField(tileRotationProp);
        EditorGUILayout.EndVertical();
        EditorGUI.indentLevel--;
        
        EditorGUILayout.Separator();

        EditorGUILayout.EndVertical();
        
        tileWeightsDrawer();


        serializedObject.ApplyModifiedProperties();
    }

    private void tileWeightsDrawer(){
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
    }

    private void initBoxStyle(){
        
        if (boxStyle != null) return;
        
        boxStyle = new GUIStyle(GUI.skin.box);
        boxStyle.normal.background = MakeTex(2, 2, new Color(0.8f, 0.8f, 0.8f, 1f));
        boxStyle.padding = new RectOffset(4, 4, 4, 4);
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
