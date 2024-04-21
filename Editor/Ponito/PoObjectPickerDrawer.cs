using System;
using System.ComponentModel;
using System.Reflection;
using Ponito.Core.Extensions;
using Ponito.DIAttributes;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ponito
{
    [CustomPropertyDrawer(typeof(DIObjectPicker))]
    public class PoObjectPickerDrawer : PropertyDrawer
    {
        private void PrefixLabel(ref Rect position, GUIContent label)
        {
            var labelWidth = EditorGUIUtility.labelWidth;
            EditorGUI.PrefixLabel(position, label);
            position.x     += labelWidth;
            position.width -= labelWidth;
        }

        private GUIContent GetContent(SerializedProperty property)
        {
            var di = attribute as DIObjectPicker;
            return EditorGUIUtility.ObjectContent(property.objectReferenceValue, di.type);
        }

        private void ObjectField(ref Rect position, SerializedProperty property)
        {
            var di      = attribute as DIObjectPicker;
            var content = GetContent(property);
            var input = new PoObjectPicker.Param
            {
                target    = property,
                constrain = di.type,
                setter = o =>
                {
                    property.objectReferenceValue = (Object)o;
                    property.serializedObject.ApplyModifiedProperties();
                },
            };

            if (GUI.Button(position, content, EditorStyles.objectField)) PoObjectPicker.Show(input);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            PrefixLabel(ref position, label);
            ObjectField(ref position, property);
        }
    }
}