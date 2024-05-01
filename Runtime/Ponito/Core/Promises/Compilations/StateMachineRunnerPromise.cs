using System;

namespace Ponito.Core.Promises.Compilations
{
    public interface StateMachineRunnerPromise
    {
        Action MoveNext { get; }
        PoTask Task     { get; }
        void   SetResult();
        void   SetException(Exception exception);
    }

    public interface StateMachineRunnerPromise<T>
    {
        Action    MoveNext { get; }
        PoTask<T> Task     { get; }
        void      SetResult(T result);
        void      SetException(Exception exception);
    }
}