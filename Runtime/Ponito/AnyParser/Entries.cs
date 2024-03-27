using System.Collections.Generic;
using System.Linq;

namespace Ponito.AnyParser
{
    public static class Entries
    {
        /// <summary>
        ///     Splits the <see cref="Input"/>
        /// </summary>
        /// <param name="strings">splitters</param>
        /// <returns><see cref="Parser{T}"/></returns>
        public static Parser<IEnumerable<Input>> Split(params string[] strings) => input => input.Split(strings);

        /// <summary>
        ///     Checks first character of the <see cref="Input"/> == <see cref="c"/>
        /// </summary>
        /// <param name="c"><see cref="char"/></param>
        /// <returns><see cref="Parser{T}"/></returns>
        public static Parser<char> Char(char c) => input => input.Match(c);

        /// <summary>
        ///     Checks first character of the <see cref="Input"/> is one of the <see cref="chars"/>
        /// </summary>
        /// <param name="chars"><see cref="char"/></param>
        /// <returns><see cref="Parser{T}"/></returns>
        public static Parser<char> Chars(params char[] chars) =>
            chars.Aggregate<char, Parser<char>>(
                seed: null, (accumulated, c) => accumulated is null ? Char(c) : accumulated.Or(Char(c)));
    }
}