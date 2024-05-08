using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using Ponito.Core.Asyncs.Compilations;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.Asyncs.Tasks.Sources;
using Ponito.Core.DebugHelper;
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

        public Awaiter GetAwaiter() => new(this);

        public class Awaiter : Movable
        {
            internal PoTask             task         { get; }
            internal IAsyncStateMachine machine      { get; set; }
            private  Action             continuation { get; set; }

            public Awaiter(in PoTask task)
            {
                this.task    = task;
                continuation = null;
            }

            public bool IsCompleted => task.source?.IsCompleted ?? true;

            public void GetResult()
            {
            }

            public bool MoveNext()
            {
                if (!IsCompleted) return true;

                continuation?.Invoke();
                return false;
            }

            public void OnCompleted(Action continuation)
            {
                if (this.continuation != null)
                {
                    Debug.LogError("continuation overflown");
                    return;
                }

                this.continuation = continuation;
                MovableRunner.Instance.Queue(this);
            }
        }
    }

    [AsyncMethodBuilder(typeof(PoTaskBuilder<>))]
    public readonly partial struct PoTask<T>
    {
        private readonly short   token;
        private readonly Movable source;

        public PoTask(Movable source, short token)
        {
            this.source = source;
            this.token  = token;
        }

        public Awaiter GetAwaiter() => new(this);

        public struct Awaiter : INotifyCompletion
        {
            private readonly PoTask<T> task;
            private          Action    continuation;

            public Awaiter(PoTask<T> task)
            {
                this.task         = task;
                this.continuation = null;
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