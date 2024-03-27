using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Codice.CM.Common.Replication;
using Unity.Plastic.Antlr3.Runtime;

namespace Ponito.AnyParser
{
    public static class Extensions
    {
        /// <summary>
        ///     Enumerates <see cref="T"/> with <see cref="second"/>
        /// </summary>
        /// <param name="splitter">previous <see cref="Parser{T}"/></param>
        /// <param name="second">the <see cref="second"/> <see cref="CastParser{T, U}"/> with <see cref="T"/> as input</param>
        /// <typeparam name="T">to type</typeparam>
        /// <returns>the manufactured <see cref="Parser{T}"/></returns>
        public static Parser<IEnumerable<T>> ForEach<T>(
            this Parser<IEnumerable<Input>> splitter,
            Parser<T> second)
        {
            return input =>
            {
                var fr = splitter(input);
                if (!fr) return Result<IEnumerable<T>>.Fail(input);

                var list = new List<T>();
                foreach (var item in fr.value)
                {
                    var ir = second(item);
                    if (ir) list.Add(ir.value);
                }

                return list.Count == 0
                           ? Result<IEnumerable<T>>.Fail(input)
                           : Result<IEnumerable<T>>.Passed(list, fr.remainder);
            };
        }

        /// <summary>
        ///     If <see cref="first"/> fails it goes to <see cref="second"/>
        /// </summary>
        /// <returns>the manufactured <see cref="Parser{T}"/></returns>
        public static Parser<T> Or<T>(this Parser<T> first, Parser<T> second)
        {
            return input =>
            {
                var fr = first(input);
                return fr ? fr : second(input);
            };
        }
        
        /// <summary>
        ///     If <see cref="first"/> passed it goes to <see cref="createSecond"/>
        /// </summary>
        /// <returns>the manufactured <see cref="Parser{T}"/></returns>
        public static Parser<U> Then<T, U>(this Parser<T> first, Passer<T, Parser<U>> createSecond)
        {
            return input =>
            {
                var fr = first(input);
                return fr ? createSecond(fr.value)(fr.remainder) : Result<U>.Fail(input);
            };
        }

        /// <summary>
        ///     Repeats the <see cref="parser"/> many times until it fails
        /// </summary>
        /// <returns>the manufactured <see cref="Parser{T}"/></returns>
        public static Parser<IEnumerable<T>> Repeat<T>(this Parser<T> parser)
        {
            return input =>
            {
                var list   = new List<T>();
                var result = parser(input);
                while (result)
                {
                    list.Add(result.value);
                    input  = result.remainder;
                    result = parser(input);
                }

                return list.Count > 0
                           ? Result<IEnumerable<T>>.Passed(list, result.remainder)
                           : Result<IEnumerable<T>>.Fail(input);
            };
        }

        /// <summary>
        ///     Casts the <see cref="parser"/>'s <see cref="Result{T}"/> with <see cref="Passer{T,U}"/>
        /// </summary>
        /// <returns>the manufactured <see cref="Parser{T}"/></returns>
        public static Parser<U> To<T, U>(this Parser<T> parser, Passer<T, U> passer)
        {
            return input =>
            {
                var result = parser(input);
                return result ? Result<U>.Passed(passer(result.value), result.remainder) : Result<U>.Fail(input);
            };
        }

        /// <summary>
        ///     Casts the <see cref="parser"/>'s <see cref="Result{T}"/> to <see cref="string"/> with <see cref="FlatToString"/>
        /// </summary>
        /// <returns>the manufactured <see cref="Parser{T}"/></returns>
        public static Parser<string> String<T>(this Parser<T> parser)
        {
            return input =>
            {
                var result = parser(input);
                return !result
                           ? Result<string>.Fail(input)
                           : Result<string>.Passed(result.value.FlatToString(), result.remainder);
            };
        }

        /// <summary>
        ///     Collapses <see cref="IEnumerable{T}"/> of <see cref="char"/> or <see cref="string"/>
        ///     or turns any <see cref="object"/> to <see cref="string"/> by <see cref="object.ToString"/>
        /// </summary>
        /// <param name="obj">any input</param>
        /// <returns>flatten <see cref="string"/></returns>
        public static string FlatToString(this object obj)
        {
            return obj switch
            {
                IEnumerable<char> chars     => new string(chars.ToArray()),
                IEnumerable<string> strings => strings.Aggregate((a, b) => a + b),
                _                           => obj.ToString(),
            };
        }
    }
}