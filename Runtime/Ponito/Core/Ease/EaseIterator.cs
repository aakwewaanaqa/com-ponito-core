using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Ponito.Core.Ease.DoEase;

namespace Ponito.Core.Ease
{
    public class EaseIterator<T> : IEnumerator<T>
    {
        private readonly T        start;
        private readonly T        end;
        private          Lerp<T>  lerp;
        private readonly float    duration;
        public           EaseType easeType;
        private          float    time;

        private EaseFunction easeFunction;

        public EaseIterator(T start, T end, Lerp<T> lerp, float duration, EaseType easeType = EaseType.InSine)
        {
            this.start        = start;
            this.end          = end;
            this.lerp         = lerp;
            this.duration     = duration;
            this.easeType     = easeType;
            this.time         = 0f;
            this.easeFunction = EasingEquations.GetFunction(easeType);
        }

        public static EaseIterator<float> Float(
            float start,
            float end,
            float duration,
            EaseType easeType = EaseType.InSine)
            => new(start, end, LerpOfFloat, duration);
        
        public static EaseIterator<Vector2> Vector2(
            Vector2 start,
            Vector2 end,
            float duration,
            EaseType easeType = EaseType.InSine) =>
            new(start, end, LerpOfVector2, duration, easeType);
        
        public static EaseIterator<Vector3> Vector3(
            Vector3 start,
            Vector3 end,
            float duration,
            EaseType easeType = EaseType.InSine) =>
            new(start, end, LerpOfVector3, duration, easeType);
        
        public static EaseIterator<Quaternion> Quaternion(
            Quaternion start,
            Quaternion end,
            float duration,
            EaseType easeType = EaseType.InSine) =>
            new(start, end, LerpOfQuaternion, duration, easeType);

        public bool MoveNext()
        {
            Current =  lerp(start, end, easeFunction(time / duration));
            time    += Time.deltaTime;

            if (time < duration) return true;

            time = duration;
            return false;
        }

        public void Reset()
        {
            time = 0f;
        }

        public T Current { get; private set; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            lerp         = null;
            easeFunction = null;
        }
    }
}