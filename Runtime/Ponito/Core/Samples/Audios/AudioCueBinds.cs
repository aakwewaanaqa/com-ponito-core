using System;
using System.Linq;
using UnityEngine;

namespace Ponito.Core.Samples.Audios
{
    [Serializable]
    public struct AudioCueBinds
    {
        [SerializeField] private AudioCue[] cues;

        public AudioCue GetCue()
        {
            var max    = cues.Sum(c => c.odds);
            var random = UnityEngine.Random.Range(0, max);
            foreach (var cue in cues)
            {
                if (random < cue.odds) return cue;
                random -= cue.odds;
            }

            return cues[^1];
        }
    }
}