using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.Profiling.LowLevel;

namespace Ponito.Core.Extensions
{
    public static partial class Extensions
    {
        private static BindingFlags MakeFlags(bool isPublic = default,
                                              bool isStatic = default)
        {
            return isPublic switch
            {
                true when isStatic  => BindingFlags.Public    | BindingFlags.Static,
                true                => BindingFlags.Public    | BindingFlags.Instance,
                false when isStatic => BindingFlags.NonPublic | BindingFlags.Static,
                false               => BindingFlags.NonPublic | BindingFlags.Instance,
            };
        }
        
        public static IEnumerable<MemberInfo> GetFields(
            this Type type,
            string name = default,
            bool isPublic = default,
            bool isStatic = default)
        {
            var binding = MakeFlags(isPublic, isStatic);
            var fields  = type.GetFields(binding);
            return string.IsNullOrEmpty(name) ? fields : fields.Where(f => f.Name == name);
        }

        public static IEnumerable<MemberInfo> WithAttributes<T>(this IEnumerable<MemberInfo> infos) where T : Attribute
        {
            return infos.Where(i => i.GetCustomAttribute<T>() is object);
        }

        public static Type Field<T>(
            this Type self,
            out T result,
            object target,
            string name,
            bool isPublic = default,
            bool isStatic = default)
        {
            var binding = MakeFlags(isPublic, isStatic);
            var info = self.GetField(name, binding);
            result = (T)info?.GetValue(target);
            return self;
        }

        public static Type Attribute<T>(this Type self, out T result) where T : Attribute
        {
            result = (T)self.GetCustomAttribute(typeof(T));
            return self;
        }
    }
}