using System;
using System.Text;
using System.Threading.Tasks;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.Asyncs.Tasks;
using Ponito.Core.Asyncs.Tasks.Movables;
using Ponito.Core.DebugHelper;

namespace Ponito.Core.Asyncs.Extensions
{
    public static partial class Exts
    {
        public static Movable<T> AsPoTask<T>(this ValueTask<T> vt)
        {
            return new ValueTaskAwait<T>(vt);
        }
        
        public static Movable AsPoTask<T>(this ValueTask vt)
        {
            return new ValueTaskAwait(vt);
        }
        
        public static Movable<T> AsPoTask<T>(this Task<T> vt)
        {
            return new TaskAwait<T>(vt);
        }
        
        public static Movable AsPoTask<T>(this Task vt)
        {
            return new TaskAwait(vt);
        }

        internal static string Tag(this string str, string tag, string arg = null)
        {
            var builder = new StringBuilder($"<{tag}");
            if (!string.IsNullOrEmpty(arg)) builder.Append($"={arg}");
            builder.Append(">").Append(str).Append($"</{tag}>");
            return builder.ToString();
        }
    }
}