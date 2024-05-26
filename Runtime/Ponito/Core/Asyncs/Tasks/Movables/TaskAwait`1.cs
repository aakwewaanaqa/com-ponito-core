using System.Threading.Tasks;

namespace Ponito.Core.Asyncs.Tasks.Movables
{
    internal class TaskAwait<T> : MovableBase<T>
    {
        private Task<T> task { get; }

        public TaskAwait(Task<T> task)
        {
            this.task = task;
        }

        public override T GetResult()
        {
            return task.Result;
        }

        public override bool MoveNext()
        {
            if (IsCompleted) return false;
            if (!task.IsCompleted) return true;
            return ContinueMoveNext();
        }
    }
}