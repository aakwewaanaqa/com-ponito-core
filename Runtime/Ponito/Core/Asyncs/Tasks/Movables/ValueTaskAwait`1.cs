using System.Threading.Tasks;

namespace Ponito.Core.Asyncs.Tasks.Movables
{
    public class ValueTaskAwait<T> : MovableBase<T>
    {
        private ValueTask<T> vt { get; }

        public ValueTaskAwait(ValueTask<T> vt)
        {
            this.vt = vt;
        }

        public override T GetResult()
        {
            return vt.Result;
        }

        public override bool MoveNext()
        {
            if (IsCompleted) return false;
            if (!vt.IsCompleted) return true;
            return FinishMoveNext();
        }
    }
}