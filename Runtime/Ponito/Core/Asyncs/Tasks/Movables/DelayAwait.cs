using System.Threading;
using Ponito.Core.Asyncs.Interfaces;
using UnityEngine;

namespace Ponito.Core.Asyncs.Tasks.Movables
{
    /// <summary>
    ///     Delays a certain amount of time.
    /// </summary>
    internal class DelayAwait : MovableBase, ProgressMovable
    {
        /// <summary>
        ///     By <see cref="milliseconds" />
        /// </summary>
        public DelayAwait(int milliseconds, CancellationToken ct = default)
        {
            Ct         = ct;
            seconds    = milliseconds / 1000f;
            targetTime = Time.time + seconds;
        }

        /// <summary>
        ///     By <see cref="seconds" />
        /// </summary>
        public DelayAwait(float seconds, CancellationToken ct = default)
        {
            Ct           = ct;
            this.seconds = seconds;
            targetTime   = Time.time + this.seconds;
        }

        /// <summary>
        ///     目標 <see cref="Time" />.<see cref="Time.time" />
        /// </summary>
        private float targetTime { get; }

        /// <summary>
        ///     要等候的秒數
        /// </summary>
        private float seconds { get; }

        /// <inheritdoc />
        public override bool MoveNext()
        {
            if (Ct.IsCancellationRequested) return false;
            if (IsCompleted) return false;

            var timeIsUp = Time.time >= targetTime;
            if (!timeIsUp) return true;

            return ContinueMoveNext();
        }

        /// <summary>
        ///     回報等候的進度
        /// </summary>
        public float Progress => 1f - (targetTime - Time.time) / seconds;
    }
}