using System.Threading.Tasks;

namespace Ponito.Core.Asyncs.Tasks.Movables
{
    public class ValueTaskAwait : MovableBase
    {
        private ValueTask vt { get; }
        
        public ValueTaskAwait(ValueTask vt)
        {
            this.vt = vt;
        }
        
        public override void GetResult()
        {
        }

        public override bool MoveNext()
        {
            if (IsCompleted) return false;
            if (!vt.IsCompleted) return true;
            return ContinueMoveNext();
        }
    }
}