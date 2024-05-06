using System;
using System.Threading;
using Ponito.Core.Asyncs.Compilations;
using Ponito.Core.Asyncs.Extensions;
using Ponito.Core.Asyncs.Tasks.Sources;
using UnityEngine;

namespace Ponito.Core.Asyncs.Tasks
{
    public readonly partial struct PoTask
    {
        public static PoTask Delay(int milliseconds)
        {
            var source = DelayPromise.Create(TimeSpan.FromMilliseconds(milliseconds),
                                             PlayerLoopTiming.Update,
                                             default,
                                             false,
                                             out short token);
            return new PoTask(source, token);
        }

        private sealed class DelayPromise : PoTaskSource, PlayerLoopItem, TaskPoolNode<DelayPromise>
        {
            private        DelayPromise                       nextNode;
            private        PoTaskCompletionSourceCore<object> core;
            private static TaskPool<DelayPromise>             pool;
            private        CancellationToken                  ct;
            private        CancellationTokenRegistration      ctr;
            private        bool                               cancelImmediately;
            private        int                                initialFrame;
            private        float                              delayTimeSpan;
            private        float                              elapsed;

            public ref DelayPromise NextNode => ref nextNode;

            public static PoTaskSource Create(
                TimeSpan delayTimeSpan,
                PlayerLoopTiming timing,
                CancellationToken ct,
                bool cancelImmediately,
                out short token)

            {
                if (ct.IsCancellationRequested)
                {
                    return AutoResetPoTaskCompletionSource.CreateFromCanceled(ct, out token);
                }

                if (!pool.TryPop(out var result))
                {
                    result = new DelayPromise();
                }

                result.elapsed           = 0.0f;
                result.delayTimeSpan     = (float)delayTimeSpan.TotalSeconds;
                result.ct                = ct;
                result.initialFrame      = PlayerLoopHelper.IsMainThread ? Time.frameCount : -1;
                result.cancelImmediately = cancelImmediately;

                if (cancelImmediately && ct.CanBeCanceled)
                {
                    result.ctr = ct.RegisterWithoutCaptureExecutionContext(
                        state =>
                        {
                            var promise = (DelayPromise)state;
                            promise.core.TrySetCanceled(promise.ct);
                        }, result);
                }

                // TODO: Track source
                // TaskTracker.TrackActiveTask(result, 3);

                PlayerLoopHelper.AddAction(timing, result);

                token = result.core.Version;
                return result;
            }

            public bool MoveNext()
            {
                if (ct.IsCancellationRequested)
                {
                    core.TrySetCanceled(ct);
                    return false;
                }

                if (elapsed == 0.0f)
                {
                    if (initialFrame == Time.frameCount)
                    {
                        return true;
                    }
                }

                elapsed += Time.deltaTime;
                if (elapsed >= delayTimeSpan)
                {
                    core.TrySetResult(null);
                    return false;
                }

                return true;
            }

            public PoTaskStatus GetStatus(short token)
            {
                return core.GetStatus(token);
            }

            public void OnCompleted(Action<object> continuation, object state, short token)
            {
                core.OnCompleted(continuation, state, token);
            }

            public void GetResult(short token)
            {
                try
                {
                    core.GetResult(token);
                }
                finally
                {
                    if (!cancelImmediately && !ct.IsCancellationRequested) TryReturn();
                }
            }

            private bool TryReturn()
            {
                // TODO: Track source
                // TaskTracker.RemoveTracking(this);
                core.Reset();
                ctr.Dispose();
                delayTimeSpan     = default;
                elapsed           = default;
                ct                = default;
                cancelImmediately = default;
                return pool.TryPush(this);
            }
        }
    }
}