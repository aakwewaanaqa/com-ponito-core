using Cysharp.Threading.Tasks;

namespace Ponito.Core.UI
{
    /// <summary>
    ///     If you wish to manage your UI through async MVC system, inherit this by your class.
    /// </summary>
    /// <seealso cref="Show"/>
    /// <seealso cref="Hide"/>
    public interface AsyncView
    {
        /// <summary>
        ///     Smart manage your <see cref="AsyncView"/> to <see cref="Show"/>
        /// </summary>
        /// <param name="args">any arguments you wish to pass into</param>
        /// <returns><see cref="UniTask"/> of itself</returns>
        UniTask Show(object args);
        
        /// <summary>
        ///     Smart manage your <see cref="AsyncView"/> to <see cref="Hide"/>
        /// </summary>
        /// <param name="args">any arguments you wish to pass into</param>
        /// <returns><see cref="UniTask"/> of itself</returns>
        UniTask Hide(object args);
        
        /// <summary>
        ///     Gets the state of the <see cref="AsyncView"/>
        /// </summary>
        ViewState State { get; }
    }
}