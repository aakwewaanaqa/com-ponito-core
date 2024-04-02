using System;
using Cysharp.Threading.Tasks;
using Ponito.Core.Ease;
using Ponito.Core.Extensions;
using Ponito.Core.Samples;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ponito.Core.UI
{
    [AddComponentMenu("Ponito/Core/UI/PoButton")]
    public class PoButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        [SerializeField] private bool      isInteractable = true;
        [SerializeField] private Graphic   image;
        [SerializeField] private AnimationType animationType = AnimationType.Scale;
        [SerializeField] private AudioClip pointerDownSound;
        [SerializeField] private AudioClip pointerUpSound;

        public Action onPointerDownEvent;
        public Action onPointerUpEvent;

        private RectTransform rectTransform;

        public bool IsInteractable
        {
            get => isInteractable;
            set
            {
                isInteractable = value;

                if (this.IsNull()) return;

                TryGetComponent(out image);
                if (image.IsObject()) image.raycastTarget = value;
            }
        }

        private void OnEnable()
        {
            gameObject.EnsureComponent(out rectTransform);
            originalScale = rectTransform.localScale;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!isInteractable) return;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!isInteractable) return;

            UniTask.RunOnThreadPool(async () =>
            {
                await UniTask.SwitchToMainThread();
                await SoundMusicManager.GetInstance().PlayFXAsync(pointerUpSound);
            });

            onPointerUpEvent?.Invoke();

            if (DOTween.IsTweening(rectTransform)) return;

            ScaleAnimation(false);
        }

        private Vector3       originalScale;
        private EaseAnimation ease;

        private async void Audio(bool isPressed)
        {
            var clip = isPressed? pointerDownSound : pointerUpSound;
            await PoAudioManager.Instance.Play(clip);
        }
        
        private async void Animation(bool isPressed)
        {
            ease?.Kill();
            ease?.Dispose();
            ease = animationType switch
            {
                AnimationType.None => null, 
                AnimationType.Scale => ScaleAnimation(isPressed),
                _ => null,
            };
        }
        
        private EaseAnimation ScaleAnimation(bool isPressed)
        {
            var from   = rectTransform.localScale;
            var to     = isPressed ? originalScale * 0.8f : originalScale;
            var setter = new Setter<Vector3>(s => rectTransform.localScale = s);
            var type   = isPressed ? EaseType.InSine : EaseType.OutBounce;
            return DoEase.Create(from, to, setter, 0.2f, type);
        }

#if UNITY_EDITOR
        private void Reset()
        {
            TryGetComponent(out image);
        }

        private void OnValidate()
        {
            if (image.IsObject()) image.raycastTarget = isInteractable;
        }
#endif
    }
}