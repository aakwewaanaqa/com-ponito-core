using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Ponito.Core.Ease
{
    public partial class EaseEnumerator<T> : EaseAnimation
    {
        private readonly float        duration;
        private readonly T            end;
        private readonly T            start;
        private readonly UniTask      task;
        private          EaseFunction easeFunction;
        private          EaseType     easeType;
        private          bool         isPlaying;

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
            time          = 0f;
            easeFunction  = EasingEquations.GetFunction(easeType);

            isPlaying = true;
            task      = EnumerateAsync();
        }

        public void Dispose()
        {
            lerper       = null;
            setter       = null;
            easeFunction = null;
        }

        public void Kill()
        {
            isPlaying = false;
        }

        public bool IsPlaying()
        {
            return isPlaying;
        }

        public float Progress => time / duration;

        public UniTask.Awaiter GetAwaiter()
        {
            return task.GetAwaiter();
        }

        public async UniTask EnumerateAsync()
        {
            while (isPlaying && time < duration)
            {
                setter(lerper(start, end, easeFunction(time / duration)));
                time += Time.deltaTime;
                await UniTask.Yield();
            }

            setter?.Invoke(end);
            Dispose();
        }
    }
}