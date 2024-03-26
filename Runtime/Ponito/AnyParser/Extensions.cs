using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Ponito.AnyParser
{
    public static class Extensions
    {
        public static Parser<char> Char(char c) => input => input.Match(c);

        public static Parser<char> Chars(params char[] chars)
        {
            return chars.Aggregate<char, Parser<char>>(
                seed: null, (accumulated, c) => accumulated is null ? Char(c) : accumulated.Or(Char(c)));
        }

        public static Parser<T> Or<T>(this Parser<T> first, Parser<T> second)
        {
            return input =>
            {
                var fr = first(input);
                return fr ? fr : second(input);
            };
        }

        public static Parser<U> Then<T, U>(this Parser<T> first, Caster<T, Parser<U>> createSecond)
        {
            return input =>
            {
                var fr = first(input);
                return fr ? createSecond(fr.value)(fr.remainder) : Result<U>.Fail(input);
            };
        }

        public static Parser<U> To<T, U>(this Parser<T> first, Caster<T, U> caster)
        {
            return input =>
            {
                var fr = first(input);
                return fr ? Result<U>.Passed(caster(fr.value), fr.remainder) : Result<U>.Fail(input);
            };
        }
    }
}