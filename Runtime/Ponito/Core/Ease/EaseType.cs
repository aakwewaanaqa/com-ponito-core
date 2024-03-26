namespace Ponito.Core.Ease
{
    /// <summary>
    ///     type of the <see cref="EaseFunction"/>
    /// </summary>
    /// <remarks>
    ///     In will be like slow to fast. Out will be like fast to slow.
    /// </remarks>
    /// <seealso cref="EasingEquations.GetFunction"/>
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