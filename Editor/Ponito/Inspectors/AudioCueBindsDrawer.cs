using UnityEditor;
using Ponito.Core.Samples.Audios;
using UnityEngine;

namespace Ponito.Inspectors
{
    [CustomPropertyDrawer(typeof(AudioCueBinds))]
    public class AudioCueBindsDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var cues = property.FindPropertyRelative("cues");
            EditorGUI.PropertyField(position, cues);
            property.isExpanded = cues.isExpanded;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var cues  = property.FindPropertyRelative("cues");
            return EditorGUI.GetPropertyHeight(cues);
        }
    }
}