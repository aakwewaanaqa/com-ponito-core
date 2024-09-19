using System.Linq;
using UnityEngine;

namespace Ponito.Core.Ease.SpecialEases
{
    /// <summary>
    ///     從 DOTween's Punch 參考的動畫，寫法有再做簡化。
    ///     要記得 Punch 是 0 -> magnitude -> 0 的過程，所以 magnitude 不能是 0。
    /// </summary>
    public readonly struct Punch
    {
        /// <summary>
        ///     這個值是恆定的
        /// </summary>
        private readonly float[] returnValues;

        /// <summary>
        ///     這個時間值是持續累積地
        /// </summary>
        private readonly float[] returnDurations;

        public Punch(
            float magnitude,
            float duration,
            float vibrato,
            float elasticity)
        {
            elasticity = Mathf.Clamp01(elasticity);

            {
                var length        = Mathf.Max(2, (int)(vibrato * duration));
                var durations     = new float[length];
                var totalDuration = 0f;
                for (var i = 0; i < length; i++)
                {
                    var t               = (float)(i + 1) / length;
                    var segmentDuration = duration       * t;
                    totalDuration += segmentDuration;
                    durations[i]  =  segmentDuration;
                }

                var durationScale = duration / totalDuration;

                for (var i = 0; i < length; i++) { durations[i] *= durationScale; }

                returnDurations = durations;
            }

            {
                var length   = Mathf.Max(2, (int)(vibrato * duration));
                var values   = new float[length];
                var velocity = magnitude;
                for (var i = 0; i < length; i++)
                {
                    if (i >= length - 1)
                    {
                        values[i] = 0f;
                        continue;
                    }

                    var isEven       = i % 2 == 0;
                    var evenVelocity = Mathf.Clamp(velocity, 0f, magnitude);
                    var oddVelocity  = -Mathf.Clamp(velocity, 0f, magnitude * elasticity);
                    values[i] =  i == 0 ? velocity : isEven ? evenVelocity : oddVelocity;
                    velocity  -= magnitude / length;
                }

                returnValues = values;
            }
        }

        public EaseFunction GetEaseFunction()
        {
            var values       = returnValues;
            var durations    = returnDurations;
            var sineFunction = EasingEquations.GetFunction(EaseType.InOutSine);
            return t =>
            {
                var index = durations.TakeWhile(d => !(d > t)).Count();
                if (index >= values.Length) return 0f;

                var fd = index - 1 < 0f ? 0f : durations[index - 1];
                var td = durations[index];
                var fv = index - 1 < 0f ? 0f : values[index - 1];
                var tv = values[index];

                var process = (t - fd) / (td - fd);
                var lerp    = sineFunction(process);

                return Mathf.Lerp(fv, tv, lerp);
            };
        }
    }
}