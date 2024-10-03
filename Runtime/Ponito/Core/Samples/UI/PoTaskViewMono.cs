using System;
using System.Threading;
using Ponito.Core.Asyncs.Tasks;
using Ponito.Core.Extensions;
using UnityEngine;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace Ponito.Core.Samples.UI
{
    /// <summary>
    ///     本身就是 <see cref="PoTaskView" /> 基於要序列化，就繼承了 <see cref="MonoBehaviour" />
    /// </summary>
    [Obsolete("請盡量不用繼承的方式實作，可以用 PoTaskViewUnit 來組合實作😀。")]
    public abstract class PoTaskViewMono : MonoBehaviour, PoTaskView
    {
        /// <summary>
        ///     用來取消自身矛盾的動畫狀態，但又能基於外部的取消令牌進行取消
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        protected CancellationTokenSource innerCts;

        /// <inheritdoc />
        public virtual async PoTask Show(object state, CancellationToken ct = default)
        {
            if (ct.IsCancellationRequested) return;
            innerCts = innerCts.LinkAfterCancel(ct, out var innerCt);
            await InnerShow(state, innerCt);
            State = ViewState.Showing;
        }

        /// <inheritdoc />
        public virtual async PoTask Hide(object state, CancellationToken ct = default)
        {
            if (ct.IsCancellationRequested) return;
            innerCts = innerCts.LinkAfterCancel(ct, out var innerCt);
            await InnerHide(state, innerCt);
            State = ViewState.Hidden;
        }

        /// <inheritdoc />
        public virtual ViewState State { get; protected set; }

        /// <summary>
        ///     複寫這個方法來實現顯示的邏輯，比複寫 <see cref="Show" /> 更加不易漏掉重要的邏輯檢查
        /// </summary>
        protected abstract PoTask InnerShow(object args, CancellationToken ct = default);

        /// <summary>
        ///     複寫這個方法來實現隱藏的邏輯，比複寫 <see cref="Hide" /> 更加不易漏掉重要的邏輯檢查
        /// </summary>
        protected abstract PoTask InnerHide(object args, CancellationToken ct = default);
    }
}