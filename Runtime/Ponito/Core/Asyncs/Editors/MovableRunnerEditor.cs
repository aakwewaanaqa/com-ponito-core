using UnityEditor;
using UnityEngine;

namespace Ponito.Core.Asyncs.Editors
{
    [CustomEditor(typeof(MovableRunner))]
    public class MovableRunnerEditor : Editor
    {
        private MovableRunner runner;

        private void GetRect(out Rect label, out Rect field)
        {
            var r          = EditorGUILayout.GetControlRect();
            var labelWidth = EditorGUIUtility.labelWidth;
            label = new Rect(r.x, r.y, labelWidth, r.height);
            field = new Rect(r.x + labelWidth, r.y, r.width - labelWidth, r.height);
        }
        
        public override void OnInspectorGUI()
        {
            runner = (MovableRunner) target;
            foreach (var item in runner.Items)
            {
                if (item == null) continue;
                
                GetRect(out var label, out var field);
                EditorGUI.LabelField(label, item.Name);
                if (item.Progress > 0f) EditorGUI.ProgressBar(field, item.Progress, item.Progress.ToString("P"));
            }
            
            if (Application.isPlaying) Repaint();
        }        
    }
}