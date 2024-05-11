using System;
using System.Runtime.CompilerServices;
using Ponito.Core.Asyncs.Compilations;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.Asyncs.Tasks.Movables;

namespace Ponito.Core.Asyncs.Tasks
{
    [AsyncMethodBuilder(typeof(PoTaskBuilder<>))]
    public class PoTask<T> : PoTaskBase
    {
        internal T result;

        public PoTask(Movable source = null)
        {
            this.source = source;
        }

        public Awaiter GetAwaiter()
        {
            return new Awaiter(this);
        }

        public class Awaiter : MovableBase<T>
        {
            public Awaiter(PoTask<T> task)
            {
                this.task = task;
            }

            internal PoTask<T> task { get; }

            public override T GetResult()
            {
                Dispose();
                return task.result;
            }

            public override bool MoveNext()
            {
                if (IsCompleted) return false;
                if (!(task.source?.IsCompleted ?? true)) return true;
                return FinishMoveNext();
            }
        }
    }
}