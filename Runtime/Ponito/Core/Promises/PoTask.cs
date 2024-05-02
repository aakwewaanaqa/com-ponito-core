using System;
using System.Runtime.CompilerServices;
using Ponito.Core.Promises.Compilations;

namespace Ponito.Core.Promises
{
    [AsyncMethodBuilder(typeof(PoTaskBuilder))]
    public readonly partial struct PoTask
    {
        private short        token  { get; }
        private PoTaskSource source { get; }

        public PoTask(PoTaskSource source, short token)
        {
            this.source = source;
            this.token  = token;
        }

        public PoTaskStatus Status => source?.GetStatus(token) ?? PoTaskStatus.Succeeded;

        public Awaiter GetAwaiter() => new(this);

        public readonly struct Awaiter : INotifyCompletion
        {
            private readonly PoTask task;

            public Awaiter(PoTask task)
            {
                this.task = task;
            }

            public bool IsCompleted => task.Status is not PoTaskStatus.Pending;

            public void GetResult()
            {
                if (task.source is null) return;
                task.source.GetResult(task.token);
            }

            public void OnCompleted(Action continuation)
            {
                if (task.source == null) continuation();
                task.source!.OnCompleted(AwaiterShared.ContinuationDelegate, continuation, task.token);
            }

            public void SourceOnCompleted(Action<object> continuation, object state)
            {
                if (task.source == null) continuation(state);
                task.source!.OnCompleted(continuation, state, task.token);
            }
        }
    }

    [AsyncMethodBuilder(typeof(PoTaskBuilder<>))]
    public readonly partial struct PoTask<T>
    {
        private readonly short           token;
        private readonly PoTaskSource<T> source;
        private readonly T               result;

        public PoTask(PoTaskSource<T> source, short token)
        {
            this.source = source;
            this.token  = token;
            result      = default;
        }
        
        public PoTask(T result)
        {
            this.source = default;
            this.token  = default;
            this.result = result;
        }

        public PoTaskStatus Status => source?.GetStatus(token) ?? PoTaskStatus.Succeeded;

        public Awaiter GetAwaiter() => new(this);

        public readonly struct Awaiter : INotifyCompletion
        {
            private readonly PoTask<T> task;

            public Awaiter(PoTask<T> task)
            {
                this.task = task;
            }

            public bool IsCompleted => task.Status is not PoTaskStatus.Pending;

            public T GetResult(T result)
            {
                if (task.source is null) return task.result;
                return task.source.GetResult(task.token);
            }

            public void OnCompleted(Action continuation)
            {
                if (task.source == null) continuation();
                task.source!.OnCompleted(AwaiterShared.ContinuationDelegate, continuation, task.token);
            }

            public void SourceOnCompleted(Action<object> continuation, object state)
            {
                if (task.source == null) continuation(state);
                task.source!.OnCompleted(continuation, state, task.token);
            }
        }
    }
}