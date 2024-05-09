using System;
using System.Runtime.CompilerServices;
using Ponito.Core.Asyncs.Compilations;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.Asyncs.Tasks.Movables;
using UnityEngine;

namespace Ponito.Core.Asyncs.Tasks
{
    [AsyncMethodBuilder(typeof(PoTaskBuilder))]
    public partial class PoTask
    {
        internal Movable source;

        public PoTask(Movable source = null)
        {
            this.source = source;
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

            internal PoTask task { get; }

            public override bool MoveNext()
            {
                if (IsCompleted) return false;
                if (!(task.source?.IsCompleted ?? true)) return true;
                return FinishMoveNext();
            }
        }
    }

    [AsyncMethodBuilder(typeof(PoTaskBuilder<>))]
    public readonly struct PoTask<T>
    {
        private readonly short   token;
        private readonly Movable source;

        public PoTask(Movable source, short token)
        {
            this.source = source;
            this.token  = token;
        }

        public Awaiter GetAwaiter()
        {
            return new Awaiter(this);
        }

        public struct Awaiter : INotifyCompletion
        {
            private readonly PoTask<T> task;
            private          Action    continuation;

            public Awaiter(PoTask<T> task)
            {
                this.task    = task;
                continuation = null;
            }

            public bool IsCompleted => false;

            public T GetResult()
            {
                return default;
            }

            public void OnCompleted(Action continuation)
            {
                this.continuation = continuation;
            }

            public bool MoveNext()
            {
                if (task.source is null)
                {
                    continuation();
                    return false;
                }

                return task.source.MoveNext();
            }
        }
    }
}