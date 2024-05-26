using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Ponito.Core.Asyncs.Compilations;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.Asyncs.Promises;
using Ponito.Core.Asyncs.Tasks.Movables;
using UnityEngine;

namespace Ponito.Core.Asyncs.Tasks
{
    [AsyncMethodBuilder(typeof(PoTaskBuilder<>))]
    public partial class PoTask<T> : PoTask
    {
        internal T result;

        public PoTask(Movable source = null) : base(source)
        {
        }

        public new Awaiter GetAwaiter()
        {
            return new Awaiter(this);
        }

        public new class Awaiter : MovableBase<T>
        {
            public Awaiter(PoTask<T> task)
            {
                this.task = task;
            }

            private PoTask<T> task { get; }

            public override T GetResult()
            {
                if (Exception != null) throw Exception;
                Dispose();
                return task.result;
            }
            
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