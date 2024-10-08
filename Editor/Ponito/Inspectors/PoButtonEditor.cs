using Ponito.Core.Samples.Settings;
using Ponito.Core.Samples.UI;
using UnityEditor;
using UnityEngine;

namespace Ponito.Inspectors
{
    [CustomEditor(typeof(PoButton))]
    public class PoButtonEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            {
                var prop      = serializedObject.FindProperty("isInteractable");
                var isWarning = !prop.boolValue;
                if (isWarning) EditorGUILayout.HelpBox("Button is not interactable", MessageType.Warning);
                EditorGUILayout.PropertyField(prop);
            }
            {
                var prop = serializedObject.FindProperty("image");
                EditorGUILayout.PropertyField(prop);
            }
            {
                var prop = serializedObject.FindProperty("animationType");
                EditorGUILayout.PropertyField(prop);
            }
            {
                var settings = PoAudioSettings.Singleton;
                var keys     = settings.uiBinds.Keys;
                var prop     = serializedObject.FindProperty("onClickCue");
                var position = EditorGUILayout.GetControlRect();
                var pos      = position.position;
                {
                    var size = new Vector2(EditorGUIUtility.labelWidth, position.height);
                    var rect = new Rect(pos, size);
                    EditorGUI.LabelField(rect, prop.displayName);
                    pos.x += size.x + 2;
                }
                {
                    var width = position.width - EditorGUIUtility.labelWidth - 20 - 2 - 2;
                    var size  = new Vector2(width, position.height);
                    var rect  = new Rect(pos, size);
                    EditorGUI.TextField(rect, prop.stringValue);
                    pos.x += size.x + 2;
                }
                {
                    var size = new Vector2(20, position.height);
                    var rect = new Rect(pos, size);
                    if (EditorGUI.DropdownButton(rect, GUIContent.none, FocusType.Keyboard))
                    {
                        var menu = new GenericMenu();
                        foreach (var key in keys)
                        {
                            menu.AddItem(new GUIContent(key), false, () => prop.stringValue = key);
                        }
                        menu.ShowAsContext();
                    }
                }
            }
            {
                var prop = serializedObject.FindProperty("onClick");
                EditorGUILayout.PropertyField(prop);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}