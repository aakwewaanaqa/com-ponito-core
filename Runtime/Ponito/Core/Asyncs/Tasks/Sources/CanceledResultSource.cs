using System;
using System.Threading;
using Ponito.Core.Asyncs.Compilations;

namespace Ponito.Core.Asyncs.Tasks.Sources
{
    public class CanceledResultSource : PoTaskSource
    {
        private readonly CancellationToken ct;

        public CanceledResultSource(CancellationToken ct)
        {
            this.ct = ct;
        }

        public PoTaskStatus GetStatus(short token)
        {
            return PoTaskStatus.Canceled;
        }

        public void OnCompleted(Action<object> continuation, object state, short token)
        {
            continuation(state);
        }

        public void GetResult(short token)
        {
            throw new InvalidOperationException();
        }
    }

    public class CanceledResultSource<T> : PoTaskSource<T>
    {
        private readonly CancellationToken ct;

        public CanceledResultSource(CancellationToken ct)
        {
            this.ct = ct;
        }

        public PoTaskStatus GetStatus(short token)
        {
            return PoTaskStatus.Canceled;
        }

        public void OnCompleted(Action<object> continuation, object state, short token)
        {
            continuation(state);
        }

        public T GetResult(short token)
        {
            throw new InvalidOperationException();
        }
        
        void PoTaskSource.GetResult(short token)
        {
            throw new InvalidOperationException();
        }
    }
}