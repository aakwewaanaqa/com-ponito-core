using System;
using System.Collections;
using Ponito.Core.Asyncs.Tasks;
using Ponito.Core.Asyncs.Tasks.Movables;
using UnityEngine;

namespace Ponito.Core.Asyncs.Promises
{
    /// <summary>
    ///     超級方便利用的承諾類別，用來異步執行任務但可以選擇不等待的方式
    /// </summary>
    public partial class Promise
    {
        public float            Progress { get; set; }
        
        /// <summary>
        ///     檢測現在這個承諾的狀態
        /// </summary>
        public PromiseState     State    { get; set; } = PromiseState.Doing;
        
        /// <summary>
        ///     多元儲存例外狀態的地方 <see cref="PromiseException"/>
        /// </summary>
        public PromiseException Ex    { get; set; }

        /// <summary>
        ///     The last called factory <see cref="Func{TResult}"/> that creates this <see cref="Promise"/>
        /// </summary>
        private Func<Promise> factory { get; set; }

        public bool IsDoing => State is PromiseState.Doing;

        public IEnumerator AsCoroutine()
        {
            while (State is not PromiseState.Done)
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
                promise = p;
                promise?.Ex?.TryThrow();
            }

            public override bool MoveNext()
            {
                promise?.Ex?.TryThrow();
                if (IsCompleted) return false;
                return promise.State switch
                {
                    PromiseState.Doing => true,
                    PromiseState.Done  => ContinueMoveNext(),
                    _                  => throw new ArgumentOutOfRangeException()
                };
            }
        }
    }
}