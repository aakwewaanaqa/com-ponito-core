using System;
using System.Diagnostics;
using UnityEngine;

namespace Ponito.Core.Extensions
{
    /// <summary>
    ///     This is a class to contain all extension methods.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        ///     Ensures to get or add <see cref="Component"/> <see cref="TComp"/> from <see cref="GameObject"/> <see cref="self"/>
        /// </summary>
        /// <param name="self"><see cref="GameObject"/> itself</param>
        /// <param name="result">the result</param>
        /// <param name="apply"><see cref="Action{T}"/> to passed the <see cref="result"/></param>
        /// <param name="ifNullAdd">if comp is not attached to <see cref="self"/> add one</param>
        /// <typeparam name="TObj">type of <see cref="self"/></typeparam>
        /// <typeparam name="TComp">type of <see cref="Component"/> <see cref="result"/></typeparam>
        /// <returns><see cref="GameObject"/> itself</returns>
        /// <exception cref="NullReferenceException"><see cref="self"/> is null</exception>
        [DebuggerHidden]
        public static TObj EnsureComponent<TObj, TComp>(
            this TObj self,
            out TComp result,
            Action<TComp> apply = null,
            bool ifNullAdd = true) where TComp : Component
        {
            if (self.IsNull()) throw new NullReferenceException(nameof(self));

            result = self switch
            {
                GameObject gameObject   => Ensure(gameObject),
                Component selfComponent => Ensure(selfComponent.gameObject),
                _ => throw new InvalidOperationException(nameof(EnsureComponent)),
            };
            return self;

            TComp Ensure(GameObject gameObject)
            {
                bool has                    = gameObject.TryGetComponent(out TComp comp);
                if (ifNullAdd && !has) comp = gameObject.AddComponent<TComp>();
                apply?.Invoke(comp);
                return comp;
            }
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