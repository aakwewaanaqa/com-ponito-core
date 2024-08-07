using System.Linq;
using System.Reflection;
using System.Threading;
using Ponito.Core.Animations;
using Ponito.Core.Asyncs.Extensions;
using Ponito.Core.Asyncs.Tasks;
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
        public const float DEFAULT_FADE_DURATION = 0.2f;
        
        [SerializeField] private AudioSource music;
        [SerializeField] private AudioSource fx;
        [SerializeField] private AudioSource voice;
        [SerializeField] private AudioSource ui;

        /// <inheritdoc />
        protected override bool IsInitialized => music.IsObject() && fx.IsObject() &&
                                                 voice.IsObject() && ui.IsObject();

        /// <inheritdoc />
        protected override bool IsDontDestroyOnLoad => true;
        
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

        private TimeState stop { get; set; } = new(); 

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
                var groups = mixer.FindMatchingGroups(info.Name);
                source.outputAudioMixerGroup = groups.Length > 0 ? groups[0] : default;
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
        public async PoTask Stop(AudioPlayType type = AudioPlayType.Music, float duration = DEFAULT_FADE_DURATION, CancellationToken ct = default)
        {
            var source = GetSource(type);
            if (source.isPlaying)
            {
                await stop.Play(source.volume, 0f, duration, t =>
                {
                    source.volume = t;
                    if (t == 0f)
                    {
                        source.clip = null;
                        source.Stop();
                        source.volume = 1f;
                    }
                }, ct);
            }
        }

        public async PoTask Play(AudioClip clip, AudioPlayType type = AudioPlayType.Music, bool isOneShot = false, CancellationToken ct = default)
        {
            if (ct.IsCancellationRequested) return;
            if (clip.IsNull()) return;
            
            if (!isOneShot) await Stop(type, ct: ct); // 慢慢地停止音效
            if (ct.IsCancellationRequested) return;
            
            var source = GetSource(type);
            source.clip = clip;
            
            if (isOneShot) source.PlayOneShot(clip);
            else source.Play();
             
            await clip.length.Delay(ct);
        }
    }
}