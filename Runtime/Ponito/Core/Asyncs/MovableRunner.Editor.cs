#if UNITY_EDITOR
using System.Collections.Generic;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.Asyncs.Tasks;
using UnityEditor;
using UnityEngine;

namespace Ponito.Core.Asyncs
{
    public partial class MovableRunner
    {
        [CustomEditor(typeof(MovableRunner))]
        private class Editor : UnityEditor.Editor
        {
            private MovableRunner runner;

            private Movable[] movables => runner.movables;

            private void OnEnable()
            {
                runner = target as MovableRunner;
            }

            public override void OnInspectorGUI()
            {
                foreach (var movable in movables)
                {
                    var sourceName = movable?.GetType().Name;

                    var cr   = EditorGUILayout.GetControlRect();
                    var rect = cr;
                    {
                        var pos  = rect.position;
                        var size = new Vector2(rect.width, rect.height);
                        rect = new Rect(pos, size);
                        EditorGUI.TextField(rect, sourceName);
                        rect.x += size.x;
                    }

                    // {
                    //     var pos  = rect.position;
                    //     var size = new Vector2(50, rect.height);
                    //     rect = new Rect(pos, size);
                    //     EditorGUI.LabelField(rect, nameof(builder), builder);
                    //     rect.x += size.x;
                    // }
                }

                Repaint();
            }
        }
    }
}
#endif