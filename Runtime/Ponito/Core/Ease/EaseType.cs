namespace Ponito.Core.Ease
{
    /// <summary>
    ///     <see cref="EaseFunction"/> 的漸變類型。
    /// </summary>
    /// <remarks>
    ///     In 會像是由慢到快； Out 會像是由快到慢。
    /// </remarks>
    /// <seealso cref="EasingEquations.GetFunction"/>
    /// <value>
    ///     <see cref="InSine"/>
    ///     <see cref="OutSine"/>
    ///     <see cref="InOutSine"/>
    ///     <see cref="InQuad"/>
    ///     <see cref="OutQuad"/>
    ///     <see cref="InOutQuad"/>
    ///     <see cref="InCubic"/>
    ///     <see cref="OutCubic"/>
    ///     <see cref="InOutCubic"/>
    ///     <see cref="InQuart"/>
    ///     <see cref="OutQuart"/>
    ///     <see cref="InOutQuart"/>
    ///     <see cref="InExpo"/>
    ///     <see cref="OutExpo"/>
    ///     <see cref="InOutExpo"/>
    ///     <see cref="InCirc"/>
    ///     <see cref="OutCirc"/>
    ///     <see cref="InOutCirc"/>
    ///     <see cref="InElastic"/>
    ///     <see cref="OutElastic"/>
    ///     <see cref="InOutElastic"/>
    ///     <see cref="InBounce"/>
    ///     <see cref="OutBounce"/>
    ///     <see cref="InOutBounce"/>
    /// </value>
    public enum EaseType
    {
        InSine,
        OutSine,
        InOutSine,
        InQuad,
        OutQuad,
        InOutQuad,
        InCubic,
        OutCubic,
        InOutCubic,
        InQuart,
        OutQuart,
        InOutQuart,
        InExpo,
        OutExpo,
        InOutExpo,
        InCirc,
        OutCirc,
        InOutCirc,
        InElastic,
        OutElastic,
        InOutElastic,
        InBounce,
        OutBounce,
        InOutBounce
    }
}