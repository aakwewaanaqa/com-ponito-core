using UnityEngine;
using UnityEngine.UI;

namespace Ponito.Core.Samples.UI
{
    /// <summary>
    ///     How to match the current content of the <see cref="PoCanvas"/> with <see cref="Canvas"/>
    /// </summary>
    public enum MatchMode
    {
        /// <inheritdoc cref="CanvasScaler.ScreenMatchMode.Expand"/>
        Expand = CanvasScaler.ScreenMatchMode.Expand,

        /// <inheritdoc cref="CanvasScaler.ScreenMatchMode.Shrink"/>
        Shrink = CanvasScaler.ScreenMatchMode.Shrink,
    }
}