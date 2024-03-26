using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ponito.Core.Extensions
{
    public static partial class Extensions
    {
        public static IEnumerable<MemberInfo> GetFields(
            this Type type,
            string name = default,
            bool isPublic = default,
            bool isStatic = default)
        {
            var binding = isPublic switch
            {
                true when isStatic  => BindingFlags.Public    | BindingFlags.Static,
                true                => BindingFlags.Public    | BindingFlags.Instance,
                false when isStatic => BindingFlags.NonPublic | BindingFlags.Static,
                false               => BindingFlags.NonPublic | BindingFlags.Instance,
            };
            var fields = type.GetFields(binding);
            return string.IsNullOrEmpty(name) ? fields : fields.Where(f => f.Name == name);
        }

        public static IEnumerable<MemberInfo> WithAttributes<T>(this IEnumerable<MemberInfo> infos) where T : Attribute
        {
            return infos.Where(i => i.GetCustomAttribute<T>() is object);
        }
    }
}