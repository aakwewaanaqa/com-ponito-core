using System;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.Asyncs.Tasks;
using Ponito.Core.Asyncs.Tasks.Movables;
using Ponito.Core.DebugHelper;
using UnityEngine;

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
        
        public static Movable<T> AsPoTask<T>(this Task<T> t)
        {
            return new TaskAwait<T>(t);
        }
        
        public static Movable AsPoTask<T>(this Task t)
        {
            return new TaskAwait(t);
        }

        public static IEnumerator AsCoroutine(this PoTask t)
        {
            var awaiter = t.GetAwaiter();
            MovableRunner.Singleton.Enqueue(awaiter);
            while (!awaiter.IsCompleted) yield return new WaitForEndOfFrame();
        }
        
        public static IEnumerator AsCoroutine(this Movable t)
        {
            MovableRunner.Singleton.Enqueue(t);
            while (!t.IsCompleted) yield return new WaitForEndOfFrame();
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