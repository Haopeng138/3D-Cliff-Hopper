using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(TileWeight))]
public class TileWeightEditor : PropertyDrawer {

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        // Create property container element.
        var container = new VisualElement();

        // Create property fields.
        var tile = new PropertyField(property.FindPropertyRelative("tile"));
        var weight = new PropertyField(property.FindPropertyRelative("weight"));
        var canRotate = new PropertyField(property.FindPropertyRelative("canBeRotate"));

        // Add fields to the container.
        container.Add(tile);
        container.Add(weight);
        container.Add(canRotate);

        return container;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), GUIContent.none);
        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        

        // Calculate rects
        var tileRect = new Rect(position.x, position.y, position.width - 75, position.height);
        var weightRect = new Rect(position.x + position.width - 70 , position.y, 35, position.height);
        var rotateRect = new Rect(position.x + position.width - 30, position.y, 25, position.height);

        var tile = property.FindPropertyRelative("tile");
        var weight = property.FindPropertyRelative("weight");
        var canRotate = property.FindPropertyRelative("canBeRotate");

        // Draw fields - pass GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(tileRect, tile, GUIContent.none);
        EditorGUI.PropertyField(weightRect, weight, GUIContent.none);
        EditorGUI.PropertyField(rotateRect, canRotate, GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }

}