using System;
using System.Text;
using System.Threading;
using Ponito.Core.Asyncs.Compilations;
using Ponito.Core.Asyncs.Extensions;
using Ponito.Core.Asyncs.Interfaces;
using Ponito.Core.DebugHelper;
using UnityEngine;

namespace Ponito.Core.Asyncs.Tasks.Movables
{
    /// <summary>
    ///     <see cref="Movable"/> 異步來源基礎 <see cref="MovableRunner"/>
    /// </summary>
    public abstract class MovableBase : Movable
    {
        /// <inheritdoc />
        public virtual Exception Ex { get; set; }

        /// <summary>
        ///     儲存內部取消用旗幟，可以在後續動作取消這個異步任務來源 <see cref="Movable"/>
        ///     會擲出 <see cref="OperationCanceledException"/>.
        /// </summary>
        protected virtual CancellationToken Ct { get; set; }

        /// <summary>
        ///     後續的執行
        /// </summary>
        private Action continuation { get; set; }

        /// <inheritdoc />
        public virtual bool IsCompleted { get; protected set; }

        /// <summary>
        ///     C# 編譯器會自動使用這個函數在 <see cref="GetAwaiter"/> 之後
        ///     把後續的執行被變成 <see cref="continuation"/>
        /// </summary>
        /// <param name="continuation"> C# 編譯器會後續的執行被變成 <see cref="continuation"/> </param>
        public virtual void OnCompleted(Action continuation)
        {
            if (this.continuation != null)
            {
                Debug.LogError("continuation overflown");
                return;
            }

            this.continuation = continuation;
            MovableRunner.Singleton.Enqueue(this);
        }

        /// <summary>
        ///     C# 編譯器會自動使用這個函數在完成工作之後
        /// </summary>
        public virtual void GetResult()
        {
            if (Ex != null) throw Ex;
            Dispose();
        }

        /// <inheritdoc />
        public abstract bool MoveNext();

        /// <summary>
        ///     標註捨棄
        /// </summary>
        public virtual void Dispose()
        {
            continuation = null; // 捨棄後續程式碼，不然很佔記憶體空間
        }

        /// <inheritdoc />
        public virtual Movable GetAwaiter()
        {
            return this;
        }

        /// <summary>
        ///     先完成後續然後為 <see cref="MovableRunner"/> 標註完成 
        /// </summary>
        /// <returns>然後永遠傳為 false</returns>
        protected bool ContinueMoveNext()
        {
            continuation?.Invoke(); // 這裡的邏輯設計真的很難，要先往下走才能完成後續
            IsCompleted = true;     // 然後為 <see cref="MovableRunner"/> 標註完成
            return false;
        }

        /// <summary>
        ///     檢查 <see cref="ct"/> 是否取消，取消的話擲回 <see cref="OperationCanceledException"/>.
        /// </summary>
        /// <param name="ct"><see cref="CancellationToken"/></param>
        /// <exception cref="OperationCanceledException"><see cref="ct"/> 已取消</exception>
        protected void ValidateCancel(CancellationToken ct)
        {
            if (!ct.IsCancellationRequested) return;
            
            var str = new StringBuilder("cancel ")
               .Append(GetType().Name.Colorize(DebugColors.CLASS_COLOR));
            
            throw new OperationCanceledException(str.ToString());
        }
    }

    public abstract class MovableBase<T> : MovableBase, Movable<T>
    {
        /// <summary>
        ///     取得結果！ C# 編譯器會自動使用這個屬性
        /// </summary>
        public new abstract T GetResult();

        /// <summary>
        ///     取得等待者。
        /// </summary>
        /// <returns>等待者</returns>
        public new Movable<T> GetAwaiter()
        {
            return this;
        }
    }
}