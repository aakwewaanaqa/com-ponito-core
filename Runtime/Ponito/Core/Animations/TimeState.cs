using System;
using System.Threading;
using Ponito.Core.Asyncs.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ponito.Core.Animations
{
    /// <summary>
    ///     A well constructed class to store and play a round-animation-state-situation.
    ///     Including cancel previous play, and storing previous time of the state.
    /// </summary>
    [Serializable]
    public class TimeState
    {
        /// <summary>
        ///     <see cref="percent"/> of current state
        /// </summary>
        [SerializeField] public float percent;

        /// <summary>
        ///     Source of previous play to <see cref="CancellationTokenSource.Cancel()"/>.
        /// </summary>
        private CancellationTokenSource cts { get; set; }

        /// <summary>
        ///     Plays <see cref="percent"/> from 0f to 1f
        /// </summary>
        /// <param name="duration">duration of this play</param>
        /// <param name="onAnimate">the action to invoke passing <see cref="percent"/></param>
        public async PoTask Play(float duration, Action<float> onAnimate)
        {
            await Play(0f, 1f, duration, onAnimate);
        }

        /// <summary>
        ///     Plays <see cref="percent"/> from <see cref="Time"/> to <see cref="to"/>
        /// </summary>
        /// <param name="to">the target <see cref="percent"/></param>
        /// <param name="duration">duration of this play</param>
        /// <param name="onAnimate">the action to invoke passing <see cref="percent"/></param>
        public async PoTask Play(float to, float duration, Action<float> onAnimate)
        {
            await Play(percent, to, duration, onAnimate);
        }

        /// <summary>
        ///     Plays <see cref="percent"/> from <see cref="from"/> to <see cref="to"/>
        /// </summary>
        /// <param name="from">the starts of the <see cref="percent"/></param>
        /// <param name="to">the target <see cref="percent"/></param>
        /// <param name="duration">duration of this play</param>
        /// <param name="onAnimate">the action to invoke passing <see cref="percent"/></param>
        public async PoTask Play(float from, float to, float duration, Action<float> onAnimate)
        {
            percent = from;

            cts?.Cancel();                       // if it plays before, cancel it.
            cts = new CancellationTokenSource(); // make a new cancel source.

            var isGoingUp = to > from; // is adding time or subtracting
            var ct        = cts.Token; // stores the source's token for identification
            var isInvoked = false;
            do
            {
                await PoTask.Yield();           // waits a frame here, so that next frame can be cancelled at from
                if (ct.IsCancellationRequested) // detects cancellation
                {
                    if (!isInvoked) // if it was invoked before, means it was cancelled in middle of the play
                        onAnimate?.Invoke(percent); // exports percent value
                    return; // stops all of this
                }

                var deltaTime = Time.deltaTime;
                var delta     = isGoingUp ? deltaTime / duration : -deltaTime / duration;

                if (Mathf.Abs(to - percent) <= Mathf.Abs(delta)) percent =  to;    // avoids Time bigger than to
                else percent                                             += delta; // advanced Time 

                isInvoked = true;           // marks invoked
                onAnimate?.Invoke(percent); // exports percent value
            } while (percent != to);
        }
    }
}