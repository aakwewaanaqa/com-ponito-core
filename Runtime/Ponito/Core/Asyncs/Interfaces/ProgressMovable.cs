namespace Ponito.Core.Asyncs.Interfaces
{
    /// <summary>
    ///     <see cref="MovableRunner"/> 中每個被等待者的核心功能基本單位，並且可以回報進度
    /// </summary>
    public interface ProgressMovable : Movable
    {
        /// <summary>
        ///     回報等候的進度
        /// </summary>
        public float Progress { get; }
    }
}