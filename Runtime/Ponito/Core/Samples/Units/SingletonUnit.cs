using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Object = UnityEngine.Object;

namespace Ponito.Core.Samples.Units
{
    public class SingletonUnit<T> where T : MonoBehaviour
    {
        private static bool      dontDestroy;
        private static Action<T> onInitialize;
        private static Lazy<T>   instance;

        public SingletonUnit(bool dontDestroy = false, Action<T> onInitialize = null)
        {
            SingletonUnit<T>.dontDestroy  = dontDestroy;
            SingletonUnit<T>.onInitialize = onInitialize;
            instance                      = new Lazy<T>(CreateInstance);
        }

        public T Instance => instance.Value;

        /// <summary>
        ///     Calls when <see cref="Lazy{T}" /> <see cref="instance" /> creates
        /// </summary>
        /// <returns>
        ///     <see cref="MonoBehaviour" /> <see cref="T" />
        /// </returns>
        private static T CreateInstance()
        {
            var isPlaying = Application.isPlaying;

            GameObject gObj;
            if (!isPlaying)
            {
#if UNITY_EDITOR
                gObj = EditorUtility.CreateGameObjectWithHideFlags(
                    string.Empty,
                    HideFlags.DontSave);
#else
                throw new InvalidOperationException();
#endif
            }

            gObj = new GameObject();
            var create = gObj.AddComponent<T>();
            gObj.name = create.GetType().Name;

            if (isPlaying && dontDestroy) Object.DontDestroyOnLoad(create);
            onInitialize?.Invoke(create);

            return create;
        }

        ~SingletonUnit()
        {
            Object.Destroy(instance.Value);
            instance     = null;
            onInitialize = null;
        }
    }
}