using System;
using Ponito.Core.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Ponito.Core.Samples.UI
{
    /// <summary>
    ///     If you wish to manage your UI by separate them by <see cref="Canvas" />, use <see cref="PoCanvas" /> instead.
    ///     'Cause this help you name your <see cref="Canvas" /> and scale your <see cref="Canvas" />.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasScaler))]
    [RequireComponent(typeof(GraphicRaycaster))]
    [AddComponentMenu("Ponito/Core/Samples/UI/Po Canvas")]
    public class PoCanvas : MonoBehaviour
    {
        /// <summary>
        ///     名字
        /// </summary>
        [Tooltip("名字")]
        [SerializeField] private string id;

        /// <summary>
        ///     渲染順序，數字越大越在上面
        /// </summary>
        [Tooltip("渲染順序，數字越大越在上面")]
        [SerializeField] private int order;

        /// <summary>
        ///     自動設定為有標籤的攝影機，因為有時候會在 prefab 裡面，所以開始時也會設置好。
        /// </summary>
        [Tooltip("自動設定為有標籤的攝影機，因為有時候會在 prefab 裡面，所以開始時也會設置好。")]
        [SerializeField] private string cameraTag = "MainCamera";

        /// <summary>
        ///     可不可以互動，會開關 GraphicRaycaster
        /// </summary>
        [Tooltip("可不可以互動，會開關 GraphicRaycaster")]
        [SerializeField] private bool isInteractable = true;

        /// <summary>
        ///     使用 Expand 來延伸小的內容來填滿螢幕，使用 Shrink 來縮小內容以符合螢幕像是背景一樣
        /// </summary>
        [Tooltip("使用 Expand 來延伸小的內容來填滿螢幕，使用 Shrink 來縮小內容以符合螢幕像是背景一樣")]
        [SerializeField] private MatchMode screenMatchMode = MatchMode.Expand;

        /// <summary>
        ///     對齊美術製作時的尺寸
        /// </summary>
        [Tooltip("對齊美術製作時的尺寸")]
        [SerializeField] private Vector2 referenceResolution = new(1920, 1080);

        public bool IsInteractable
        {
            get => isInteractable;
            set
            {
                isInteractable = value;
                this.EnsureComponent(out GraphicRaycaster _, it => { it.enabled = isInteractable; });
            }
        }

        private void OnEnable()
        {
            GetComponent<Canvas>().Apply(it =>
            {
                if (!it.worldCamera.IsNull()) return;

                var cameraObject = GameObject.FindGameObjectWithTag(cameraTag);
                it.worldCamera = cameraObject.GetComponent<Camera>();
            });
        }

#if UNITY_EDITOR
        /// <summary>
        ///     This will activate when something change in <see cref="Editor" />
        /// </summary>
        private void OnValidate()
        {
            name = $"{id} ({screenMatchMode} Canvas)";

            GetComponent<Canvas>().Apply(it =>
            {
                it.sortingOrder = order;
                it.renderMode   = RenderMode.ScreenSpaceCamera;
                var cameraObject = GameObject.FindGameObjectWithTag(cameraTag);
                it.worldCamera = cameraObject.GetComponent<Camera>();
            });

            GetComponent<GraphicRaycaster>().Apply(it => { it.enabled = isInteractable; });

            GetComponent<CanvasScaler>().Apply(it =>
            {
                it.uiScaleMode         = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                it.screenMatchMode     = (CanvasScaler.ScreenMatchMode)screenMatchMode;
                it.referenceResolution = referenceResolution;
            });
        }
#endif
    }
}