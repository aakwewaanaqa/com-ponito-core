using System;
using System.ComponentModel;
using Ponito.Core.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ponito.Core.Samples.UI
{
    [DefaultEvent(nameof(onClick))]
    [AddComponentMenu("Ponito/Core/Samples/UI/Po Button")]
    public partial class PoButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        [SerializeField]            private bool          isInteractable = true;
        [SerializeField]            private Graphic       image;
        [SerializeField]            private AnimationType animationType = AnimationType.Scale;
        [SerializeField] [Obsolete] private AudioClip     pointerDown;
        [SerializeField] [Obsolete] private AudioClip     pointerUp;
        [SerializeField]            private string        onClickCue;
        [SerializeField]            public  UnityEvent    onClick = new();

        private RectTransform     rectTransform;
        private PoButtonDataScope dataScope;

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
            this.EnsureComponent(out rectTransform, it => { originalScale = it.localScale; });
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isInteractable) return;
            if (PoButtonBlockScope.IsBlock) return;
            dataScope?.Dispose();
            onClick?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!isInteractable) return;
            if (PoButtonBlockScope.IsBlock) return;
            dataScope = new PoButtonDataScope(this);

            cts = cts.LinkAfterCancel(default, out var ct);
            _ = PlayAudio(true, ct);
            _ = PlayAnimation(true, ct);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!isInteractable) return;
            if (PoButtonBlockScope.IsBlock) return;
            dataScope?.Dispose();

            cts = cts.LinkAfterCancel(default, out var ct);
            _   = PlayAudio(false, ct);
            _   = PlayAnimation(false, ct);
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