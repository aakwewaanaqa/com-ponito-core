using System;
using Ponito.Core.Asyncs.Tasks;

namespace Ponito.Core.Asyncs.Compilations
{
    public interface StateMachineRunnerPromise
    {
        Action MoveNext { get; }
        PoTask Task     { get; }
        void   SetResult();
        void   SetException(Exception exception);
    }

    // TODO: Does StateMachineRunnerPromise<T> inherits StateMachineRunnerPromise?
    public interface StateMachineRunnerPromise<T>
    {
        Action    MoveNext { get; }
        PoTask<T> Task     { get; }
        void      SetResult(T result);
        void      SetException(Exception exception);
    }
}