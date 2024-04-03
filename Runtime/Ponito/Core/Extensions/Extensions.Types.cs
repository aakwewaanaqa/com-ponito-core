using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.Profiling.LowLevel;

namespace Ponito.Core.Extensions
{
    public static partial class Extensions
    {
        private static BindingFlags MakeFlags(BoolFlag isPublic, BoolFlag isStatic)
        {
            BindingFlags flag           = BindingFlags.Default;
            if (isPublic == true) flag  |= BindingFlags.Public;
            if (isPublic == false) flag |= BindingFlags.NonPublic;
            if (isStatic == true) flag  |= BindingFlags.Static;
            if (isStatic == false) flag |= BindingFlags.Instance;
            return flag;
        }

        public static IEnumerable<MemberInfo> GetFields(
            this Type type,
            string name = null,
            BoolFlag isPublic = default,
            BoolFlag isStatic = default)
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
            string name,
            object target = null,
            BoolFlag isPublic = default,
            BoolFlag isStatic = default)
        {
            var flags = MakeFlags(isPublic, isStatic);
            var info  = self.GetField(name, flags);
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