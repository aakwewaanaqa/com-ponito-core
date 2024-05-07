using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using Ponito.Core.Asyncs.Tasks;

namespace Ponito.Core.Asyncs.Compilations
{
    [StructLayout(LayoutKind.Auto)]
    public struct PoTaskBuilder
    {
        private PoTask    task;
        private Exception ex;

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PoTaskBuilder Create()
        {
            return default;
        }

        public PoTask Task
        {
            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return task; }
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetException(Exception exception)
        {
            // if (runnerPromise == null) ex = exception;
            // else runnerPromise.SetException(exception);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetResult()
        {
            // runnerPromise?.SetResult();
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            // don't use boxed stateMachine.
        }
    }

    public struct PoTaskBuilder<T>
    {
        private PoTask<T> task;
        private Exception ex;
        private T         result;

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PoTaskBuilder<T> Create()
        {
            return default;
        }

        public PoTask<T> Task
        {
            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return task;
            }
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetException(Exception exception)
        {
            // if (runnerPromise is null) ex = exception;
            // else runnerPromise.SetException(exception);
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetResult(T result)
        {
            this.result = result;
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
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

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0436