using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ponito.Core
{
    /// <summary>
    ///     uses <see cref="GetRandom" /> to get random result, or gives <see cref="FloatMapper" /> as parameter
    /// </summary>
    /// <seealso cref="GetRandom" />
    /// <seealso cref="GetClamp" />
    /// <seealso cref="FloatMapper" />
    [Serializable]
    public readonly partial struct FloatRange
    {
        /// <summary>
        ///     maximum
        /// </summary>
        public readonly float max;

        /// <summary>
        ///     minimum
        /// </summary>
        public readonly float min;

        /// <summary>
        ///     constructs a <see cref="FloatRange" />
        /// </summary>
        /// <param name="min">minimum <see cref="min" /></param>
        /// <param name="max">maximum <see cref="max" /></param>
        public FloatRange(float min, float max)
        {
            if (min > max)
            {
                this.min = max;
                this.max = min;
            }
            else
            {
                this.min = min;
                this.max = max;
            }
        }

        /// <summary>
        ///     magnitude of the <see cref="FloatRange" />
        /// </summary>
        public float Length => max - min;

        /// <summary>
        ///     a range always get 1
        /// </summary>
        public static FloatRange One => new(1, 1);

        /// <summary>
        ///     gets a random value
        /// </summary>
        /// <returns>the random number</returns>
        public float GetRandom()
        {
            return Random.Range(min, max);
        }

        /// <summary>
        ///     clamps the input in the <see cref="FloatRange" />
        /// </summary>
        /// <param name="input">the source value</param>
        /// <returns>the clamped result</returns>
        public float GetClamp(float input)
        {
            return Mathf.Clamp(input, min, max);
        }
    }
}