using System;
using System.Runtime.CompilerServices;
using Ponito.Core.Asyncs.Compilations;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.Asyncs.Tasks.Movables;

namespace Ponito.Core.Asyncs.Tasks
{
    /// <summary>
    ///     使用 <see cref="Awaiter"/> 並仰賴 <see cref="MovableRunner"/> 同步地執行其程式碼。
    ///     但這個是帶有回傳值的任務！
    /// </summary>
    /// <typeparam name="T">回傳值類型</typeparam>
    /// <example>
    ///     public async PoTask&lt;int&gt; Boo()
    ///     {
    ///         await PoTask.Yield();
    ///         return 5;    
    ///     }
    /// </example>
    [AsyncMethodBuilder(typeof(PoTaskBuilder<>))]
    public partial class PoTask<T> : PoTask
    {
        /// <summary>
        ///     異步任務的執行結果
        /// </summary>
        internal T result;

        /// <summary>
        ///     <see cref="PoTask{T}"/>
        /// </summary>
        /// <param name="source">
        ///     任務來源 <see cref="Movable"/>，
        /// </param>
        public PoTask(INotifyCompletion source = null) : base(source)
        {
        }

        /// <summary>
        ///     取得等待者 <see cref="Awaiter"/> 以利呼叫 <see cref="MovableBase.MoveNext()"/>
        /// </summary>
        /// <returns>可以用來表示 <see cref="PoTask{T}"/> 當前狀態的等待者。</returns>
        public new Awaiter GetAwaiter()
        {
            return new(this);
        }

        /// <summary>
        ///     可以用來表示<see cref="PoTask{T}"/>當前狀態的等待者。
        /// </summary>
        public new class Awaiter : MovableBase<T>
        {
            /// <summary>
            ///     <see cref="Awaiter"/>
            /// </summary>
            /// <param name="task">要被追蹤的<see cref="PoTask{T}"/></param>
            public Awaiter(PoTask<T> task)
            {
                this.task = task;
            }

            /// <summary>
            ///     要被追蹤的 <see cref="PoTask{T}"/>
            /// </summary>
            private PoTask<T> task { get; }

            /// <summary>
            ///     取的執行結果！
            /// </summary>
            /// <returns>執行結果</returns>
            /// <exception cref="Ex">當執行結果有例外時擲回</exception>
            public override T GetResult()
            {
                if (Ex != null) throw Ex;
                Dispose();
                return task.result;
            }

            /// <inheritdoc />
            public override Exception Ex
            {
                get => task.Ex;
                set => task.Ex = value;
            }
            
            private bool HasSource => task?.Source != null;
            
            /// <inheritdoc />
            public override bool MoveNext()
            {
                if (IsCompleted) return false;
                if (HasSource) return true;
                continuation?.Invoke();
                return IsCompleted = HasSource;
            }
        }
    }
}