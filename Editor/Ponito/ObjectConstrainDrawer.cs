using Ponito.DIAttributes;
using UnityEditor;
using UnityEngine;

namespace Ponito
{
    [CustomPropertyDrawer(typeof(ObjectConstrain))]
    public class ObjectConstrainDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var att = attribute as ObjectConstrain;
            EditorGUI.PrefixLabel(position, label);
            EditorGUI.ObjectField(position, property, att.Type);
        }
    }
}