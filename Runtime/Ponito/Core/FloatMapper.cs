using System;

namespace Ponito.Core
{
    /// <summary>
    ///     Maps float from input range <see cref="from" /> to output range <see cref="to" />
    /// </summary>
    /// <seealso cref="LinearMap" />
    /// <seealso cref="FloatRange" />
    [Serializable]
    public struct FloatMapper
    {
        /// <summary>
        ///     input range
        /// </summary>
        public FloatRange from;

        /// <summary>
        ///     output range
        /// </summary>
        public FloatRange to;

        /// <summary>
        ///     Linearly maps the <see cref="input"/>
        /// </summary>
        /// <param name="input">input value</param>
        /// <returns>mapped result</returns>
        public float LinearMap(float input)
        {
            return (input - from.min) * (to.Length / from.Length);
        }
    }
}