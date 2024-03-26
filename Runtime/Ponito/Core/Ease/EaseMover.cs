using System;
using UnityEngine;

namespace Ponito.Core.Ease
{
    public class EaseMover : IDisposable
    {
        public  float    Duration { get; set; }
        private EaseType easeType { get; set; }

        public EaseType EaseType
        {
            get => easeType;
            set
            {
                easeType = value;
                function = EasingEquations.GetFunction(value);
            }
        }

        private float        time     { get; set; }
        private EaseFunction function { get; set; }

        public EaseMover(float d)
        {
            Duration = d;
        }
        
        public bool MoveNext()
        {
            time    = Mathf.Clamp(time + Time.deltaTime, 0, Duration);
            Current = Mathf.Clamp01(function(time));
            return time >= Duration;
        }

        public float Current { get; private set; }

        public void Dispose()
        {
            function = null;
        }
    }
}