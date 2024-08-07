using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.Asyncs.Tasks.Movables;

namespace Ponito.Core.Asyncs.Tasks
{
    public static class Controls
    {
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Movable Yield(CancellationToken ct = default)
        {
            if (ct.IsCancellationRequested) return new CancelAwait();
            return new YieldAwait(ct);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Movable Delay(int milliseconds, CancellationToken ct = default)
        {
            if (ct.IsCancellationRequested) return new CancelAwait();
            return new DelayAwait(milliseconds, ct);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Movable Delay(float seconds, CancellationToken ct = default)
        {
            if (ct.IsCancellationRequested) return new CancelAwait();
            return new DelayAwait(seconds, ct);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Movable WaitWhile(Func<bool> predicate, CancellationToken ct = default)
        {
            if (ct.IsCancellationRequested) return new CancelAwait();
            return new PredicateAwait(predicate, true, ct);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Movable WaitUntil(Func<bool> predicate, CancellationToken ct = default)
        {
            if (ct.IsCancellationRequested) return new CancelAwait();
            return new PredicateAwait(predicate, false, ct);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Movable Create(IEnumerator ie, CancellationToken ct = default)
        {
            if (ct.IsCancellationRequested) return new CancelAwait();
            return new IEnumeratorAwait(ie, ct);
        }
    }
}