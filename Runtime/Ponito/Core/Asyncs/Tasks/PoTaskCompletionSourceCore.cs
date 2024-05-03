using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Ponito.Core.Asyncs.Tasks
{
    public struct PoTaskCompletionSourceCore<T>
    {
        // Struct Size: TResult + (8 + 2 + 1 + 1 + 8 + 8)

        private T              result;
        private object         error; // ExceptionHolder or OperationCanceledException
        private short          version;
        private bool           hasUnhandledError;
        private int            completedCount; // 0: completed == false
        private Action<object> continuation;
        private object         continuationState;

        public         short          Version  => version;
        private static Action<object> Sentinel => PoTaskCompletionSourceCoreShared.sentinel;

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            unchecked
            {
                version += 1;
            }

            completedCount    = 0;
            result            = default;
            error             = null;
            hasUnhandledError = false;
            continuation      = null;
            continuationState = null;
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TrySetResult(T result)
        {
            if (Interlocked.Increment(ref completedCount) != 1) return false;

            this.result = result;
            Continue();

            return true;
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TrySetException(object error)
        {
            if (Interlocked.Increment(ref completedCount) != 1) return false;

            this.error = error;
            Continue();

            return true;
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TrySetCanceled(CancellationToken ct = default)
        {
            if (Interlocked.Increment(ref completedCount) != 1) return false;

            this.hasUnhandledError = true;
            this.error             = new OperationCanceledException(ct);
            Continue();

            return true;
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PoTaskStatus GetStatus(short token)
        {
            ValidateToken(token);
            return error switch
            {
                null when continuation is null || completedCount == 0 => PoTaskStatus.Pending,
                null                                                  => PoTaskStatus.Succeeded,
                OperationCanceledException                            => PoTaskStatus.Canceled,
                _                                                     => PoTaskStatus.Faulted,
            };
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetResult(short token)
        {
            ValidateToken(token);
            if (completedCount == 0) throw new InvalidOperationException("Operation is not completed");
            if (error          == null) return result;

            hasUnhandledError = false;
            throw error switch
            {
                OperationCanceledException oce => oce,
                Exception ex => ex,
                _ => new InvalidOperationException($"Invalid error type {error.GetType().Name}")
            };
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnCompleted(Action<object> continuation, object state, short token)
        {
            if (continuation is null) throw new ArgumentNullException(nameof(continuation));

            ValidateToken(token);

            object oldContinuation = this.continuation;
            if (oldContinuation == null)
            {
                continuationState = state;
                oldContinuation   = Interlocked.CompareExchange(ref this.continuation, continuation, null);
            }

            if (oldContinuation != null)
            {
                if (!ReferenceEquals(oldContinuation, Sentinel))
                    throw new InvalidOperationException("Continuation registered...");
                continuation(state);
            }
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ValidateToken(short token)
        {
            if (version != token) throw new InvalidOperationException("Invalid version != token");
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Continue()
        {
            bool isNotNull  = continuation                                                  != null;
            bool isSentinel = Interlocked.CompareExchange(ref continuation, Sentinel, null) != null;
            if (isNotNull || isSentinel) continuation(continuationState);
        }
    }
}