using System.Threading.Tasks;

namespace Ponito.Core.Asyncs.Tasks.Movables
{
    internal class TaskAwait : MovableBase
    {
        private Task task { get; }

        public TaskAwait(Task task)
        {
            this.task = task;
        }
        
        public override bool MoveNext()
        {
            if (IsCompleted) return false;
            if (!task.IsCompleted) return true;
            return FinishMoveNext();
        }
    }
}