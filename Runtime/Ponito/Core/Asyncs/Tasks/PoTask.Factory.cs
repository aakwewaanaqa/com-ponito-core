using System;
using Ponito.Core.Asyncs.Tasks.Movables;

namespace Ponito.Core.Asyncs.Tasks
{
    public partial class PoTask
    {
        public static Yielder Yield()
        {
            return new Yielder();
        }

        public static Delayer Delay(int milliseconds)
        {
            return new Delayer(milliseconds);
        }
        
        public static Delayer Delay(float seconds)
        {
            return new Delayer(seconds);
        }

        public static PredicateWaiter WaitWhile(Func<bool> predicate)
        {
            return new PredicateWaiter(predicate, true);
        }

        public static PredicateWaiter WaitUntil(Func<bool> predicate)
        {
            return new PredicateWaiter(predicate, false);
        }
    }
}