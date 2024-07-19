using System.Threading.Tasks;

namespace Ponito.Core.Asyncs.Tasks.Movables
{
    internal class TaskAwait : MovableBase
    {
        public TaskAwait(Task task)
        {
            this.task = task;
        }

        private Task task { get; }

        public override bool MoveNext()
        {
            if (Ct.IsCancellationRequested) return false;
            if (IsCompleted) return false;
            if (!task.IsCompleted) return true;
            return ContinueMoveNext();
        }
    }
}