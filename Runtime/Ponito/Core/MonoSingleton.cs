using System;
using Ponito.Extensions;
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
        ///     Stores the instance of this
        /// </summary>
        protected static T instance;

        /// <summary>
        ///     Use this often to ensure their is always one <see cref="MonoBehaviour"/> <see cref="T"/> 
        /// </summary>
        /// <returns><see cref="MonoBehaviour"/> <see cref="T"/></returns>
        public static T GetInstance()
        {
            if (instance.IsObject()) return instance;

            new GameObject()
               .EnsureComponent(out instance)
               .Rename(instance.GetType().Name);

            return instance;
        }

        /// <summary>
        ///     Be sure to call when overriden
        /// </summary>
        protected virtual void Awake()
        {
            if (instance.IsObject()) Destroy(instance);

            instance = (T)this;

            if (isDontDestroyOnLoad) DontDestroyOnLoad(this);
            if (!isInitialized) Initialize();
        }

        /// <summary>
        ///     Destroys instance also, to prevent memory leak of reference on this
        /// </summary>
        protected void OnDestroy()
        {
            if (ReferenceEquals(instance, this)) instance = null;
        }

        /// <summary>
        ///     To initialize on <see cref="Awake"/> 
        /// </summary>
        protected abstract void Initialize();
    }
}