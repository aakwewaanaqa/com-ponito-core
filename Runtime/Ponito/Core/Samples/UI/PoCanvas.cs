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
        ///     The name of the <see cref="PoCanvas" /> to identify between <see cref="PoCanvas" />es
        /// </summary>
        [SerializeField] private string id;

        /// <summary>
        ///     The rendering order
        /// </summary>
        [SerializeField] private int order;

        /// <summary>
        ///     Uses <see cref="GraphicRaycaster" /> or not
        /// </summary>
        [SerializeField] private bool isInteractable = true;

        /// <inheritdoc cref="CanvasScaler.uiScaleMode" />
        /// <remarks>
        ///     Uses <see cref="MatchMode.Expand" /> on content smaller than screen <br />
        ///     Uses <see cref="MatchMode.Shrink" /> on content or background bigger than screen
        /// </remarks>
        [SerializeField] private MatchMode screenMatchMode = MatchMode.Expand;

        /// <inheritdoc cref="CanvasScaler.referenceResolution" />
        [SerializeField] private Vector2 referenceResolution = new(1920, 1080);

        /// <inheritdoc cref="isInteractable" />
        public bool IsInteractable
        {
            get => isInteractable;
            set
            {
                isInteractable = value;
                this.EnsureComponent(out GraphicRaycaster _, it =>
                {
                    it.enabled = isInteractable;
                });
            }
        }

#if UNITY_EDITOR
        /// <summary>
        ///     This will active when something change in <see cref="Editor" />
        /// </summary>
        private void OnValidate()
        {
            name = $"{id} ({screenMatchMode} Canvas)";

            GetComponent<Canvas>().Apply(it =>
            {
                it.sortingOrder = order;
                it.renderMode   = RenderMode.ScreenSpaceCamera;
                it.worldCamera  = Camera.main;
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