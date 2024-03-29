using System.Reflection;
using Cysharp.Threading.Tasks;
using Ponito.Core.Extensions;
using UnityEngine;
using UnityEngine.Audio;

namespace Ponito.Core.Samples
{
    [AddComponentMenu("Ponito/Core/Samples/Audio Manager")]
    public class PoAudioManager : MonoSingleton<PoAudioManager>
    {
        [SerializeField] private AudioSource music;
        [SerializeField] private AudioSource fx;
        [SerializeField] private AudioSource voice;
        [SerializeField] private AudioSource ui;

        protected override bool isInitialized => music.IsObject() && fx.IsObject() &&
                                                 voice.IsObject() && ui.IsObject();
        protected override bool isDontDestroyOnLoad => true;

        protected override void Initialize()
        {
            var mixer = Resources.Load<AudioMixer>(nameof(PoAudioManager));
            if (mixer.IsNull()) return;

            var fields = GetType()
               .GetFields(isPublic: false, isStatic: false)
               .WithAttributes<SerializeField>();

            foreach (var info in fields)
            {
                var field = info as FieldInfo;

                new GameObject(field.Name)
                   .EnsureComponent(out AudioSource source)
                   .SetParent(transform, true);

                field.SetValue(this, source);
                source.outputAudioMixerGroup = mixer.FindMatchingGroups(field.Name)[0];
            }
        }
    }
}