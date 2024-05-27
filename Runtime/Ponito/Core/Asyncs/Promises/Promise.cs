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
        /// <summary>
        ///     檢測現在這個承諾的進度，可以手動設置的
        /// </summary>
        public float Progress { get; set; }

        /// <summary>
        ///     檢測現在這個承諾的狀態，可以手動設置的
        /// </summary>
        public PromiseState State { get; set; } = PromiseState.Doing;

        /// <summary>
        ///     多元儲存例外狀態的地方 <see cref="PromiseException"/>，可以手動設置的，但會直接切斷執行喔！
        /// </summary>
        public PromiseException Ex { get; set; }

        /// <summary>
        ///     上一個產生這個承諾的方式，遇到 <see cref="TryAgain"/> 會需要再產生一個可執行的 <see cref="Promise"/>。
        /// </summary>
        private Func<Promise> factory { get; set; }

        /// <summary>
        ///     是不是 <see cref="PromiseState.Doing"/>
        /// </summary>
        public bool IsDoing => State is PromiseState.Doing;

        /// <summary>
        ///     以 <see cref="IEnumerator"/> <see cref="Coroutine"/> 的形式回傳
        /// </summary>
        /// <returns><see cref="IEnumerator"/> <see cref="Coroutine"/></returns>
        public IEnumerator AsCoroutine()
        {
            while (State is not PromiseState.Done)
            {
                var endOfFrame = new WaitForEndOfFrame();
                yield return endOfFrame;
            }
        }

        /// <summary>
        ///     取得等待者 <see cref="Awaiter"/> 以利呼叫 <see cref="MovableBase.MoveNext()"/>
        ///     await 後 C# 會自己呼叫。
        /// </summary>
        /// <returns>可以用來表示 <see cref="Promise"/> 當前狀態的等待者。</returns>
        public Awaiter GetAwaiter() => new(this);

        /// <summary>
        ///     可以用來表示 <see cref="Promise"/> 當前狀態的等待者。
        /// </summary>
        public class Awaiter : MovableBase
        {
            /// <summary>
            ///     儲存等待的目標
            /// </summary>
            private Promise promise { get; }

            /// <summary>
            ///     <see cref="Awaiter"/>
            /// </summary>
            /// <param name="p"><see cref="promise"/></param>
            internal Awaiter(Promise p)
            {
                promise = p;
                promise?.Ex?.TryThrow();
            }

            /// <inheritdoc />
            public override bool MoveNext()
            {
                promise?.Ex?.TryThrow();
                
                if (IsCompleted) return false;
                
                return promise!.State switch
                {
                    PromiseState.Doing => true,
                    PromiseState.Done  => ContinueMoveNext(),
                    _                  => throw new ArgumentOutOfRangeException($"未知的 {typeof(PromiseState)}")
                };
            }
        }
    }
}