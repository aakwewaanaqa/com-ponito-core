using System;
using Ponito.Core.Samples.Audios;
using UnityEngine;

namespace Ponito.Core.Samples.Settings
{
    public partial class PoAudioSettings
    {
        [SerializeField] public SerializableSet<string, AudioClip>     musics;
        [SerializeField] public SerializableSet<string, AudioClip>     voices;
        [SerializeField] public SerializableSet<string, AudioCueBinds> fxBinds;
        [SerializeField] public SerializableSet<string, AudioCueBinds> uiBinds;

        public AudioCue GetCue(CueType type, string key)
        {
            return type switch
            {
                CueType.Music => new AudioCue(musics[key]),
                CueType.Voice => new AudioCue(voices[key]),
                CueType.FX    => fxBinds[key].GetCue(),
                CueType.UI    => uiBinds[key].GetCue(),
                _             => default,
            };
        }
    }
}