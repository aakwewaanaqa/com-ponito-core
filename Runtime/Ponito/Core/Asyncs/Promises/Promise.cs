using System;
using System.Collections;
using Ponito.Core.Asyncs.Tasks;
using Ponito.Core.Asyncs.Tasks.Movables;
using UnityEngine;

namespace Ponito.Core.Asyncs.Promises
{
    /// <summary>
    ///     Super useful class to chain-run-delegates and stores the running result.
    /// </summary>
    public partial class Promise
    {
        public    float        Progress { get; set; }
        public    PromiseState State    { get; set; } = PromiseState.Doing;
        public    object       Error    { get; set; }

        protected Func<Promise, PoTask> caller;

        public bool IsDoing => State is PromiseState.Doing;
        
        protected static void ValidateThrow(Promise p)
        {
            if (p.State is not PromiseState.Failed) return;

            if (p.Error is Exception ex) throw ex;
            throw new Exception(p.Error.ToString());
        }

        public IEnumerator AsCoroutine()
        {
            while (State is PromiseState.Doing)
            {
                var endOfFrame = new WaitForEndOfFrame();
                yield return endOfFrame;
            }
        }
        
        public Awaiter GetAwaiter() => new(this);

        public class Awaiter : MovableBase
        {
            private Promise promise { get; }

            internal Awaiter(Promise p)
            {
                ValidateThrow(p);
                promise = p;
            }

            public override bool MoveNext()
            {
                ValidateThrow(promise);
                if (IsCompleted) return false;
                return promise.State switch
                {
                    PromiseState.Doing => true,
                    PromiseState.Done  => FinishMoveNext(),
                    _                  => throw new ArgumentOutOfRangeException()
                };
            }
        }
    }
}