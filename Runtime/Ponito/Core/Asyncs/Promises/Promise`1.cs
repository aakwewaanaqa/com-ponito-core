using System;
using Ponito.Core.Asyncs.Tasks.Movables;

namespace Ponito.Core.Asyncs.Promises
{
    /// 會有
    /// <see cref="T" />
    /// 作為完成回傳值的
    /// <see cref="Promise" />
    public partial class Promise<T> : Promise, IDisposable
    {
        /// <summary>
        ///     執行後的結果
        /// </summary>
        private T result;

        /// <summary>
        ///     取得執行後的結果，如果有例外會擲回 <see cref="Promise.Ex" />
        /// </summary>
        public T Result
        {
            get
            {
                Ex?.TryThrow();
                return result;
            }
            set => result = value;
        }

        /// <summary>
        ///     上一個產生這個承諾的方式，遇到 <see cref="TryAgain" /> 會需要再產生一個可執行的
        ///     <see cref="Promise{T}" />
        ///     。
        /// </summary>
        private new Func<Promise<T>> Factory { get; set; }

        /// <summary>
        ///     丟棄參考以利垃圾收集 <see cref="GC" />
        /// </summary>
        public new void Dispose()
        {
            Factory = null;
            Result  = default;
        }

        /// <summary>
        ///     自動轉換 <see cref="Promise{T}" /> 為 <see cref="T" />
        ///     ，如果有例外會擲回 <see cref="Promise.Ex" />
        /// </summary>
        /// <param name="p">自己</param>
        /// <returns>
        ///     <see cref="Result" />
        /// </returns>
        public static implicit operator T(Promise<T> p)
        {
            return p.Result;
        }

        /// <summary>
        ///     取得等待者，由 C# await 時自動呼叫
        /// </summary>
        /// <returns>
        ///     <see cref="Awaiter" />
        /// </returns>
        public new Awaiter GetAwaiter()
        {
            return new Awaiter(this);
        }

        /// <summary>
        ///     <see cref="Promise{T}" /> 的等待者
        /// </summary>
        public new class Awaiter : MovableBase<T>
        {
            /// <summary>
            ///     建構一個 <see cref="Promise{T}" /> 的等待者
            /// </summary>
            /// <param name="p">
            ///     <see cref="promise" />
            /// </param>
            internal Awaiter(Promise<T> p)
            {
                promise = p;
                promise?.Ex?.TryThrow();
            }

            /// <summary>
            ///     等待來源
            /// </summary>
            private Promise<T> promise { get; }

            /// <summary>
            ///     取得 <see cref="promise" /> 的執行結果，
            ///     如果有例外會擲回 <see cref="Promise.Ex" />
            /// </summary>
            /// <returns></returns>
            public override T GetResult()
            {
                return promise.Result;
            }

            /// <summary>
            ///     往前推移或是等候 <see cref="Promise{T}" />
            ///     如果有例外也會擲回 <see cref="Promise.Ex" />
            ///     ，這個函式通常由 <see cref="MovableRunner" /> 執行
            /// </summary>
            /// <returns>是否還需要繼續 <see cref="MoveNext" /></returns>
            /// <exception cref="ArgumentOutOfRangeException"><see cref="Promise.State" /> 沒有定義</exception>
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