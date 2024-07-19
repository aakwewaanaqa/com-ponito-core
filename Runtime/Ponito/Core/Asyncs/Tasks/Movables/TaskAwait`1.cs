using System.Threading.Tasks;

namespace Ponito.Core.Asyncs.Tasks.Movables
{
    internal class TaskAwait<T> : MovableBase<T>
    {
        public TaskAwait(Task<T> task)
        {
            this.task = task;
        }

        private Task<T> task { get; }

        public override T GetResult()
        {
            return task.Result;
        }

        public override bool MoveNext()
        {
            if (Ct.IsCancellationRequested) return false;
            if (IsCompleted) return false;
            if (!task.IsCompleted) return true;
            return ContinueMoveNext();
        }
    }
}