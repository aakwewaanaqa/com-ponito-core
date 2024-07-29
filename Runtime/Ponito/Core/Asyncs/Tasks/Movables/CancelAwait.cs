using System;

namespace Ponito.Core.Asyncs.Tasks.Movables
{
    public class CancelAwait : MovableBase
    {
        public override void OnCompleted(Action continuation)
        {
            continuation?.Invoke();
            this.continuation = null;
        }

        public override bool MoveNext()
        {
            return ContinueMoveNext();
        }
    }
}