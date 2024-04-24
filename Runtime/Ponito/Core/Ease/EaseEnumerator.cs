using System.Threading.Tasks;
using Ponito.Core.Asyncronized;
using UnityEngine;

namespace Ponito.Core.Ease
{
    internal partial class EaseEnumerator<T> : EaseAnimation
    {
        private readonly float        duration;
        private readonly T            end;
        private readonly T            start;
        private          EaseFunction easeFunction;
        private          EaseType     easeType;

        private Lerper<T> lerper;
        private Setter<T> setter;
        private float     time;

        public EaseEnumerator(
            T start,
            T end,
            Setter<T> setter,
            Lerper<T> lerper,
            float duration,
            EaseType easeType = EaseType.InSine)
        {
            this.start    = start;
            this.end      = end;
            this.lerper   = lerper;
            this.setter   = setter;
            this.duration = duration;
            this.easeType = easeType;

            time         = 0f;
            easeFunction = EasingEquations.GetFunction(easeType);
            IsPlaying    = true;
        }

        public float Progress => time / duration;

        public bool IsPlaying { get; private set; }

        public void Dispose()
        {
            lerper       = null;
            setter       = null;
            easeFunction = null;
        }

        public void Kill()
        {
            IsPlaying = false;
        }

        public async void Play()
        {
            while (IsPlaying && time < duration)
            {
                setter(lerper(start, end, easeFunction(time / duration)));
                time += Time.deltaTime;
                await Task.Delay(1);
            }

            IsPlaying = false;
            setter?.Invoke(end); // Weird bug when animation disposed...

            Dispose();
        }
    }
}