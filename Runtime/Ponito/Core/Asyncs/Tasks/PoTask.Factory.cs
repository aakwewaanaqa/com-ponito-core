using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.Asyncs.Tasks.Movables;

namespace Ponito.Core.Asyncs.Tasks
{
    public partial class PoTask
    {
        public static Movable Yield()
        {
            return new YieldAwait();
        }

        public static Movable Delay(int milliseconds)
        {
            return new DelayAwait(milliseconds);
        }

        public static Movable Delay(float seconds)
        {
            return new DelayAwait(seconds);
        }

        public static Movable WaitWhile(Func<bool> predicate)
        {
            return new PredicateAwait(predicate, true);
        }

        public static Movable WaitUntil(Func<bool> predicate)
        {
            return new PredicateAwait(predicate);
        }

        [AsyncMethodBuilder(typeof(PoTask))]
        public static Movable Create(IEnumerator ie)
        {
            return new IEnumeratorAwait(ie);
        }
    }
}