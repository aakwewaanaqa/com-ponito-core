using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.Asyncs.Tasks;
using Ponito.Core.Asyncs.Tasks.Movables;
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
            while (!vt.IsCompleted) await Controls.Yield();
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async PoTask<T> AsPoTask<T>(this ValueTask<T> vt)
        {
            while (!vt.IsCompleted) await Controls.Yield();
            return vt.Result;
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async PoTask AsPoTask(this Task t)
        {
            while (!t.IsCompleted) await Controls.Yield();
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async PoTask<T> AsPoTask<T>(this Task<T> t)
        {
            while (!t.IsCompleted) await Controls.Yield();
            return t.Result;
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerator WaitAsCoroutine(this Movable movable)
        {
            while (movable.MoveNext()) yield return eof;
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerator WaitAsCoroutine(this PoTask task)
        {
            var movable = task.GetAwaiter();
            while (movable.MoveNext()) yield return eof;
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerator WaitAsCoroutine<T>(this PoTask<T> task, CoroutineResult<T> result = null)
        {
            var movable = task.GetAwaiter();
            while (movable.MoveNext()) yield return eof;
            if (result != null) result.value = movable.GetResult();
        }
        
        public static async PoTask Delay(this float seconds, CancellationToken ct = default)
        {
            await Controls.Delay(seconds, ct);
        }
    }
}