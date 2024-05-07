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
    public partial class PoTask
    {
        internal readonly bool    isFromBuilder;
        internal readonly short   token;
        internal          Movable source;

        public PoTask(bool isFromBuilder, Movable source = null)
        {
            this.isFromBuilder = isFromBuilder;
            this.source        = source;
            this.token         = token;
        }

        public Awaiter GetAwaiter() => new(this);

        public class Awaiter : INotifyCompletion, Movable
        {
            private readonly PoTask task;
            private          Action continuation;

            public Awaiter(in PoTask task)
            {
                this.task         = task;
                this.continuation = null;
            }

            public Action Continuation => continuation;

            public bool IsCompleted => task.source?.IsCompleted ?? false;

            public void GetResult()
            {
            }

            public bool MoveNext()
            {
                var builder = task.isFromBuilder;
                var source  = task.source;
                if (!builder) return source.MoveNext();
                if (source.IsCompleted) continuation?.Invoke();
                return false;
            }

            public void OnCompleted(Action continuation)
            {
                var builder = task.isFromBuilder;
                if (task.source is { Continuation: null })
                {
                    typeof(Awaiter).F(nameof(OnCompleted));
                    task.source.OnCompleted(continuation);
                    MovableRunner.Instance.Queue(this);
                }
                else if (builder && this.continuation == null)
                {
                    typeof(Awaiter).F(nameof(OnCompleted));
                    this.continuation = continuation;
                    MovableRunner.Instance.Queue(this);
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