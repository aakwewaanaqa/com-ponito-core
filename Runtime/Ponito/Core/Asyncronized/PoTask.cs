using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Ponito.Core.Asyncronized.Interfaces;
using Ponito.Core.Asyncronized.Runner;
using Ponito.Core.DebugHelper;

namespace Ponito.Core.Asyncronized
{
    [AsyncMethodBuilder(typeof(PoTaskBuilder))]
    public struct PoTask : Awaitable
    {
        private Completable awaiter { get; set; }

        public bool IsCompleted
        {
            get
            {
                // typeof(PoTask).Get(nameof(IsCompleted));
                var flag = awaiter is null || awaiter.IsCompleted;
                return flag;
            }
        }

        public void OnCompleted([NotNull] Action continuation)
        {
            continuation();
        }

        public void GetResult()
        {
        }

        public PoTask GetAwaiter()
        {
            return this;
        }

        public static void Create<TAwaiter>(
            IAsyncStateMachine machine,
            ref PoTask task,
            ref TAwaiter completable,
            Action continuation)
            where TAwaiter : Completable
        {
            // machine.GetType().F(nameof(Create));
            
            task.awaiter = completable;
            PoTaskRunner.Instance.Add(completable, continuation);
        }
    }
}