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
        ///     Ensures to get or add <see cref="Component" /> <see cref="TComp" /> from <see cref="GameObject" />
        ///     <see cref="self" />
        /// </summary>
        /// <param name="self"><see cref="GameObject" /> itself</param>
        /// <param name="result">the result</param>
        /// <param name="apply"><see cref="Action{T}" /> to passed the <see cref="result" /></param>
        /// <param name="ifNullAdd">if comp is not attached to <see cref="self" /> add one</param>
        /// <typeparam name="TObj">type of <see cref="self" /></typeparam>
        /// <typeparam name="TComp">type of <see cref="Component" /> <see cref="result" /></typeparam>
        /// <returns><see cref="GameObject" /> itself</returns>
        /// <exception cref="NullReferenceException"><see cref="self" /> is null</exception>
        [DebuggerHidden]
        public static TObj EnsureComponent<TObj, TComp>(
            this TObj self,
            out TComp result,
            Action<TComp> apply = null,
            bool ifNullAdd = true) where TComp : Component
        {
            if (self.IsNull()) throw new ArgumentNullException(nameof(self));
            result = self.Grab().Ensure<GameObject, TComp>(ifNullAdd);
            apply?.Invoke(result);
            return self;
        }

        /// <summary>
        ///     Ensures there is <see cref="Component" /> <see cref="TComp" /> on the <see cref="self" />
        /// </summary>
        /// <param name="self"><see cref="object" /> to chain</param>
        /// <param name="ifNullAdd">if does't exist add one</param>
        /// <typeparam name="TObj"><see cref="self" />'s <see cref="Type" /></typeparam>
        /// <typeparam name="TComp">return's <see cref="Type" /></typeparam>
        /// <returns></returns>
        private static TComp Ensure<TObj, TComp>(this TObj self, bool ifNullAdd) where TComp : Component
        {
            var grab = self.Grab();
            var has  = grab.TryGetComponent(out TComp comp);
            if (!has && ifNullAdd) return grab.AddComponent<TComp>();
            return comp;
        }

        /// <summary>
        ///     Makes sure any <see cref="T" /> returns <see cref="GameObject" />
        /// </summary>
        /// <param name="self"><see cref="object" /> to chain</param>
        /// <typeparam name="T"><see cref="Type" /> of <see cref="self" /></typeparam>
        /// <returns><see cref="self" />'s <see cref="GameObject" /></returns>
        /// <exception cref="InvalidOperationException">not <see cref="GameObject" /> or <see cref="Component" /></exception>
        private static GameObject Grab<T>(this T self)
        {
            return self switch
            {
                GameObject gameObject => gameObject,
                Component component   => component.gameObject,
                _                     => throw new InvalidOperationException(nameof(Grab))
            };
        }

        /// <summary>
        ///     Sets the parent of the <see cref="GameObject" /> <see cref="self" />
        /// </summary>
        /// <param name="self"><see cref="T" /> itself</param>
        /// <param name="parent">the parent to bind with</param>
        /// <param name="resetTransform">to reset local position rotation and scale?</param>
        /// <returns><see cref="GameObject" /> itself</returns>
        /// <exception cref="NullReferenceException"><see cref="self" /> is null</exception>
        [DebuggerHidden]
        public static T SetParent<T>(this T self, Transform parent, bool resetTransform = false)
        {
            if (self.IsNull()) throw new ArgumentNullException(nameof(self));

            var transform = self.Grab().transform;
            transform.parent = parent;

            if (!resetTransform) return self;

            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale    = Vector3.one;
            return self;
        }

        /// <summary>
        ///     Renames the <see cref="GameObject" /> <see cref="self" />
        /// </summary>
        /// <param name="self"><see cref="GameObject" /> itself</param>
        /// <param name="name">the given new name</param>
        /// <returns><see cref="GameObject" /> itself</returns>
        /// <exception cref="NullReferenceException"><see cref="self" /> is null</exception>
        [DebuggerHidden]
        public static T Rename<T>(this T self, string name)
        {
            if (self.IsNull()) throw new ArgumentNullException(nameof(self));

            self.Grab().name = name;
            return self;
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Checks whether <see cref="object" /> is null or not, also knows a destroyed one is null too.
        /// </summary>
        /// <param name="self"><see cref="object" /> to check</param>
        /// <returns>true is null</returns>
        [DebuggerHidden]
        public static bool IsNull(this object self)
        {
            return self is null || self.Equals(null);
        }

        /// <summary>
        ///     Inverted version of <see cref="IsNull" />
        /// </summary>
        /// <param name="self"><see cref="object" /> to check</param>
        /// <returns>true is not null</returns>
        [DebuggerHidden]
        public static bool IsObject(this object self)
        {
            return self is not null && !self.Equals(null);
        }

        /// <summary>
        ///     Chains <see cref="Action{T}" />
        /// </summary>
        /// <param name="self">to check and apply</param>
        /// <param name="apply">the apply <see cref="Action{T}" /></param>
        /// <param name="stopNull">don't invoke when <see cref="self"/> is null</param>
        /// <typeparam name="T"><see cref="Type" /> of <see cref="self" /></typeparam>
        /// <returns>
        ///     <see cref="self" />
        /// </returns>
        [DebuggerHidden]
        public static T Apply<T>(this T self, Action<T> apply, bool stopNull = true)
        {
            if (stopNull && self.IsNull()) return self;
            
            apply?.Invoke(self);
            return self;
        }

        /// <summary>
        ///     Same as <see cref="object.ReferenceEquals" />
        /// </summary>
        [DebuggerHidden]
        public static bool SameReference(this object self, object target)
        {
            return ReferenceEquals(self, target);
        }

        /// <summary>
        ///     Tries to cast <see cref="self" /> to <see cref="T" />
        /// </summary>
        /// <param name="self">the cast source</param>
        /// <param name="result">the cast result</param>
        /// <typeparam name="T">type of <see cref="result" /></typeparam>
        [DebuggerHidden]
        public static void OfType<T>(this object self, out T result)
        {
            if (self is T t) result = t;
            result = (T)self;
        }

        /// <summary>
        ///     Tries to cast <see cref="self" /> to <see cref="T" />
        /// </summary>
        /// <param name="self">the cast source</param>
        /// <param name="result">the cast result</param>
        /// <typeparam name="T">type of <see cref="result" /></typeparam>
        /// <returns>passed or failed</returns>
        [DebuggerHidden]
        public static bool TryCast<T>(this object self, out T result)
        {
            try
            {
                self.OfType(out result);
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }
    }
}