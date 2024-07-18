using System.Collections;
using System.Threading;

namespace Ponito.Core.Asyncs.Tasks.Movables
{
    internal class IEnumeratorAwait : MovableBase 
    {
        private CancellationToken ct { get; }
        private IEnumerator       ie { get; }

        public IEnumeratorAwait(IEnumerator ie, CancellationToken ct = default)
        {
            this.ie = ie;
            this.ct = ct;
        }
        
        public override bool MoveNext()
        {
            if (ct.IsCancellationRequested) return false;
            if (IsCompleted) return false;
            if (ie.MoveNext()) return true;
            return ContinueMoveNext();
        }
    }
}