namespace Ponito.Core.Asyncs.Tasks.Movables
{
    /// <summary>
    ///     Yields a frame.
    /// </summary>
    internal class YieldAwait : MovableBase
    {
        /// <inheritdoc />
        public override bool MoveNext()
        {
            if (IsCompleted) return false;
            return FinishMoveNext();
        }
    }
}