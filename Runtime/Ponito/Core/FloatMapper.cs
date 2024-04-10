using System;

namespace Ponito.Core
{
    /// <summary>
    ///     Maps float from input range <see cref="from" /> to output range <see cref="to" />
    /// </summary>
    /// <seealso cref="LinearMap" />
    /// <seealso cref="FloatRange" />
    [Serializable]
    public readonly struct FloatMapper
    {
        /// <summary>
        ///     input range
        /// </summary>
        public readonly FloatRange from;

        /// <summary>
        ///     output range
        /// </summary>
        public readonly FloatRange to;

        /// <summary>
        ///     Constructs a <see cref="FloatMapper" /> by <see cref="FloatRange" />s
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public FloatMapper(FloatRange a, FloatRange b)
        {
            (from, to) = (a, b);
        }

        /// <summary>
        ///     Linearly maps the <see cref="input" />
        /// </summary>
        /// <param name="input">input value</param>
        /// <returns>mapped result</returns>
        public float LinearMap(float input)
        {
            return (input - from.min) * (to.Length / from.Length);
        }
    }
}