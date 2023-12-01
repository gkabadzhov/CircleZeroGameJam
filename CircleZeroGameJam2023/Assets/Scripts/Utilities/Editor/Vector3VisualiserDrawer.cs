using OTBG.Utilities.Attributes;
using System;
using UnityEditor;
using UnityEngine;

namespace OTBG.Utilities.Editor
{


    [CustomPropertyDrawer(typeof(Vector3VisualiserAttribute))]
    public class Vector3VisualiserDrawer : PropertyDrawer
    {
        private readonly string[] vector3Options = new string[] {
        "Zero", "One", "Up", "Down", "Left", "Right", "Forward", "Back"
    };

        private Vector3 GetVector3Value(string option)
        {
            switch (option)
            {
                case "Zero": return Vector3.zero;
                case "One": return Vector3.one;
                case "Up": return Vector3.up;
                case "Down": return Vector3.down;
                case "Left": return Vector3.left;
                case "Right": return Vector3.right;
                case "Forward": return Vector3.forward;
                case "Back": return Vector3.back;
                default: return Vector3.zero;
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Vector3)
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
            int currentIndex = Array.IndexOf(vector3Options, Vector3ToString(property.vector3Value));
            int selectedIndex = EditorGUI.Popup(position, currentIndex, vector3Options);

            if (selectedIndex != currentIndex && selectedIndex >= 0)
            {
                property.vector3Value = GetVector3Value(vector3Options[selectedIndex]);
            }

            EditorGUI.EndProperty();
        }

        private string Vector3ToString(Vector3 vector)
        {
            if (vector == Vector3.zero) return "Zero";
            if (vector == Vector3.one) return "One";
            if (vector == Vector3.up) return "Up";
            if (vector == Vector3.down) return "Down";
            if (vector == Vector3.left) return "Left";
            if (vector == Vector3.right) return "Right";
            if (vector == Vector3.forward) return "Forward";
            if (vector == Vector3.back) return "Back";
            return null; // Or some default value
        }
    }
}