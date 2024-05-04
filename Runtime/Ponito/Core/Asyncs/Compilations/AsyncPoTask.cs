using System;
using System.Runtime.CompilerServices;
using Ponito.Core.Asyncs.Tasks;
using Ponito.Core.Asyncs.Tasks.Sources;

namespace Ponito.Core.Asyncs.Compilations
{
    public class AsyncPoTask<TStateMachine> : StateMachineRunnerPromise,
                                              PoTaskSource,
                                              TaskPoolNode<AsyncPoTask<TStateMachine>>
        where TStateMachine : IAsyncStateMachine
    {
        private        IAsyncStateMachine                    stateMachine;
        private        PoTaskCompletionSourceCore<AsyncUnit> core;
        private static TaskPool<AsyncPoTask<TStateMachine>>  pool;
        private        AsyncPoTask<TStateMachine>            nextNode;

        public PoTask Task => new(this, core.Version);

        public     Action                     MoveNext { get; }
        public ref AsyncPoTask<TStateMachine> NextNode => ref nextNode;

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
            // TODO: Track task
            // TaskTracker.RemoveTracking(this);
            core.Reset();
            stateMachine = default;
            pool.TryPush(this);
        }

        private bool TryReturn()
        {
            // TODO: Track task
            // TaskTracker.RemoveTracking(this);
            core.Reset();
            stateMachine = default;
            return pool.TryPush(this);
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

    public class AsyncPoTask<TStateMachine, T> : StateMachineRunnerPromise<T>,
                                                 PoTaskSource<T>,
                                                 TaskPoolNode<AsyncPoTask<TStateMachine, T>>
        where TStateMachine : IAsyncStateMachine
    {
        private        IAsyncStateMachine                      stateMachine;
        private        PoTaskCompletionSourceCore<T>           core;
        private static TaskPool<AsyncPoTask<TStateMachine, T>> pool;
        private        AsyncPoTask<TStateMachine, T>           nextNode;

        public PoTask<T> Task => new(this, core.Version);

        public     Action                        MoveNext { get; }
        public ref AsyncPoTask<TStateMachine, T> NextNode => ref nextNode;

        private AsyncPoTask()
        {
            MoveNext = Run;
        }

        public static void SetStateMachine(
            ref TStateMachine stateMachine,
            ref StateMachineRunnerPromise<T> runnerPromiseFieldRef)
        {
            var result = new AsyncPoTask<TStateMachine, T>();
            runnerPromiseFieldRef = result;
            result.stateMachine   = stateMachine;
        }

        private void Return()
        {
            // TODO: Track task
            // TaskTracker.RemoveTracking(this);
            core.Reset();
            stateMachine = default;
            pool.TryPush(this);
        }

        private bool TryReturn()
        {
            // TODO: Track task
            // TaskTracker.RemoveTracking(this);
            core.Reset();
            stateMachine = default;
            return pool.TryPush(this);
        }

        private void Run()
        {
            stateMachine.MoveNext();
        }

        public void SetResult(T result) => core.TrySetResult(result);

        public void SetException(Exception ex) => core.TrySetException(ex);

        public T GetResult(short token)
        {
            try
            {
                return core.GetResult(token);
            }
            finally
            {
                TryReturn();
            }
        }

        void PoTaskSource.GetResult(short token)
        {
            GetResult(token);
        }

        public PoTaskStatus GetStatus(short token) => core.GetStatus(token);

        public void OnCompleted(Action<object> continuation, object state, short token) =>
            core.OnCompleted(continuation, state, token);
    }
}