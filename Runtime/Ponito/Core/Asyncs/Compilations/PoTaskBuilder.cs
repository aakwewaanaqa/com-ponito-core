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
        private PoTask task;
        
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PoTaskBuilder Create()
        {
            return new PoTaskBuilder
            {
                Task = new PoTask()
            };
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public PoTask Task
        {
            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => task;
            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private set => task = value;
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetException(Exception ex)
        {
            // throw exception;
            Task.Ex = ex;
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetResult()
        {
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            awaiter.OnCompleted(stateMachine.MoveNext);
            MovableRunner.Singleton.AwaitSource(task, awaiter, stateMachine);
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
            MovableRunner.Singleton.AwaitSource(task, awaiter, stateMachine);
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
}

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0436