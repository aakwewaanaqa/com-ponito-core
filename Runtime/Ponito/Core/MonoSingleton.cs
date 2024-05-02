using System;
using UnityEngine;

namespace Ponito.Core
{
    /// <summary>
    ///     Basic class for singleton of <see cref="MonoBehaviour" />
    /// </summary>
    /// <typeparam name="T">self</typeparam>
    /// <example>SomeManager : MonoSingleton&lt;SomeManager&gt;</example>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        /// <summary>
        ///     Checks whether it needs <see cref="Initialize" /> or not
        /// </summary>
        protected abstract bool IsInitialized { get; }

        /// <summary>
        ///     Checks whether it needs <see cref="MonoBehaviour.DontDestroyOnLoad" /> or not
        /// </summary>
        protected abstract bool IsDontDestroyOnLoad { get; }

        /// <summary>
        ///     <see cref="Lazy{T}" /> calls <see cref="CreateInstance" /> before accessing value
        /// </summary>
        protected static Lazy<T> instance { get; set; } = new(CreateInstance);

        /// <summary>
        ///     Can be accessed anywhere.
        /// </summary>
        public static T Instance => instance.Value;

        /// <summary>
        ///     Destroys instance also, to prevent memory leak of reference on this
        /// </summary>
        protected void OnDestroy()
        {
            if (ReferenceEquals(this, Instance)) instance = new Lazy<T>(CreateInstance);
        }

        /// <summary>
        ///     Calls when <see cref="Lazy{T}" /> <see cref="instance" /> creates
        /// </summary>
        /// <returns>
        ///     <see cref="MonoBehaviour" /> <see cref="T" />
        /// </returns>
        private static T CreateInstance()
        {
            var gObj     = new GameObject();
            var instance = gObj.AddComponent<T>();

            gObj.name = instance.GetType().Name; 
            if (instance.IsDontDestroyOnLoad) DontDestroyOnLoad(instance);
            if (!instance.IsInitialized) instance.Initialize();

            return instance;
        }

        /// <summary>
        ///     To initialize in <see cref="CreateInstance" />
        /// </summary>
        protected abstract void Initialize();
    }
}