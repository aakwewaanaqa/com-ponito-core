using System.Threading;
using Ponito.Core.Asyncs.Extensions;
using Ponito.Core.Asyncs.Tasks;
using Ponito.Core.Ease;
using Ponito.Core.Extensions;
using UnityEngine;

namespace Ponito.Core.Samples.Managers
{
    /// <summary>
    ///     Manages <see cref="AudioSource" /> by splitting into <see cref="AudioPlayType" />
    /// </summary>
    [AddComponentMenu("Ponito/Core/Samples/Managers/Po Audio Manager")]
    public partial class PoAudioManager : MonoBehaviour
    {
        public const float DEFAULT_FADE_DURATION = 0.2f;

        [SerializeField] private AudioSource music;
        [SerializeField] private AudioSource fx;
        [SerializeField] private AudioSource voice;
        [SerializeField] private AudioSource ui;

        private CancellationTokenSource cts = new();

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
        /// <param name="ct">取消簽證</param>
        public async PoTask Stop(
            AudioPlayType     type     = AudioPlayType.Music,
            float             duration = DEFAULT_FADE_DURATION,
            CancellationToken ct       = default)
        {
            if (ct.IsCancellationRequested) return;
            cts = cts.LinkAfterCancel(ct, out var innerCt);
            await Fade(type, duration, innerCt);
        }

        private async PoTask Fade(
            AudioPlayType     type     = AudioPlayType.Music,
            float             duration = DEFAULT_FADE_DURATION,
            CancellationToken ct       = default)
        {
            if (ct.IsCancellationRequested) return;

            var source = GetSource(type);
            if (!source.isPlaying) { source.volume = 0f; }
            else
            {
                var from   = source.volume;
                var to     = 0f;
                var setter = new Setter<float>(v => source.volume = v);
                await DoEase.To(from, to, setter, duration, EaseType.InOutSine, ct);
            }

            source.Stop();
        }

        public async PoTask Play(
            AudioClip         clip,
            AudioPlayType     type      = AudioPlayType.Music,
            bool              isOneShot = false,
            CancellationToken ct        = default)
        {
            if (ct.IsCancellationRequested) return;
            cts = cts.LinkAfterCancel(ct, out var innerCt);
            if (clip.IsNull()) return;

            if (!isOneShot) await Fade(type, ct: innerCt); // 慢慢地停止音效
            if (innerCt.IsCancellationRequested) return;

            var source = GetSource(type);
            source.clip = clip;

            if (isOneShot) source.PlayOneShot(clip);
            else source.Play();

            await clip.length.Delay(innerCt);
        }
    }
}