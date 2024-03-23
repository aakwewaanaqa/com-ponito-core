using System;
using System.Diagnostics;
using UnityEngine;

namespace Ponito.Extensions
{
    /// <summary>
    ///     This is a class to contain all extension methods.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        ///     Ensures to get or add <see cref="Component"/> <see cref="T"/> from <see cref="GameObject"/> <see cref="self"/>
        /// </summary>
        /// <param name="self"><see cref="GameObject"/> itself</param>
        /// <param name="comp">the result</param>
        /// <typeparam name="T">type of component</typeparam>
        /// <returns><see cref="GameObject"/> itself</returns>
        /// <exception cref="NullReferenceException"><see cref="self"/> is null</exception>
        [DebuggerHidden]
        public static GameObject EnsureComponent<T>(this GameObject self, out T comp) where T : Component
        {
            if (self.IsNull()) throw new NullReferenceException(nameof(self));

            if (self.TryGetComponent(out comp)) return self;

            comp = self.AddComponent<T>();
            return self;
        }

        /// <summary>
        ///     Sets the parent of the <see cref="GameObject"/> <see cref="self"/>
        /// </summary>
        /// <param name="self"><see cref="GameObject"/> itself</param>
        /// <param name="parent">the parent to bind with</param>
        /// <param name="resetTransform">to reset local position rotation and scale?</param>
        /// <returns><see cref="GameObject"/> itself</returns>
        /// <exception cref="NullReferenceException"><see cref="self"/> is null</exception>
        [DebuggerHidden]
        public static GameObject SetParent(this GameObject self, Transform parent, bool resetTransform = false)
        {
            if (self.IsNull()) throw new NullReferenceException(nameof(self));

            var transform = self.transform;
            transform.parent = parent;

            if (!resetTransform) return self;

            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale    = Vector3.one;
            return self;
        }

        /// <summary>
        ///     Renames the <see cref="GameObject"/> <see cref="self"/>
        /// </summary>
        /// <param name="self"><see cref="GameObject"/> itself</param>
        /// <param name="name">the given new name</param>
        /// <returns><see cref="GameObject"/> itself</returns>
        /// <exception cref="NullReferenceException"><see cref="self"/> is null</exception>
        [DebuggerHidden]
        public static GameObject Rename(this GameObject self, string name)
        {
            if (self.IsNull()) throw new NullReferenceException(nameof(self));

            self.name = name;
            return self;
        }
    }
}