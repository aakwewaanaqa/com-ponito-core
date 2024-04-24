using System.Linq;
using System.Reflection;
using Ponito.Core.Asyncronized;
using Ponito.Core.Ease;
using Ponito.Core.Extensions;
using UnityEngine;
using UnityEngine.Audio;

namespace Ponito.Core.Samples
{
    /// <summary>
    ///     Manages <see cref="AudioSource" /> by splitting into <see cref="AudioPlayType" />
    /// </summary>
    [AddComponentMenu("Ponito/Core/Samples/Po Audio Manager")]
    public class PoAudioManager : MonoSingleton<PoAudioManager>
    {
        [SerializeField] private AudioSource music;
        [SerializeField] private AudioSource fx;
        [SerializeField] private AudioSource voice;
        [SerializeField] private AudioSource ui;

        /// <inheritdoc />
        protected override bool isInitialized => music.IsObject() && fx.IsObject() &&
                                                 voice.IsObject() && ui.IsObject();

        /// <inheritdoc />
        protected override bool isDontDestroyOnLoad => true;

        /// <summary>
        ///     Gets every field of [<see cref="SerializeField"/>]s
        /// </summary>
        /// <returns></returns>
        protected IQueryable<FieldInfo> GetFields()
        {
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
            return typeof(PoAudioManager)
               .GetFields(flags)
               .Where(f => f.GetCustomAttribute<SerializeField>() is object)
               .AsQueryable();
        }

        /// <inheritdoc />
        protected override void Initialize()
        {
            var mixer = Resources.Load<AudioMixer>(nameof(PoAudioManager));

            foreach (var info in GetFields())
            {
                new GameObject(info.Name)
                   .EnsureComponent(out AudioSource source)
                   .SetParent(transform, true);

                info.SetValue(this, source);
                if (mixer.IsNull()) continue;
                source.outputAudioMixerGroup = mixer.FindMatchingGroups(info.Name)[0];
            }
        }

        /// <summary>
        ///     Gets <see cref="AudioSource" /> by <see cref="type" />
        /// </summary>
        /// <param name="type">the type of managed <see cref="AudioSource" />s</param>
        /// <returns>source</returns>
        public AudioSource GetSource(AudioPlayType type = AudioPlayType.Music)
        {
            return type switch
            {
                AudioPlayType.Music => music,
                AudioPlayType.FX    => fx,
                AudioPlayType.Voice => voice,
                AudioPlayType.UI    => ui,
                _                   => music
            };
        }

        /// <summary>
        ///     Stops the <see cref="AudioSource" /> in <see cref="duration" /> of seconds
        /// </summary>
        /// <param name="type">the type of managed <see cref="AudioSource" />s</param>
        /// <param name="duration">delay in seconds</param>
        public async PoTask Stop(AudioPlayType type = AudioPlayType.Music, float duration = 0.2f)
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

        public async PoTask Play(AudioClip clip, AudioPlayType type = AudioPlayType.Music, bool isOneShot = false)
        {
            if (clip.IsNull()) return;
            
            if (!isOneShot) await Stop(type); // Stops gentally
            
            var source = GetSource(type);
            source.clip = clip;
            
            if (isOneShot) source.PlayOneShot(clip);
            else source.Play();
        }
    }
}