using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.Asyncs.Promises;
using Ponito.Core.Asyncs.Tasks;
using Ponito.Core.Asyncs.Tasks.Movables;
using Ponito.Core.DebugHelper;
using UnityEngine;

namespace Ponito.Core.Asyncs.Extensions
{
    public static partial class Exts
    {
        [DebuggerHidden]
        public static Movable<T> AsPoTask<T>(this ValueTask<T> vt)
        {
            return new ValueTaskAwait<T>(vt);
        }

        [DebuggerHidden]
        public static Movable AsPoTask<T>(this ValueTask vt)
        {
            return new ValueTaskAwait(vt);
        }

        [DebuggerHidden]
        public static Movable<T> AsPoTask<T>(this Task<T> t)
        {
            return new TaskAwait<T>(t);
        }

        [DebuggerHidden]
        public static Movable AsPoTask<T>(this Task t)
        {
            return new TaskAwait(t);
        }

        [DebuggerHidden]
        public static IEnumerator RunAsCoroutine(this PoTask t)
        {
            var awaiter = t.GetAwaiter();
            MovableRunner.Singleton.Enqueue(awaiter);
            while (!awaiter.IsCompleted) yield return new WaitForEndOfFrame();
        }

        [DebuggerHidden]
        public static IEnumerator RunAsCoroutine<T>(this PoTask<T> t, Promise<T> promise)
        {
            promise.State = PromiseState.Doing;
            var awaiter = t.GetAwaiter();
            MovableRunner.Singleton.Enqueue(awaiter);
            while (!awaiter.IsCompleted) yield return new WaitForEndOfFrame();
            try
            {
                if (awaiter.Exception != null) throw awaiter.Exception;
                promise.Result = awaiter.GetResult();
                promise.State = PromiseState.Done;
            }
            catch (Exception e)
            {
                promise.Error = e;
                promise.State = PromiseState.Failed;
            }
        }

        [DebuggerHidden]
        public static IEnumerator RunAsCoroutine(this Movable t)
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