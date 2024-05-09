using System.Collections;

namespace Ponito.Core.Asyncs.Tasks.Movables
{
    internal class IEnumeratorWaiter : MovableBase 
    {
        private IEnumerator ie { get; }

        public IEnumeratorWaiter(IEnumerator ie)
        {
            this.ie = ie;
        }
        
        public override bool MoveNext()
        {
            if (IsCompleted) return false;
            if (ie.MoveNext()) return true;
            return FinishMoveNext();
        }
    }
}