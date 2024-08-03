using System.Threading;
using Ponito.Core.Asyncs.Tasks;
using UnityEngine;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace Ponito.Core.Samples.UI
{
    /// <summary>
    ///     本身就是 <see cref="PoTaskView"/> 基於要序列化，就繼承了 <see cref="MonoBehaviour"/>
    /// </summary>
    public class PoTaskViewMono : MonoBehaviour, PoTaskView
    {
        /// <inheritdoc />
        public virtual async PoTask Show(object args, CancellationToken ct = default)
        {
            if (ct.IsCancellationRequested) return;
            gameObject.SetActive(true);
            State = ViewState.Showing;
        }

        /// <inheritdoc />
        public virtual async PoTask Hide(object args, CancellationToken ct = default)
        {
            if (ct.IsCancellationRequested) return;
            gameObject.SetActive(false);
            State = ViewState.Hidden;
        }

        /// <inheritdoc />
        public virtual ViewState State { get; protected set; }
    }
}