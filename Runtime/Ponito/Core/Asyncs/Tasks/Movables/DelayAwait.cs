﻿using UnityEngine;

namespace Ponito.Core.Asyncs.Tasks.Movables
{
    /// <summary>
    ///     Delays a certain amount of time.
    /// </summary>
    internal class DelayAwait : MovableBase
    {
        /// <summary>
        ///     By <see cref="milliseconds" />
        /// </summary>
        public DelayAwait(int milliseconds)
        {
            targetTime = Time.time + milliseconds / 1000f;
        }

        /// <summary>
        ///     By <see cref="seconds" />
        /// </summary>
        public DelayAwait(float seconds)
        {
            targetTime = Time.time + seconds;
        }

        /// <summary>
        ///     Target <see cref="Time" />.<see cref="Time.time" />
        /// </summary>
        private float targetTime { get; }

        /// <inheritdoc />
        public override bool MoveNext()
        {
            if (IsCompleted) return false;

            var timeIsUp = Time.time >= targetTime;
            if (!timeIsUp) return true;

            return ContinueMoveNext();
        }
    }
}