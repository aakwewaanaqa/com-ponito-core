﻿using System.ComponentModel;
using Ponito.Core.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ponito.Core.UI
{
    [HasEvent(nameof(onClick))]
    [AddComponentMenu("Ponito/Core/UI/Po Button")]
    public partial class PoButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        [SerializeField] private bool          isInteractable = true;
        [SerializeField] private Graphic       image;
        [SerializeField] private AnimationType animationType = AnimationType.Scale;
        [SerializeField] private AudioClip     pointerDown;
        [SerializeField] private AudioClip     pointerUp;
        [SerializeField] public  UnityEvent    onClick;

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

            PlayAudio(true);
            PlayAnimation(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!isInteractable) return;

            PlayAudio(true);
            PlayAnimation(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isInteractable) return;

            onClick?.Invoke();
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