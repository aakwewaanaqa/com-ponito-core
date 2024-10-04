using UnityEditor;
using UnityEngine.SceneManagement;

namespace Ponito
{
    public static class PoShortcuts
    {
        [MenuItem("Tools/Shortcuts/First In Hierarchy &1")]
        public static void FirstInHierarchy()
        {
            EditorApplication.ExecuteMenuItem("Window/General/Hierarchy");
            var scene                               = SceneManager.GetActiveScene();
            var active                              = Selection.activeGameObject;
            if (!active) Selection.activeGameObject = scene.GetRootGameObjects()[0];
        }

        [MenuItem("Tools/Shortcuts/Po Editor &2")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow<PoKeyboardEditorWindow>("Po Editor");
        }
    }
}