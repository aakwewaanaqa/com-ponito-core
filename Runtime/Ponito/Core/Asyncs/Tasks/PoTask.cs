using System;
using System.Runtime.CompilerServices;
using Ponito.Core.Asyncs.Compilations;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.Asyncs.Tasks.Movables;

namespace Ponito.Core.Asyncs.Tasks
{
    [AsyncMethodBuilder(typeof(PoTaskBuilder))]
    public partial class PoTask : PoTaskBase
    {
        public PoTask(Movable source = null) : base(source)
        {
        }

        public Awaiter GetAwaiter()
        {
            return new Awaiter(this);
        }
        
        public class Awaiter : MovableBase, IDisposable
        {
            public Awaiter(in PoTask task)
            {
                this.task = task;
            }

            private PoTask task { get; }

            public override Exception Exception
            {
                get => task.Exception;
                set => task.Exception = value;
            }

            public override bool MoveNext()
            {
                if (IsCompleted) return false;
                if (!(task.Source?.IsCompleted ?? true)) return true;
                return FinishMoveNext();
            }
        }
    }
}