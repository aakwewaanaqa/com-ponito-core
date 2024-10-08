using System;
using UnityEngine;

namespace Ponito.Core.Samples.Audios
{
    [Serializable]
    public struct AudioCue
    {
        public int        odds;
        public FloatRange volume;
        public FloatRange pitch;
        public AudioClip  clip;

        public AudioCue(AudioClip clip)
        {
            odds = 0;
            volume = new FloatRange(1, 1);
            pitch = new FloatRange(1, 1);
            this.clip = clip;
        }
    }

    public enum CueType
    {
        Music,
        FX,
        UI,
        Voice
    }
}