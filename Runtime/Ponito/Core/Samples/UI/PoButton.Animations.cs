using System.Threading;
using Ponito.Core.Asyncs.Tasks;
using Ponito.Core.Ease;
using UnityEngine;
using static Ponito.Core.Ease.EaseType;

namespace Ponito.Core.Samples.UI
{
    public partial class PoButton
    {
        private Vector3                 originalScale { get; set; }
        private CancellationTokenSource cts           { get; set; }
        
        private async PoTask PlayAudio(bool isPressed)
        {
            var clip = isPressed ? pointerDown : pointerUp;
            await PoAudioManager.Singleton.Play(clip, AudioPlayType.UI, true);
        }

        private async PoTask PlayAnimation(bool isPressed)
        {
            cts?.Cancel();
            cts = new CancellationTokenSource();
            var ct = cts.Token;
            
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
            var from   = rectTransform.localScale;
            var to     = isPressed ? originalScale * 0.8f : originalScale;
            var setter = new Setter<Vector3>(s => rectTransform.localScale = s);
            var type   = isPressed ? InSine : OutBounce;
            await DoEase.To(from, to, setter, 0.2f, type, ct);
        }

        private async PoTask PunchAnimation(bool isPressed, CancellationToken ct)
        {
            if (isPressed) return;
            
            {
                var from   = originalScale;
                var to     = originalScale * 1.2f;
                var setter = new Setter<Vector3>(s => rectTransform.localScale = s);
                await DoEase.To(from, to, setter, 0.1f, InSine, ct);
            }
            
            {
                var from   = originalScale * 1.2f;
                var to     = originalScale;
                var setter = new Setter<Vector3>(s => rectTransform.localScale = s);
                await DoEase.To(from, to, setter, 0.1f, InSine, ct);
            }
        }
    }
}