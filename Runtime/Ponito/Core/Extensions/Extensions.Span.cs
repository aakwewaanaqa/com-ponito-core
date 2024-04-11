using System;
using System.Collections.Generic;
using System.Linq;

namespace Ponito.Core.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        ///     Turns <see cref="IEnumerable{T}"/> to <see cref="Span{T}"/>
        /// </summary>
        /// <param name="self"><see cref="T"/>s to turn</param>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="self"/></typeparam>
        /// <returns>the ref struct <see cref="Span{T}"/></returns>
        public static Span<T> AsSpan<T>(this IEnumerable<T> self)
        {
            return new Span<T>(self.ToArray());
        }
    }
}