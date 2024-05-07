using System;
using Ponito.Core.DebugHelper;
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
        private bool         isEnded      { get; set; }
        private Action       continuation { get; set; }

        private Easer(
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
            easeFunction  = EasingEquations.GetFunction(easeType);
        }

        public float  Progress => time / duration;

        public void Dispose()
        {
            lerper       = null;
            setter       = null;
            easeFunction = null;
            continuation = null;
        }


        public void Kill() => isEnded = true;
    }
}