using System;

namespace Ponito.Core.Asyncs.Promises
{
    public class Promise<T> : Promise, IDisposable
    {
        private T result;

        public T Result
        {
            get
            {
                var invalid = new InvalidOperationException(null, Error as Exception);
                return State switch
                {
                    PromiseState.Done => result,
                    _                 => throw invalid
                };
            }
            set => result = value;
        }

        public static implicit operator T(Promise<T> p)
        {
            return p.Result;
        }

        public void Dispose()
        {
            result = default;
        }
    }
}