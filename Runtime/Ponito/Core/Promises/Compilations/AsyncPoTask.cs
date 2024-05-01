using System.Runtime.CompilerServices;

namespace Ponito.Core.Promises.Compilations
{
    public class AsyncPoTask<TStateMachine> : StateMachineRunnerPromise
        where TStateMachine : IAsyncStateMachine
    {
    }

    public class AsyncPoTask<TStateMachine, T> : StateMachineRunnerPromise
        where TStateMachine : IAsyncStateMachine
    {
    }
}