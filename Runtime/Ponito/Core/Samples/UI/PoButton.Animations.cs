using Ponito.Core.Ease;
using UnityEngine;

namespace Ponito.Core.Samples.UI
{
    public partial class PoButton
    {
        private Vector3       originalScale;
        private EaseAnimation ease;
        
        private async void PlayAudio(bool isPressed)
        {
            var clip = isPressed? pointerDown : pointerUp;
            await PoAudioManager.Instance.Play(clip);
        }
        
        private async void PlayAnimation(bool isPressed)
        {
            ease?.Kill();
            ease?.Dispose();
            ease = animationType switch
            {
                AnimationType.None  => null,
                AnimationType.Scale => ScaleAnimation(isPressed),
                _                   => null,
            };
            if (ease != null) await ease;
        }

        private EaseAnimation ScaleAnimation(bool isPressed)
        {
            var from   = rectTransform.localScale;
            var to     = isPressed ? originalScale * 0.8f : originalScale;
            var setter = new Setter<Vector3>(s => rectTransform.localScale = s);
            var type   = isPressed ? EaseType.InSine : EaseType.OutBounce;
            return DoEase.Create(from, to, setter, 0.2f, type);
        }
    }
}