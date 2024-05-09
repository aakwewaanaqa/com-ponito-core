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
            return new Yielder();
        }

        public static Movable Delay(int milliseconds)
        {
            return new Delayer(milliseconds);
        }

        public static Movable Delay(float seconds)
        {
            return new Delayer(seconds);
        }

        public static Movable WaitWhile(Func<bool> predicate)
        {
            return new PredicateWaiter(predicate, true);
        }

        public static Movable WaitUntil(Func<bool> predicate)
        {
            return new PredicateWaiter(predicate);
        }

        [AsyncMethodBuilder(typeof(PoTask))]
        public static Movable Create(IEnumerator ie)
        {
            return new IEnumeratorWaiter(ie);
        }
    }
}