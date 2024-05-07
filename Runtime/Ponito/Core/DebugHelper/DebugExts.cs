using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Debug = UnityEngine.Debug;

namespace Ponito.Core.DebugHelper
{
    public static class DebugExts
    {
        private const string FUNCTION_COLOR = "#DFCD7E";
        private const string STRUCT_COLOR   = "#B6FF91";
        private const string CLASS_COLOR    = "#07FFD4";
        private const string KEYWORD_COLOR  = "#6B96F8";

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string Colorize(this string msg, string color)
        {
            return $"<color={color}>{msg}</color>";
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Log(this object obj)
        {
            if (DebugHelperScope.IsBlock) return;
            Debug.Log(obj);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void F(this Type type, string function)
        {
            var typeColor = type.IsValueType ? STRUCT_COLOR : CLASS_COLOR;
            object o = $"{type.Name.Colorize(typeColor)}" +
                       $".{function.Colorize(FUNCTION_COLOR)}()";
            o.Log();
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void F(this Type type, string function, params object[] args)
        {
            var typeColor = type.IsValueType ? STRUCT_COLOR : CLASS_COLOR;
            var arg       = args.Aggregate((a, b) => $"{a}, {b}");
            var o = $"{type.Name.Colorize(typeColor)}" +
                    $".{function.Colorize(FUNCTION_COLOR)}({arg})";
            o.Log();
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Get(this Type type, string function)
        {
            var typeColor = type.IsValueType ? STRUCT_COLOR : CLASS_COLOR;
            object o = $"{type.Name.Colorize(typeColor)}" +
                       $".{function.Colorize(FUNCTION_COLOR)} {{ {"get".Colorize(KEYWORD_COLOR)}; }}";
            o.Log();
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Keyword(this string keyWord, object obj)
        {
            var type = obj.GetType();
            object o = $"{keyWord.Colorize(KEYWORD_COLOR)} " +
                       $"{obj}";
            o.Log();
        }
    }
}