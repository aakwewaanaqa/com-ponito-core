using System;
using Ponito.Core.Extensions;
using UnityEngine;

namespace Ponito.Core
{
    /// <summary>
    ///     Basic class for singleton of <see cref="MonoBehaviour"/>
    /// </summary>
    /// <typeparam name="T">self</typeparam>
    /// <example>SomeManager : MonoSingleton&lt;SomeManager&gt;</example>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        /// <summary>
        ///     Checks whether it needs <see cref="Initialize"/> or not
        /// </summary>
        protected abstract bool isInitialized { get; }

        /// <summary>
        ///     Checks whether it needs <see cref="MonoBehaviour.DontDestroyOnLoad"/> or not
        /// </summary>
        protected abstract bool isDontDestroyOnLoad { get; }

        /// <summary>
        ///     <see cref="Lazy{T}"/> calls <see cref="CreateInstance"/> before accessing value
        /// </summary>
        protected static Lazy<T> instance { get; set; } = new(CreateInstance);

        /// <summary>
        ///     Can be accessed anywhere.
        /// </summary>
        public static T Instance => instance.Value;

        /// <summary>
        ///     Use this often to ensure their is always one <see cref="MonoBehaviour"/> <see cref="T"/> 
        /// </summary>
        /// <returns><see cref="MonoBehaviour"/> <see cref="T"/></returns>
        private static T CreateInstance()
        {
            new GameObject()
               .EnsureComponent(out T instance)
               .Rename(instance.GetType().Name);

            if (instance.isDontDestroyOnLoad) DontDestroyOnLoad(instance);
            if (!instance.isInitialized) instance.Initialize();
            
            return instance;
        }

        /// <summary>
        ///     Destroys instance also, to prevent memory leak of reference on this
        /// </summary>
        protected void OnDestroy()
        {
            if (this.SameReference(Instance)) instance = new Lazy<T>(CreateInstance);
        }

        /// <summary>
        ///     To initialize in <see cref="CreateInstance"/> 
        /// </summary>
        protected abstract void Initialize();
    }
}