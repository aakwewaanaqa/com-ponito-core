using System;
using System.Collections.Generic;
using System.Linq;
using Ponito.Core.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Ponito
{
    public class PoObjectPicker : EditorWindow
    {
        private static Param          param  { get; set; }
        private static PoObjectPicker window { get; set; }

        public static void Show(object input)
        {
            param = input.OfType<Param>();
            if (window.IsObject()) window.Close();
            window = CreateWindow<PoObjectPicker>("Po Object Picker");
        }

        private IEnumerable<object> Find(Type constrain)
        {
            var activeScene = SceneManager.GetActiveScene();
            var objects     = activeScene.GetRootGameObjects();
            return objects.SelectMany(o => o.GetComponentsInChildren(constrain));
        }

        private void CreateGUI()
        {
            Find(param.constrain).ForEach((obj, i) =>
            {
                var icon = EditorGUIUtility.GetIconForObject((Object)obj);
                var element = new VisualElement().Apply(it =>
                {
                    it.focusable           = true;
                    it.style.paddingTop    = 3f;
                    it.style.paddingBottom = 3f;
                    it.style.paddingLeft   = 20f;
                    it.RegisterCallback(new EventCallback<MouseDownEvent>(e =>
                    {
                        param.setter(obj);
                        it.style.backgroundColor = new Color(.2f, .5f, 1f, .4f);
                    }));
                    it.RegisterCallback(new EventCallback<MouseEnterEvent>(e =>
                    {
                        it.style.backgroundColor =
                            new Color(1, 1, 1, .05f);
                    }));
                    it.RegisterCallback(new EventCallback<MouseLeaveEvent>(e =>
                    {
                        it.style.backgroundColor = Color.clear;
                    }));
                    it.Add(new Image{ image = icon });
                    it.Add(new Label(obj.ToString()));
                });
                rootVisualElement.Add(element);
            });
        }

        public class Param
        {
            public object         target;
            public Type           constrain;
            public Action<object> setter;
        }
    }
}