using System;
using System.Runtime.CompilerServices;
using CompilerServices;

namespace Ponito.Core.Promises.Compilations
{
    public class AsyncPoTask<TStateMachine> : StateMachineRunnerPromise, PoTaskSource
        where TStateMachine : IAsyncStateMachine
    {
        public  Action                                MoveNext     { get; }
        private IAsyncStateMachine                    stateMachine { get; set; }
        private PoTaskCompletionSourceCore<AsyncUnit> core         { get; }

        public PoTask Task => new(this, core.Version);

        private AsyncPoTask()
        {
            MoveNext = Run;
        }

        public static void SetStateMachine(
            ref TStateMachine stateMachine,
            ref StateMachineRunnerPromise runnerPromiseFieldRef)
        {
            var result = new AsyncPoTask<TStateMachine>();
            runnerPromiseFieldRef = result;
            result.stateMachine   = stateMachine;
        }

        private void Return()
        {
            // TaskTracker.RemoveTracking(this);
            core.Reset();
            stateMachine = default;
            // pool.TryPush(this);
            return;
        }

        private bool TryReturn()
        {
            // TaskTracker.RemoveTracking(this);
            core.Reset();
            stateMachine = default;
            // return pool.TryPush(this);
            return true;
        }
        
        private void Run()
        {
            stateMachine.MoveNext();
        }

        public void SetResult() => core.TrySetResult(AsyncUnit.Default);

        public void SetException(Exception ex) => core.TrySetException(ex);

        public void GetResult(short token)
        {
            try
            {
                core.GetResult(token);
            }
            finally
            {
                TryReturn();
            }
        }

        public PoTaskStatus GetStatus(short token) => core.GetStatus(token);

        public void OnCompleted(Action<object> continuation, object state, short token) =>
            core.OnCompleted(continuation, state, token);
    }

    public class AsyncPoTask<TStateMachine, T> : StateMachineRunnerPromise
        where TStateMachine : IAsyncStateMachine
    {
    }
}