using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Ponito.Core.Ease
{
    public partial class EaseEnumerator<T> : EaseAnimation
    {
        private readonly T            start;
        private readonly T            end;
        private          Lerper<T>    lerper;
        private          Setter<T>    setter;
        private readonly float        duration;
        private          EaseType     easeType;
        private          float        time;
        private          EaseFunction easeFunction;
        private          bool         isPlaying;
        private readonly UniTask      task;

        public EaseEnumerator(
            T start,
            T end,
            Setter<T> setter,
            Lerper<T> lerper,
            float duration,
            EaseType easeType = EaseType.InSine)
        {
            this.start        = start;
            this.end          = end;
            this.lerper       = lerper;
            this.setter       = setter;
            this.duration     = duration;
            this.easeType     = easeType;
            this.time         = 0f;
            this.easeFunction = EasingEquations.GetFunction(easeType);

            isPlaying = true;
            this.task = EnumerateAsync();
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
    }
}