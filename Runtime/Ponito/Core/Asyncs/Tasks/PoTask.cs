using System;
using System.Runtime.CompilerServices;
using Ponito.Core.Asyncs.Compilations;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.Asyncs.Tasks.Movables;

namespace Ponito.Core.Asyncs.Tasks
{
    /// <summary>
    ///     使用 <see cref="Awaiter"/> 並仰賴 <see cref="MovableRunner"/> 同步地執行其程式碼。
    /// </summary>
    /// <remarks>
    ///     參考 <see cref="PoTaskBuilder"/> 看看底層怎麼運作的。<br/>
    ///     參考 <see cref="Promises.Promise"/> 看看怎麼處理連環呼叫！
    /// </remarks>
    /// <example>
    ///     public async PoTask Boo()
    ///     {
    ///         await PoTask.Yield();
    ///     }
    /// </example>
    [AsyncMethodBuilder(typeof(PoTaskBuilder))]
    public partial class PoTask : PoTaskBase
    {
        /// <summary>
        ///     <see cref="PoTask"/>
        /// </summary>
        /// <param name="source">
        ///     任務來源 <see cref="Movable"/>，
        /// </param>
        public PoTask(Movable source = null) : base(source)
        {
        }

        /// <summary>
        ///     取得等待者 <see cref="Awaiter"/> 以利呼叫 <see cref="MovableBase.MoveNext()"/>
        /// </summary>
        /// <returns>可以用來表示<see cref="PoTask"/>當前狀態的等待者。</returns>
        public Awaiter GetAwaiter()
        {
            return new Awaiter(this);
        }
        
        /// <summary>
        ///     可以用來表示<see cref="PoTask"/>當前狀態的等待者。
        /// </summary>
        public class Awaiter : MovableBase, IDisposable
        {
            /// <summary>
            ///     <see cref="Awaiter"/>
            /// </summary>
            /// <param name="task">要被追蹤的<see cref="PoTask"/></param>
            public Awaiter(in PoTask task)
            {
                this.task = task;
            }

            /// <summary>
            ///     要被追蹤的 <see cref="PoTask"/>
            /// </summary>
            private PoTask task { get; }

            /// <inheritdoc />
            public override Exception Ex
            {
                get => task.Exception;
                set => task.Exception = value;
            }

            /// <inheritdoc />
            public override bool MoveNext()
            {
                if (IsCompleted) return false;
                if (!(task.Source?.IsCompleted ?? true)) return true;
                return ContinueMoveNext();
            }
        }
    }
}