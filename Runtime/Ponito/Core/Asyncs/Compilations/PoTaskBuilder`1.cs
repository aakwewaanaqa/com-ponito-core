using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;
using Ponito.Core.Asyncs.Tasks;
using Debug = UnityEngine.Debug;

namespace Ponito.Core.Asyncs.Compilations
{
    public struct PoTaskBuilder<T>
    {
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PoTaskBuilder<T> Create()
        {
            return new PoTaskBuilder<T>
            {
                Task = new PoTask<T>()
            };
        }

        public PoTask<T> Task
        {
            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private set;
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetException(Exception exception)
        {
            Task.ex = exception;
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetResult(T result)
        {
            Task.result = result;
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            Task.TrySetSource(awaiter);
            awaiter.OnCompleted(stateMachine.MoveNext);
        }

        [DebuggerHidden]
        [SecuritySafeCritical]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter,
            ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            Task.TrySetSource(awaiter);
            awaiter.OnCompleted(stateMachine.MoveNext);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }

        [DebuggerHidden]
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            // don't use boxed stateMachine.
        }
    }
}