namespace Ponito.Core.Ease
{
    /// <summary>
    ///     Delegate for interpolation value <see cref="T"/> 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public delegate T Lerper<T>(T a, T b, float t);
}