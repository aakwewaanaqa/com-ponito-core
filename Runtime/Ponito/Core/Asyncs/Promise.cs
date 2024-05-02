using System;

namespace Ponito.Core.Asyncs
{
    public class Promise
    {
        public bool      IsCompleted => IsResolved || IsRejected;
        public bool      IsResolved  { get; private set; }
        public bool      IsRejected  { get; private set; }
        public Exception Error       { get; private set; }

        public void Reject(Exception error) => Error = error;
    }

    public class Promise<T> : Promise
    {
        public T Result { get; private set; }
    }
}