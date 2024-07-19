using System.Threading;

namespace Ponito.Core.Asyncs.Tasks.Movables
{
    /// <summary>
    ///     Yields a frame.
    /// </summary>
    internal sealed class YieldAwait : MovableBase
    {
        public YieldAwait(CancellationToken ct = default)
        {
            Ct = ct;
        }

        /// <inheritdoc />
        public override bool MoveNext()
        {
            if (Ct.IsCancellationRequested) return false;
            return !IsCompleted && ContinueMoveNext();
        }
    }
}