using System.Threading;
using Ponito.Core.Asyncs.Tasks;
using Ponito.Core.Ease;
using Ponito.Core.Ease.SpecialEases;
using Ponito.Core.Extensions;
using Ponito.Core.Samples.Audios;
using Ponito.Core.Samples.Managers;
using Ponito.Core.Samples.Settings;
using UnityEngine;
using static Ponito.Core.Ease.EaseType;

namespace Ponito.Core.Samples.UI
{
    public partial class PoButton
    {
        private Vector3                 originalScale { get; set; }
        private CancellationTokenSource cts           { get; set; }

        private async PoTask PlayAudio(bool isPressed, CancellationToken ct = default)
        {
            if (isPressed) return;
            if (ct.IsCancellationRequested) return;
            var cue = PoAudioSettings.Singleton.GetCue(CueType.UI, onClickCue);
            var manager = PoAudioManager.Singleton;
            manager.SetSettings(AudioPlayType.UI, cue.volume, cue.pitch);
            await manager.Play(cue.clip, AudioPlayType.UI, ct: ct);
        }

        private async PoTask PlayAnimation(bool isPressed, CancellationToken ct = default)
        {
            var task = animationType switch
            {
                AnimationType.None  => null,
                AnimationType.Scale => ScaleAnimation(isPressed, ct),
                AnimationType.Punch => PunchAnimation(isPressed, ct),
                _                   => null
            };
            if (task != null) await task;
        }

        private async PoTask ScaleAnimation(bool isPressed, CancellationToken ct)
        {
            if (ct.IsCancellationRequested) return;

            var from   = rectTransform.localScale;
            var to     = isPressed ? originalScale * 0.8f : originalScale;
            var setter = new Setter<Vector3>(s => rectTransform.localScale = s);
            var type   = isPressed ? InSine : OutBounce;
            await DoEase.To(from, to, setter, 0.2f, type, ct);
        }

        private async PoTask PunchAnimation(bool isPressed, CancellationToken ct)
        {
            if (ct.IsCancellationRequested) return;
            if (isPressed) return;

            var ease = new Punch(0.2f, 0.35f, 10f, 1f).GetEaseFunction();
            var from = originalScale;
            var t    = 0f;
            while (t < 0.2f)
            {
                var s = from * (1f + ease(t));
                rectTransform.localScale = s;
                await Controls.Yield();
                t += Time.deltaTime;
            }
        }
    }
}