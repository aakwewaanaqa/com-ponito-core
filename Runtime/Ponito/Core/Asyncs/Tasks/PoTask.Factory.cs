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
        public static Movable Yield(CancellationToken ct = default)
        {
            return new YieldAwait(ct);
        }

        [DebuggerHidden]
        public static Movable Delay(int milliseconds)
        {
            return new DelayAwait(milliseconds);
        }

        [DebuggerHidden]
        public static Movable Delay(float seconds)
        {
            return new DelayAwait(seconds);
        }

        [DebuggerHidden]
        public static Movable WaitWhile(Func<bool> predicate)
        {
            return new PredicateAwait(predicate, true);
        }

        [DebuggerHidden]
        public static Movable WaitUntil(Func<bool> predicate)
        {
            return new PredicateAwait(predicate);
        }

        [DebuggerHidden]
        public static Movable Create(IEnumerator ie)
        {
            return new IEnumeratorAwait(ie);
        }
    }
}