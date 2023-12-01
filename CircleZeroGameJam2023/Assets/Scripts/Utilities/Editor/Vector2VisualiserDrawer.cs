using OTBG.Utilities.Attributes;
using System;
using UnityEditor;
using UnityEngine;
namespace OTBG.Utilities.Editor
{

    [CustomPropertyDrawer(typeof(Vector2VisualiserAttribute))]
    public class Vector2VisualiserDrawer : PropertyDrawer
    {
        private readonly string[] vector2Options = new string[] {
        "Zero", "One", "Up", "Down", "Left", "Right"
    };

        private Vector2 GetVector2Value(string option)
        {
            switch (option)
            {
                case "Zero": return Vector2.zero;
                case "One": return Vector2.one;
                case "Up": return Vector2.up;
                case "Down": return Vector2.down;
                case "Left": return Vector2.left;
                case "Right": return Vector2.right;
                default: return Vector2.zero;
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Vector2)
            {
                EditorGUI.LabelField(position, label.text, "Use Vector3Visualiser with Vector3.");
                return;
            }

            EditorGUI.BeginProperty(position, label, property);

            // Draw the label
            position.width /= 2;
            EditorGUI.LabelField(position, label);

            // Draw the popup
            position.x += position.width;
            int currentIndex = Array.IndexOf(vector2Options, Vector2ToString(property.vector2Value));
            int selectedIndex = EditorGUI.Popup(position, currentIndex, vector2Options);

            if (selectedIndex != currentIndex && selectedIndex >= 0)
            {
                property.vector2Value = GetVector2Value(vector2Options[selectedIndex]);
            }

            EditorGUI.EndProperty();
        }

        private string Vector2ToString(Vector2 vector)
        {
            if (vector == Vector2.zero) return "Zero";
            if (vector == Vector2.one) return "One";
            if (vector == Vector2.up) return "Up";
            if (vector == Vector2.down) return "Down";
            if (vector == Vector2.left) return "Left";
            if (vector == Vector2.right) return "Right";
            return null; // Or some default value
        }
    }
}