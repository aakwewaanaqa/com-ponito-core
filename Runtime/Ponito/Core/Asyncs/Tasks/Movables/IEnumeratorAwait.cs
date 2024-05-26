using System.Collections;

namespace Ponito.Core.Asyncs.Tasks.Movables
{
    internal class IEnumeratorAwait : MovableBase 
    {
        private IEnumerator ie { get; }

        public IEnumeratorAwait(IEnumerator ie)
        {
            this.ie = ie;
        }
        
        public override bool MoveNext()
        {
            if (IsCompleted) return false;
            if (ie.MoveNext()) return true;
            return ContinueMoveNext();
        }
    }
}