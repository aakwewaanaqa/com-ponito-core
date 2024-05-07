using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using Ponito.Core.Asyncs.Compilations;
using Ponito.Core.Asyncs.Tasks.Sources;
using Ponito.Core.DebugHelper;
using UnityEngine;

namespace Ponito.Core.Asyncs.Tasks
{
    [AsyncMethodBuilder(typeof(PoTaskBuilder))]
    public readonly partial struct PoTask
    {
        private readonly short   token;
        private readonly Movable source;

        public PoTask(Movable source, short token)
        {
            this.source = source;
            this.token  = token;
        }

        public Awaiter GetAwaiter() => new(this);

        public readonly struct Awaiter : INotifyCompletion
        {
            private readonly PoTask task;

            public Awaiter(in PoTask task)
            {
                this.task = task;
            }

            public bool IsSource => false;

            public bool IsCompleted => false;

            public void GetResult()
            {
            }

            public void OnCompleted(Action continuation)
            {
                typeof(Awaiter).F(nameof(OnCompleted));
                if (task.source != null)
                {
                    Debug.Log("task.source.OnCompleted");
                    task.source.OnCompleted(continuation);
                }
                else
                {
                    Debug.Log("continuation()");
                    continuation();
                }
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