using System.Reflection;
using Cysharp.Threading.Tasks;
using Ponito.Core.Ease;
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

        public AudioSource GetSource(AudioPlayType type = AudioPlayType.Music)
        {
            return type switch
            {
                AudioPlayType.Music => music,
                AudioPlayType.FX    => fx,
                AudioPlayType.Voice => voice,
                AudioPlayType.UI    => ui,
                _                   => music,
            };
        }

        public async UniTask Stop(AudioPlayType type = AudioPlayType.Music, float duration = 0.2f)
        {
            var source = GetSource(type);
            if (source.isPlaying)
            {
                var from   = source.volume;
                var setter = new Setter<float>(v => source.volume = v);
                await DoEase.Create(from, 0f, setter, duration);
                source.clip = null;
            }

            source.Stop();
        }

        public async UniTask Play(AudioClip clip, AudioPlayType type = AudioPlayType.Music, bool isOneShot = false)
        {
            var source = GetSource(type);
            if (!isOneShot) source.Stop();
            source.clip = clip;
            source.Play();
            while (source.isPlaying) await UniTask.Yield();
        }
    }
}