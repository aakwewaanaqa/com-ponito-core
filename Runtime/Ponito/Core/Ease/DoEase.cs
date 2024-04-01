using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Ponito.Core.Ease
{
    /// <summary>
    ///     Provides ease functions of <see cref="UniTask"/> for interpolating values and performing easing animations.
    /// </summary>
    /// <seealso cref="To{T}"/>
    public static class DoEase
    {
        /// <summary>
        ///     Delegate for interpolation value <see cref="T"/> 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public delegate T Lerp<T>(T a, T b, float t);

        /// <inheritdoc cref="Mathf.Lerp"/>
        public static Lerp<float> LerpOfFloat => Mathf.Lerp;

        /// <inheritdoc cref="Vector2.Lerp"/>
        public static Lerp<Vector2> LerpOfVector2 => Vector2.Lerp;

        /// <inheritdoc cref="Vector3.Lerp"/>
        public static Lerp<Vector3> LerpOfVector3 => Vector3.Lerp;

        /// <inheritdoc cref="Quaternion.Lerp"/>
        public static Lerp<Quaternion> LerpOfQuaternion => Quaternion.Lerp;

        /// <summary>
        ///     Eases to and manually setting up a <see cref="Lerp{T}"/>
        /// </summary>
        /// <param name="start">start value of <see cref="easeType"/></param>
        /// <param name="end">end value of <see cref="easeType"/></param>
        /// <param name="setter">the value setter</param>
        /// <param name="lerp">the interpolator</param>
        /// <param name="duration">the ease duration</param>
        /// <param name="easeType">the type of the ease</param>
        /// <typeparam name="T">the value <see cref="Type"/></typeparam>
        public static async UniTask To<T>(
            T start,
            T end,
            Setter<T> setter,
            Lerp<T> lerp,
            float duration,
            EaseType easeType = EaseType.InSine)
        {
            var time = 0f;
            var ease = EasingEquations.GetFunction(easeType);
            while (time < duration)
            {
                var p = ease(time / duration);
                var v = lerp(start, end, p);
                setter(v);

                time += Time.deltaTime;
                await UniTask.Yield();
            }

            setter(end);
        }

        public static async UniTask To(
            float start,
            float end,
            Setter<float> setter,
            float duration,
            EaseType easeType = EaseType.InSine)
        {
            await To(start, end, setter, LerpOfFloat, duration);
        }

        public static async UniTask To(
            Vector2 start,
            Vector2 end,
            Setter<Vector2> setter,
            float duration,
            EaseType easeType = EaseType.InSine)
        {
            await To(start, end, setter, LerpOfVector2, duration);
        }

        public static async UniTask To(
            Vector3 start,
            Vector3 end,
            Setter<Vector3> setter,
            float duration,
            EaseType easeType = EaseType.InSine)
        {
            await To(start, end, setter, LerpOfVector3, duration);
        }

        public static async UniTask To(
            Quaternion start,
            Quaternion end,
            Setter<Quaternion> setter,
            float duration,
            EaseType easeType = EaseType.InSine)
        {
            await To(start, end, setter, LerpOfQuaternion, duration);
        }
    }
}