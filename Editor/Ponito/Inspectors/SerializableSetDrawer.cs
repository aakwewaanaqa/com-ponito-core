using Ponito.Core;
using UnityEditor;
using UnityEngine;

namespace Ponito.Inspectors
{
    [CustomPropertyDrawer(typeof(SerializableSet<,>))]
    public class SerializableSetDrawer : PropertyDrawer
    {
        private static float lineHeight => EditorGUIUtility.singleLineHeight;
        private static float rowHeight  => lineHeight * 2f;
        private static float padding    => 2f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            {
                var pos  = new Vector2(position.x,     position.y);
                var size = new Vector2(position.width, lineHeight);
                var rect = new Rect(pos, size);
                property.isExpanded = EditorGUI.Foldout(
                    rect,
                    property.isExpanded, label);
                if (!property.isExpanded) return;
            }

            var keys   = property.FindPropertyRelative("keys");
            var values = property.FindPropertyRelative("values");

            var keyCount   = keys.arraySize;
            var valueCount = values.arraySize;
            var count      = Mathf.Min(keyCount, valueCount);

            {
                var pos  = new Vector2(position.x,     position.y + lineHeight);
                var size = new Vector2(position.width, lineHeight);
                EditorGUI.indentLevel++;
                for (var i = 0; i < count; i++)
                {
                    var key   = keys.GetArrayElementAtIndex(i);
                    var value = values.GetArrayElementAtIndex(i);

                    {
                        var rect    = new Rect(pos, size);
                        var content = new GUIContent(key.displayName);
                        EditorGUI.PropertyField(rect, key, content);
                        pos.y += lineHeight + padding;
                    }

                    {
                        var  rect        = new Rect(pos, size);
                        var  content     = new GUIContent(value.displayName);
                        EditorGUI.PropertyField(rect, value, content, true);
                        pos.y += lineHeight + padding;
                        if (value.hasChildren)
                        {
                            pos.y += lineHeight + padding;
                        }
                    }
                }

                {
                    var rect = new Rect(pos, size);
                    if (GUI.Button(rect, "Add"))
                    {
                        keys.arraySize++;
                        values.arraySize++;
                    }
                }

                EditorGUI.indentLevel--;
            }

            {
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded) return lineHeight;

            var keys     = property.FindPropertyRelative("keys");
            var keyCount = keys.arraySize;

            return rowHeight * (keyCount + 1);
        }
    }
}