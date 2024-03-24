using UnityEngine;

namespace Ponito.Ease
{
    /// <summary>
    ///     Contains all ease functions.
    /// </summary>
    /// <seealso cref="InSine"/>
    /// <seealso cref="OutSine"/>
    /// <seealso cref="InOutSine"/>
    public static class EasingEquations
    {
        /// <summary>
        ///     f(0) = 0, f(1) = 1
        /// </summary>
        /// <param name="t">0 &lt; <see cref="t"/> &lt; 1</param>
        /// <returns>0 &lt; y &lt; 1</returns>
        public static float InSine(float t) => 1f - Mathf.Cos(t * Mathf.PI / 2f);

        /// <inheritdoc cref="InSine"/> by sine wave
        public static float OutSine(float t) => Mathf.Sin(t * Mathf.PI / 2f);

        /// <inheritdoc cref="InSine"/> by half cosine half sine
        public static float InOutSine(float t) => -(Mathf.Cos(Mathf.PI * t) - 1f) / 2f;

        /// <inheritdoc cref="InSine"/> by t^2
        public static float InQuad(float t) => t * t;

        /// <inheritdoc cref="InSine"/> by 1 - (1 - t)^2
        public static float OutQuad(float t) => 1f - (1f - t) * (1f - t);

        /// <inheritdoc cref="InSine"/>
        public static float InOutQuad(float t)
        {
            return t < 0.5f ? 2f * t * t : 1f - (-2f * t + 2f) * (-2f * t + 2f) / 2f;
        }

        /// <inheritdoc cref="InSine"/> by t^3
        public static float InCubic(float t) => t * t * t;

        /// <inheritdoc cref="InSine"/> by 1 - (1 - t)^3
        public static float OutCubic(float t) => 1f - (1f - t) * (1f - t) * (1f - t);

        /// <inheritdoc cref="InSine"/>
        public static float InOutCubic(float t)
        {
            return t < 0.5f ? 4f * t * t * t : 1f - (-2f * t + 2f) * (-2f * t + 2f) * (-2f * t + 2f) / 2f;
        }

        /// <inheritdoc cref="InSine"/> by t^4
        public static float InQuart(float t) => t * t * t * t;

        /// <inheritdoc cref="InSine"/> by 1f - (1 - t)^4
        public static float OutQuart(float t) => 1 - (1f - t) * (1f - t) * (1f - t) * (1f - t);

        /// <inheritdoc cref="InSine"/>
        public static float InOutQuart(float t)
        {
            return t < 0.5f
                       ? 16f * t * t * t * t
                       : 1f - (-2f * t + 2f) * (-2f * t + 2f) * (-2f * t + 2f) * (-2f * t + 2f) / 2f;
        }

        /// <inheritdoc cref="InSine"/> by 2^(10t - 10)
        public static float InExpo(float t)
        {
            return t == 0f ? 0f : Mathf.Pow(2f, 10f * t - 10f);
        }

        /// <inheritdoc cref="InSine"/> by 1 - 2^(10t - 10)
        public static float OutExpo(float t)
        {
            return t == 1f ? 1f : 1f - Mathf.Pow(2f, 10f * t - 10f);
        }

        /// <inheritdoc cref="InSine"/>
        public static float InOutExpo(float t)
        {
            return t switch
            {
                0f     => 0f,
                1f     => 1f,
                < 0.5f => Mathf.Pow(2f, 20f * t - 10f)         / 2f,
                _      => (2f - Mathf.Pow(2f, -20f * t + 10f)) / 2f
            };
        }

        /// <inheritdoc cref="InSine"/> by 1 - sqrt(1 - t^2)
        public static float InCirc(float t) => 1f - Mathf.Sqrt(1 - Mathf.Pow(t, 2));

        /// <inheritdoc cref="InSine"/> by sqrt(1 - (t - 1)^2)
        public static float OutCirc(float t) => Mathf.Sqrt(1f - Mathf.Pow(t - 1f, 2));

        /// <inheritdoc cref="InSine"/>
        public static float InOutCirc(float t)
        {
            return t < 0.5f
                       ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * t,      2)))     / 2
                       : (Mathf.Sqrt(1     - Mathf.Pow(-2 * t + 2, 2)) + 1) / 2;
        }

        /// <inheritdoc cref="InSine"/>
        public static float InElastic(float t)
        {
            const float c4 = (2f * Mathf.PI) / 3f;
            return t switch
            {
                0f => 0f,
                1f => 1f,
                _  => -Mathf.Pow(2f, 10f * t - 10f) * Mathf.Sin((t * 10f - 10.75f) * c4)
            };
        }

        /// <inheritdoc cref="InSine"/>
        public static float OutElastic(float t)
        {
            const float c4 = (2f * Mathf.PI) / 3f;
            return t switch
            {
                0f => 0f,
                1f => 1f,
                _  => Mathf.Pow(2f, -10f * t) * Mathf.Sin((t * 10f - 0.75f) * c4) + 1f
            };
        }

        /// <inheritdoc cref="InSine"/>
        public static float InOutElastic(float t)
        {
            const float c5 = (2f * Mathf.PI) / 4.5f;
            return t switch
            {
                0f     => 0f,
                1f     => 1f,
                < 0.5f => -(Mathf.Pow(2f, 20f  * t - 10f) * Mathf.Sin((20f * t - 11.125f) * c5)) / 2f,
                _      => (Mathf.Pow(2f,  -20f * t + 10f) * Mathf.Sin((20f * t - 11.125f) * c5)) / 2f + 1f
            };
        }

        /// <inheritdoc cref="InSine"/>
        public static float InBounce(float t) => 1f - OutBounce(t);

        /// <inheritdoc cref="InSine"/>
        public static float OutBounce(float t)
        {
            const float n1 = 7.5625f;
            const float d1 = 2.75f;
            return t switch
            {
                < 1f   / d1 => n1 * t * t,
                < 2f   / d1 => n1 * (t -= 1.5f   / d1) * t + 0.75f,
                < 2.5f / d1 => n1 * (t -= 2.25f  / d1) * t + 0.9375f,
                _           => n1 * (t -= 2.625f / d1) * t + 0.984375f,
            };
        }

        /// <inheritdoc cref="InSine"/>
        public static float InOutBounce(float t)
        {
            return t < 0.5f
                       ? (1f - OutBounce(1f - 2f * t))      / 2f
                       : (1f + OutBounce(2f      * t - 1f)) / 2f;
        }
    }
}