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
    }
}