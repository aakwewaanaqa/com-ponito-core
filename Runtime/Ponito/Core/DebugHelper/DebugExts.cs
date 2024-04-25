using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Ponito.Core.DebugHelper
{
    public static class DebugExts
    {
        private const string FUNCTION_COLOR = "#DFCD7E";
        private const string STRUCT_COLOR   = "#B6FF91";
        private const string CLASS_COLOR    = "#07FFD4";
        private const string KEYWORD_COLOR  = "#6B96F8";

        private static string Colorize(this string msg, string color)
        {
            return $"<color={color}>{msg}</color>";
        }
        
        [DebuggerHidden]
        public static void F(this Type type, string function)
        {
            var    typeColor = type.IsValueType ? STRUCT_COLOR : CLASS_COLOR;
            object o         = $"{type.Name.Colorize(typeColor)}" +
                               $".{function.Colorize(FUNCTION_COLOR)}()";
            Debug.Log(o);
        }

        [DebuggerHidden]
        public static void Keyword(this string keyWord, object obj)
        {
            var type = obj.GetType();
            object o = $"{keyWord.Colorize(KEYWORD_COLOR)} " +
                       $"{obj}";
            Debug.Log(o);
        }
    }
}