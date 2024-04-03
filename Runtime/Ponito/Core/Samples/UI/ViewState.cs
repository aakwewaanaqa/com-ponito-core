namespace Ponito.Core.Samples.UI
{
    /// <summary>
    ///     Common states of a <see cref="AsyncView"/>
    /// </summary>
    public enum ViewState
    {
        /// <summary>
        ///     The <see cref="AsyncView"/> is on
        /// </summary>
        Showing,
        
        /// <summary>
        ///     The <see cref="AsyncView"/> is in between on and off
        /// </summary>
        Transitioning,
        
        /// <summary>
        ///     The <see cref="AsyncView"/> is off
        /// </summary>
        Hidden,
    }
}