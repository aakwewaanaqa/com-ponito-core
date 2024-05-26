using System;
using Ponito.Core.Asyncs.Tasks;
using Ponito.Core.Asyncs.Tasks.Movables;

namespace Ponito.Core.Asyncs.Promises
{
    public partial class Promise<T> : Promise, IDisposable
    {
        private T result;

        public T Result
        {
            get
            {
                ValidateThrow(this);
                return result;
            }
            set => result = value;
        }

        public static implicit operator T(Promise<T> p)
        {
            return p.Result;
        }

        public new void Dispose()
        {
            result = default;
        }

        public new Awaiter GetAwaiter() => new(this);

        public new class Awaiter : MovableBase<T>
        {
            private Promise<T> promise { get; }

            internal Awaiter(Promise<T> p)
            {
                ValidateThrow(p);
                promise = p;
            }

            public override T GetResult()
            {
                return promise.Result;
            }

            public override bool MoveNext()
            {
                ValidateThrow(promise);
                if (IsCompleted) return false;
                return promise.State switch
                {
                    PromiseState.Doing => true,
                    PromiseState.Done  => ContinueMoveNext(),
                    _                  => throw new ArgumentOutOfRangeException()
                };
            }
        }
    }
}