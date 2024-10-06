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
    }

    public enum CueType
    {
        Music,
        FX,
        UI,
        Voice
    }
}