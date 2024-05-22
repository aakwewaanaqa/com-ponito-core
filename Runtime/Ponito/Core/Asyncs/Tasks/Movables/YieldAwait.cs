using System;
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
            ValidateCancel(Ct);
        }

        /// <inheritdoc />
        public override bool MoveNext()
        {
            ValidateCancel(Ct);
            if (IsCompleted) return false;
            return FinishMoveNext();
        }
    }
}