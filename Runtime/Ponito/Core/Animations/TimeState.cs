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
        public async PoTask Play(float duration, Action<float> onAnimate, CancellationToken ct = default)
        {
            await Play(0f, 1f, duration, onAnimate, ct);
        }

        /// <summary>
        ///     Plays <see cref="percent"/> from <see cref="Time"/> to <see cref="to"/>
        /// </summary>
        /// <param name="to">the target <see cref="percent"/></param>
        /// <param name="duration">duration of this play</param>
        /// <param name="onAnimate">the action to invoke passing <see cref="percent"/></param>
        public async PoTask Play(float to, float duration, Action<float> onAnimate, CancellationToken ct = default)
        {
            await Play(percent, to, duration, onAnimate, ct);
        }

        /// <summary>
        ///     Plays <see cref="percent"/> from <see cref="from"/> to <see cref="to"/>
        /// </summary>
        /// <param name="from">the starts of the <see cref="percent"/></param>
        /// <param name="to">the target <see cref="percent"/></param>
        /// <param name="duration">duration of this play</param>
        /// <param name="onAnimate">the action to invoke passing <see cref="percent"/></param>
        public async PoTask Play(
            float from,
            float to,
            float duration,
            Action<float> onAnimate,
            CancellationToken ct = default)
        {
            percent = from;

            cts?.Cancel();
            cts = new CancellationTokenSource();

            var isGoingUp = to > from;           // 檢查是否是往上
            ct = ct == default ? cts.Token : ct; // 如果沒有傳入 CancellationToken，就使用自己的
            var isInvoked = false;
            do
            {
                await Controls.Yield();           // 等一幀所以被取消的時候還是在 from
                if (ct.IsCancellationRequested) // 如果被取消
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