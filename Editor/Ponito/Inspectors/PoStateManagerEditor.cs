using System;
using System.Collections.Generic;
using System.Reflection;
using Ponito.Core.Samples.Managers;
using UnityEditor;
using UnityEngine;
using static Ponito.Core.Samples.Managers.PoStateManager;

namespace Ponito.Inspectors
{
    [CustomEditor(typeof(PoStateManager))]
    public class PoStateManagerEditor : Editor
    {
        private static PoStateManager                comp;
        private static Vector2                       scroll;
        private static Dictionary<IntPtr, StateBind> pairs;

        private const BindingFlags FLAGS = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        private void OnEnable()
        {
            comp = (PoStateManager)target;
            var field = typeof(PoStateManager).GetField("binds", FLAGS);
            pairs = (Dictionary<IntPtr, StateBind>)field.GetValue(comp);
        }

        public override void OnInspectorGUI()
        {
            scroll = EditorGUILayout.BeginScrollView(scroll);
            foreach (var pair in pairs)
            {
                var bind = pair.Value;
                var stateName = ObjectNames.NicifyVariableName(bind.state.GetType().Name);
                EditorGUILayout.BeginFoldoutHeaderGroup(true, stateName);
                foreach (var watcher in bind.watchers)
                {
                    var watcherName = ObjectNames.NicifyVariableName(watcher.Name);
                    EditorGUILayout.LabelField(watcherName);
                }
                EditorGUILayout.EndFoldoutHeaderGroup();
            }

            EditorGUILayout.EndScrollView();
        }
    }
}