using System;
using System.Runtime.CompilerServices;
using CompilerServices;

namespace Ponito.Core.Promises.Compilations
{
    public class AsyncPoPoTask<TStateMachine> : StateMachineRunnerPromise, PoTaskSource
        where TStateMachine : IAsyncStateMachine
    {
        private IAsyncStateMachine              stateMachine { get; set; }
        private CompletionSourceCore<AsyncUnit> core         { get; set; }

        public Action MoveNext => Run;
        public PoTask Task     => new(this);

        private AsyncPoPoTask()
        {
        }

        public static void SetStateMachine(
            ref TStateMachine stateMachine,
            ref StateMachineRunnerPromise runnerPromiseFieldRef)
        {
            var result = new AsyncPoPoTask<TStateMachine>();
            runnerPromiseFieldRef = result;
            result.stateMachine   = stateMachine;
        }

        private void Run()
        {
            stateMachine.MoveNext();
        }
    }

    public class AsyncPoTask<TStateMachine, T> : StateMachineRunnerPromise
        where TStateMachine : IAsyncStateMachine
    {
    }
}