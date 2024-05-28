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
            if (p != null) p.State = PromiseState.Doing;
            while (movable.MoveNext()) yield return Eof;
            if (p != null) p.State = PromiseState.Done;
        }

        public static IEnumerator RunAsCoroutine(this PoTask task, Promise p = null)
        {
            var movable            = task.GetAwaiter();
            if (p != null) p.State = PromiseState.Doing;
            while (movable.MoveNext()) yield return Eof;

            if (p != null && movable.Ex != null)
            {
                p.State = PromiseState.Failed;
                p.Ex    = new PromiseException(movable.Ex);
                yield break;
            }

            if (p != null) p.State = PromiseState.Done;
        }

        public static IEnumerator RunAsCoroutine<T>(this PoTask<T> task, Promise<T> p = null)
        {
            var movable            = task.GetAwaiter();
            if (p != null) p.State = PromiseState.Doing;
            while (movable.MoveNext()) yield return Eof;

            if (p != null && movable.Ex != null)
            {
                p.State = PromiseState.Failed;
                p.Ex    = new PromiseException(movable.Ex);
                yield break;
            }

            if (p != null)
            {
                p.Result = movable.GetResult();
                p.State  = PromiseState.Done;
            }
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