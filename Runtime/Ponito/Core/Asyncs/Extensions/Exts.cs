using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.Asyncs.Promises;
using Ponito.Core.Asyncs.Tasks;
using UnityEngine;

namespace Ponito.Core.Asyncs.Extensions
{
    public static partial class Exts
    {
        private static readonly YieldInstruction eof = new WaitForEndOfFrame();

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async PoTask AsPoTask(this ValueTask vt)
        {
            while (!vt.IsCompleted) await PoTask.Yield();
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async PoTask<T> AsPoTask<T>(this ValueTask<T> vt)
        {
            while (!vt.IsCompleted) await PoTask.Yield();
            return vt.Result;
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async PoTask AsPoTask(this Task t)
        {
            while (!t.IsCompleted) await PoTask.Yield();
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async PoTask<T> AsPoTask<T>(this Task<T> t)
        {
            while (!t.IsCompleted) await PoTask.Yield();
            return t.Result;
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerator RunAsCoroutine(this Movable movable, Promise p = null)
        {
            if (p != null) p.State = PromiseState.Doing;
            while (movable.MoveNext()) yield return eof;
            if (p != null) p.State = PromiseState.Done;
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerator RunAsCoroutine(this PoTask task, Promise p = null)
        {
            var movable            = task.GetAwaiter();
            if (p != null) p.State = PromiseState.Doing;
            while (movable.MoveNext()) yield return eof;

            if (p != null && movable.Ex != null)
            {
                p.State = PromiseState.Failed;
                p.Ex    = new PromiseException(movable.Ex);
                yield break;
            }

            if (p != null) p.State = PromiseState.Done;
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerator RunAsCoroutine<T>(this PoTask<T> task, Promise<T> p = null)
        {
            var movable            = task.GetAwaiter();
            if (p != null) p.State = PromiseState.Doing;
            while (movable.MoveNext()) yield return eof;

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
        
        public static async PoTask Delay(this float seconds, CancellationToken ct = default)
        {
            await PoTask.Delay(seconds, ct);
        }
    }
}