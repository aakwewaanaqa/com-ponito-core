using Ponito.Core;
using UnityEditor;
using UnityEngine;

namespace Ponito.Inspectors
{
    [CustomPropertyDrawer(typeof(FloatRange))]
    public class FloatRangeDrawer : PropertyDrawer
    {
        private static float lineHeight => EditorGUIUtility.singleLineHeight;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var minProp = property.FindPropertyRelative("min");
            var maxProp = property.FindPropertyRelative("max");
            {
                var pos  = position.position;
                var size = position.size;
                {
                    var rect      = new Rect(pos, size);
                    var contents = new GUIContent[] { new("Min"), new("Max") };
                    var values    = new[] { minProp.floatValue, maxProp.floatValue };
                    EditorGUI.MultiFloatField(rect, label, contents, values);
                    minProp.floatValue = values[0];
                    maxProp.floatValue = values[1];
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return lineHeight;
        }
    }
}