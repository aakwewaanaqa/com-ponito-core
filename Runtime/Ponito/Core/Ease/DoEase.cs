using System;
using System.Collections;
using UnityEngine;

namespace Ponito.Core.Ease
{
    /// <summary>
    ///     Provides ease functions of <see cref="UniTask" /> for interpolating values
    ///     and performing easing animations.
    /// </summary>
    /// <seealso cref="Create{T}" />
    public static class DoEase
    {
        /// <summary>
        ///     Eases to a value using the specified ease type.
        /// </summary>
        /// <param name="start">start value of <see cref="easeType" /></param>
        /// <param name="end">end value of <see cref="easeType" /></param>
        /// <param name="setter">the value setter</param>
        /// <param name="duration">the ease duration</param>
        /// <param name="easeType">the type of the ease</param>
        /// <typeparam name="T">the value <see cref="Type" /></typeparam>
        public static Easable Create<T>(
            T start,
            T end,
            Setter<T> setter,
            float duration,
            EaseType easeType = EaseType.InSine)
        {
            Easable easable = start switch
            {
                float s1 when end is float e1 && setter is Setter<float> set1 =>
                    Easer<T>.Float(s1, e1, set1, duration, easeType),
                Vector2 s2 when end is Vector2 e2 && setter is Setter<Vector2> set2 =>
                    Easer<T>.Vector2(s2, e2, set2, duration, easeType),
                Vector3 s3 when end is Vector3 e3 && setter is Setter<Vector3> set3 =>
                    Easer<T>.Vector3(s3, e3, set3, duration, easeType),
                Quaternion s4 when end is Quaternion e4 && setter is Setter<Quaternion> set4 =>
                    Easer<T>.Quaternion(s4, e4, set4, duration, easeType),
                _ => throw new ArgumentException($"Unsupported type {typeof(T).Name} for DoEase.Create")
            };
            EasableManager.Instance.Run(easable);
            return easable;
        }
    }
}