using Ponito.Core.Asyncs.Tasks;
using Ponito.Core.Ease;
using UnityEngine;

namespace Ponito.Core.Samples.UI
{
    public partial class PoButton
    {
        private Easable ease;
        private Vector3 originalScale;

        private async PoTask PlayAudio(bool isPressed)
        {
            var clip = isPressed ? pointerDown : pointerUp;
            await PoAudioManager.Instance.Play(clip, AudioPlayType.UI, true);
        }

        private async PoTask PlayAnimation(bool isPressed)
        {
            ease?.Kill();
            ease = animationType switch
            {
                AnimationType.None  => null,
                AnimationType.Scale => ScaleAnimation(isPressed),
                _                   => null
            };
            await ease;
        }

        private Easable ScaleAnimation(bool isPressed)
        {
            var from   = rectTransform.localScale;
            var to     = isPressed ? originalScale * 0.8f : originalScale;
            var setter = new Setter<Vector3>(s => rectTransform.localScale = s);
            var type   = isPressed ? EaseType.InSine : EaseType.OutBounce;
            return DoEase.To(from, to, setter, 0.2f, type);
        }
    }
}