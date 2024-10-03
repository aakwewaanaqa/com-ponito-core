using UnityEditor;
using UnityEngine;

namespace Ponito
{
    public partial class PoKeyboardEditorWindow
    {
        private static bool IsKeyCode(KeyCode code)
        {
            return Event.current.keyCode == code;
        }

        private static bool IsKey()
        {
            var current = Event.current;
            return current.isKey && current.type == EventType.KeyDown;
        }

        private static bool IsAlt()
        {
            return Event.current.alt;
        }

        private static void HandleKeyboardEvent(ref bool repaint)
        {
            if (IsKey() && IsKeyCode(KeyCode.LeftArrow) && IsAlt())
            {
                targetIndex--;
                RefreshTarget();
                repaint = true;
            }

            if (IsKey() && IsKeyCode(KeyCode.RightArrow) && IsAlt())
            {
                targetIndex++;
                RefreshTarget();
                repaint = true;
            }

            if (IsKey() && IsKeyCode(KeyCode.Tab)) { EditorGUIUtility.editingTextField = false; }
        }
    }
}