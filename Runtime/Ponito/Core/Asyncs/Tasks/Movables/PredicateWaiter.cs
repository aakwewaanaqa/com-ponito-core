using System;

namespace Ponito.Core.Asyncs.Tasks.Movables
{
    public class PredicateWaiter : MovableBase
    {
        private Func<bool> predicate   { get; set; }
        private bool       waitForTrue { get; set; }
        
        public PredicateWaiter(Func<bool> predicate, bool waitForTrue = false)
        {
            this.predicate   = predicate;
            this.waitForTrue = waitForTrue;
        }
        
        public override bool MoveNext()
        {
            if (IsCompleted) return false;
            if (waitForTrue == predicate()) return true;
            return FinishMoveNext();
        }

        public override void Dispose()
        {
            base.Dispose();
            predicate = null;
        }
    }
}