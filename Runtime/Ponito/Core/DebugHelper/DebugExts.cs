using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Debug = UnityEngine.Debug;

namespace Ponito.Core.DebugHelper
{
    internal static class DebugColors
    {
        internal const string FUNCTION_COLOR = "#DFCD7E";
        internal const string STRUCT_COLOR   = "#B6FF91";
        internal const string CLASS_COLOR    = "#07FFD4";
        internal const string KEYWORD_COLOR  = "#6B96F8";
        internal const string WARNING_COLOR  = "#FC8C03";
        internal const string STRING_COLOR   = "#E6A409";
    }

    public static class DebugExts
    {
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static string Colorize(this string msg, string color)
        {
            return $"<color={color}>{msg}</color>";
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Log(this object obj)
        {
            if (DebugHelperScope.IsBlock) return;
            Debug.Log(obj);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Warn(this object obj)
        {
            if (DebugHelperScope.IsBlock) return;
            Debug.LogWarning(obj);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void F(this Type type, string function)
        {
            var typeColor = type.IsValueType ? DebugColors.STRUCT_COLOR : DebugColors.CLASS_COLOR;
            object o = $"{type.Name.Colorize(typeColor)}" +
                       $".{function.Colorize(DebugColors.FUNCTION_COLOR)}()";
            o.Log();
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void F(this Type type, string function, params object[] args)
        {
            var count     = type.Name.Length + function.Length + 2;
            var indention = new string(' ', count);
            var typeColor = type.IsValueType ? DebugColors.STRUCT_COLOR : DebugColors.CLASS_COLOR;
            var arg = args
               .Select(a => a.ToString().Colorize(DebugColors.STRING_COLOR))
               .Aggregate((a, b) => $"{a},\n{indention}{b}");
            var o = $"{type.Name.Colorize(typeColor)}" +
                    $".{function.Colorize(DebugColors.FUNCTION_COLOR)}({arg})";
            o.Log();
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void F(this Type type, string function, IEnumerable<object> args)
        {
            var count     = type.Name.Length + function.Length + 2;
            var indention = new string(' ', count);
            var typeColor = type.IsValueType ? DebugColors.STRUCT_COLOR : DebugColors.CLASS_COLOR;
            var arg = args
               .Select(a => a.ToString().Colorize(DebugColors.STRING_COLOR))
               .Aggregate((a, b) => $"{a},\n{indention}{b}");
            var o = $"{type.Name.Colorize(typeColor)}" +
                    $".{function.Colorize(DebugColors.FUNCTION_COLOR)}({arg})";
            o.Log();
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Get(this Type type, string function)
        {
            var typeColor = type.IsValueType ? DebugColors.STRUCT_COLOR : DebugColors.CLASS_COLOR;
            object o = $"{type.Name.Colorize(typeColor)}" +
                       $".{function.Colorize(DebugColors.FUNCTION_COLOR)} {{ {"get".Colorize(DebugColors.KEYWORD_COLOR)}; }}";
            o.Log();
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Keyword(this string keyWord, object obj)
        {
            var type = obj.GetType();
            object o = $"{keyWord.Colorize(DebugColors.KEYWORD_COLOR)} " +
                       $"{obj}";
            o.Log();
        }
    }
}