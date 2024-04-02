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

    public static partial class Extensions
    {
        public static RectTransform SetMinX(this RectTransform self, float x)
        {
            var min = self.anchorMin;

            min.x          = x;
            self.anchorMin = min;
            return self;
        }

        public static RectTransform SetMinY(this RectTransform self, float y)
        {
            var min = self.anchorMin;

            min.y          = y;
            self.anchorMin = min;
            return self;
        }

        public static RectTransform SetMaxX(this RectTransform self, float x)
        {
            var max = self.anchorMax;

            max.x          = x;
            self.anchorMax = max;
            return self;
        }

        public static RectTransform SetMaxY(this RectTransform self, float y)
        {
            var max = self.anchorMax;

            max.y          = y;
            self.anchorMax = max;
            return self;
        }

        public static RectTransform SetSizeX(this RectTransform self, float x)
        {
            var size = self.sizeDelta;

            size.x         = x;
            self.sizeDelta = size;
            return self;
        }

        public static RectTransform SetSizeY(this RectTransform self, float y)
        {
            var size = self.sizeDelta;

            size.y         = y;
            self.sizeDelta = size;
            return self;
        }

        public static RectTransform SetRight(this RectTransform self, float right)
        {
            var offset = self.offsetMax;

            offset.x       = -right;
            self.offsetMax = offset;
            return self;
        }

        public static RectTransform SetUp(this RectTransform self, float up)
        {
            var offset = self.offsetMax;

            offset.y       = -up;
            self.offsetMax = offset;
            return self;
        }

        public static RectTransform SetLeft(this RectTransform self, float left)
        {
            var offset = self.offsetMin;

            offset.x       = left;
            self.offsetMin = offset;
            return self;
        }

        public static RectTransform SetBottom(this RectTransform self, float bottom)
        {
            var offset = self.offsetMin;

            offset.y       = bottom;
            self.offsetMin = offset;
            return self;
        }

        public static RectTransform SetPosX(this RectTransform self, float x)
        {
            var pos = self.anchoredPosition;

            pos.x                 = x;
            self.anchoredPosition = pos;
            return self;
        }

        public static RectTransform SetPosY(this RectTransform self, float y)
        {
            var pos = self.anchoredPosition;

            pos.y                 = y;
            self.anchoredPosition = pos;
            return self;
        }

        public static RectTransform SetPivotX(this RectTransform self, float x)
        {
            var pivot = self.pivot;
            pivot.x    = x;
            self.pivot = pivot;
            return self;
        }

        public static RectTransform SetPivotY(this RectTransform self, float y)
        {
            var pivot = self.pivot;
            pivot.y    = y;
            self.pivot = pivot;
            return self;
        }
    }
}