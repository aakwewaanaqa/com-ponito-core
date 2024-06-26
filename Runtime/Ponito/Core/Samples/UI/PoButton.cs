﻿using System.ComponentModel;
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
        [SerializeField] private bool          isInteractable = true;
        [SerializeField] private Graphic       image;
        [SerializeField] private AnimationType animationType = AnimationType.Scale;
        [SerializeField] private AudioClip     pointerDown;
        [SerializeField] private AudioClip     pointerUp;
        [SerializeField] public  UnityEvent    onClick = new();

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
            this.EnsureComponent(out rectTransform, it =>
            {
                originalScale = it.localScale;
            });
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
            _         = PlayAudio(true);
            _         = PlayAnimation(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!isInteractable) return;
            if (PoButtonBlockScope.IsBlock) return;
            dataScope?.Dispose();
            _ = PlayAudio(false);
            _ = PlayAnimation(false);
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