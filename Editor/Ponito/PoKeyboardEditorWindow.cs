using System;
using System.Linq;
using System.Reflection;
using Codice.Client.Common.WebApi;
using Ponito.Core.Extensions;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ponito
{
    public partial class PoKeyboardEditorWindow : EditorWindow
    {
        private static GameObject activeGameObject => Selection.activeGameObject;

        private static PoKeyboardEditorWindow instance;
        private static Component              target;
        private static int                    targetIndex;
        private static Editor                 targetEditor;
        private static Vector2                scroll;
        private static GUIStyle               style;

        private void OnEnable()
        {
            instance = this;
            style = new GUIStyle
            {
                margin = new RectOffset(15, 5, 0, 0),
            };
            RefreshTarget();
        }

        private void OnGUI()
        {
            var repaint = false;

            HandleKeyboardEvent(ref repaint);

            if (repaint)
            {
                Repaint();
                return;
            }

            if (targetEditor.IsNull()) return;

            targetEditor.DrawHeader();

            EditorGUILayout.BeginVertical(style);

            scroll = EditorGUILayout.BeginScrollView(scroll);
            targetEditor.OnInspectorGUI();
            EditorGUILayout.EndScrollView();

            EditorGUILayout.EndFoldoutHeaderGroup();

            var title = targetEditor.GetInfoString();
            EditorGUILayout.LabelField(title);

            EditorGUILayout.EndVertical();
        }

        private static void RefreshTarget()
        {
            if (activeGameObject.IsNull()) return;

            var comps = activeGameObject.GetComponents<Component>();
            targetIndex = Mathf.Clamp(targetIndex, 0, comps.Length - 1);
            target      = comps[targetIndex];
            if (targetEditor != null) DestroyImmediate(targetEditor);
            targetEditor          = Editor.CreateEditorWithContext(new Object[] { target }, target);
            instance.titleContent = new GUIContent(target ? target.GetType().Name : "Po Editor");
        }

        private void OnDestroy()
        {
            if (ReferenceEquals(instance, this)) instance = null;
        }
    }
}