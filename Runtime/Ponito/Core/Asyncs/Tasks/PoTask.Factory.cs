using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.Asyncs.Tasks.Movables;
using UnityEngine;

namespace Ponito.Core.Asyncs.Tasks
{
    public partial class PoTask
    {
        
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Movable Yield(CancellationToken ct = default)
        {
            return new YieldAwait(ct);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Movable Delay(int milliseconds, CancellationToken ct = default)
        {
            return new DelayAwait(milliseconds, ct);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Movable Delay(float seconds, CancellationToken ct = default)
        {
            return new DelayAwait(seconds, ct);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Movable WaitWhile(Func<bool> predicate, CancellationToken ct = default)
        {
            return new PredicateAwait(predicate, true, ct);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Movable WaitUntil(Func<bool> predicate, CancellationToken ct = default)
        {
            return new PredicateAwait(predicate, false, ct);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Movable Create(IEnumerator ie, CancellationToken ct = default)
        {
            return new IEnumeratorAwait(ie, ct);
        }
    }
}