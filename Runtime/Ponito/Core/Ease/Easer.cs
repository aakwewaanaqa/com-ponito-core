using UnityEngine;

namespace Ponito.Core.Ease
{
    internal partial class Easer<T> : Easable
    {
        private float        duration     { get; }
        private T            end          { get; }
        private T            start        { get; }
        private EaseFunction easeFunction { get; set; }
        private EaseType     easeType     { get; set; }
        private Lerper<T>    lerper       { get; set; }
        private Setter<T>    setter       { get; set; }
        private float        time         { get; set; }
        
        public  bool         IsPlaying    { get; set; }

        public Easer(
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

        public float  Progress    => time / duration;
        public object Current     => lerper(start, end, easeFunction(time / duration));
        public bool   IsCompleted => IsPlaying && time >= duration;

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

        public EaseAwaiter GetAwaiter()
        {
            return new EaseAwaiter(this);
        }

        public bool MoveNext()
        {
            if (!IsCompleted)
            {
                IsPlaying = false;
                setter?.Invoke(end); // Weird bug when animation disposed...
                Dispose();
                return false;
            }

            setter((T)Current);
            time += Time.deltaTime;

            return true;
        }

        public void Reset()
        {
            time = 0f;
        }
    }
}