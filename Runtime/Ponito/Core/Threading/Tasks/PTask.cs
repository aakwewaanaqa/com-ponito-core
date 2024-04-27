using System;
using System.Runtime.CompilerServices;
using Ponito.Core.Threading.Enums;
using Ponito.Core.Threading.Interfaces;

namespace Ponito.Core.Threading.Tasks
{
    internal class Actions
    {
        public static readonly Action<object> Continue = Call;

        private static void Call(object state)
        {
            ((Action)state)?.Invoke();
        }
    }

    [AsyncMethodBuilder(typeof(PTaskBuilder))]
    public readonly struct PTask
    {
        private readonly PTaskSource source;

        public Awaiter GetAwaiter()
        {
            return new Awaiter(this);
        }

        public PTaskStatus Status
        {
            get
            {
                if (source is null) return PTaskStatus.Done;
                return source.GetStatus();
            }
        }

        public void GetResult()
        {
            source?.GetResult();
        }

        public readonly struct Awaiter : INotifyCompletion
        {
            private readonly PTask p;

            public Awaiter(in PTask p)
            {
                this.p = p;
            }

            public bool IsCompleted => p.Status is not PTaskStatus.Doing;

            public void GetResult() => p.source?.GetResult();

            public void OnCompleted(Action continuation)
            {
                if (p.source == null) continuation();
                else p.source.OnCompleted(Actions.Continue, continuation);
            }
        }
    }
}