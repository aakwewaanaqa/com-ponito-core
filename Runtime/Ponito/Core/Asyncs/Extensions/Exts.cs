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
        private static YieldInstruction Eof = new WaitForEndOfFrame();

        [DebuggerHidden]
        public static Movable<T> AsPoTask<T>(this ValueTask<T> vt)
        {
            return new ValueTaskAwait<T>(vt);
        }

        [DebuggerHidden]
        public static Movable AsPoTask(this ValueTask vt)
        {
            return new ValueTaskAwait(vt);
        }

        [DebuggerHidden]
        public static Movable<T> AsPoTask<T>(this Task<T> t)
        {
            return new TaskAwait<T>(t);
        }

        [DebuggerHidden]
        public static Movable AsPoTask(this Task t)
        {
            return new TaskAwait(t);
        }

        public static IEnumerator RunAsCoroutine(this Movable movable, Promise p = null)
        {
            var dummy        = new PoTask();
            var dummyAwaiter = dummy.GetAwaiter();
            MovableRunner.Singleton.AwaitSource(dummy, movable, null);
            while (!dummyAwaiter.IsCompleted) yield return Eof;

            if (p != null && dummy.Ex != null)
            {
                p.Ex = new PromiseException(dummy.Ex);
                yield break;
            }
        }

        public static IEnumerator RunAsCoroutine(this PoTask task, Promise p = null)
        {
            var dummy = new PoTask();
            MovableRunner.Singleton.AwaitSource(dummy, task.GetAwaiter(), null);
            while (!dummy.GetAwaiter().IsCompleted) yield return Eof;

            if (p != null && dummy.Ex != null)
            {
                p.Ex = new PromiseException(dummy.Ex);
                yield break;
            }
        }

        public static IEnumerator RunAsCoroutine<T>(this PoTask<T> task, Promise<T> p = null)
        {
            var awaiter      = task.GetAwaiter();
            var dummy        = new PoTask();
            var dummyAwaiter = dummy.GetAwaiter();
            MovableRunner.Singleton.AwaitSource(dummy, awaiter, null);
            while (!dummyAwaiter.IsCompleted) yield return Eof;

            if (p != null && dummy.Ex != null)
            {
                p.Ex = new PromiseException(dummy.Ex);
                yield break;
            }

            if (p != null) p.Result = awaiter.GetResult();
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