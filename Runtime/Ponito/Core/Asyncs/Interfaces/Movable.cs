using System;
using System.Runtime.CompilerServices;
using Ponito.Core.Asyncs.Tasks;

namespace Ponito.Core.Asyncs.Interfaces
{
    /// <summary>
    ///     <see cref="MovableRunner"/> 中每個被等待者的核心功能基本單位
    /// </summary>
    public interface Movable : INotifyCompletion, IDisposable
    {
        /// <summary>
        ///     任何在這個 <see cref="Movable"/> 中存在的 <see cref="Ex"/> 
        ///     當涉及到 <see cref="PoTask"/> 時，可能會有內部例外......
        /// </summary>
        Exception Ex { set; get; }

        /// <summary>
        ///     這個 <see cref="Movable"/> 是否已經完成移動？ C# 編譯器會自動使用這個屬性
        ///     <see cref="PoTask"/> 會有已完成的內部 <see cref="Movable"/>。
        /// </summary>
        bool IsCompleted { get; }

        /// <summary>
        ///     把 <see cref="Movable"/>往前推移的函式，通常為 <see cref="MovableRunner"/> 使用。
        /// </summary>
        /// <returns>
        ///     <see cref="Movable"/>完成了沒？
        /// </returns>
        bool MoveNext();

        /// <summary>
        ///     取得結果！ C# 編譯器會自動使用這個屬性
        /// </summary>
        void GetResult();

        /// <summary>
        ///     取得等待者。 C# 編譯器會自動使用這個屬性
        /// </summary>
        /// <returns>等待者</returns>
        Movable GetAwaiter();
    }

    /// <inheritdoc />
    public interface Movable<out T> : Movable
    {
        /// <summary>
        ///     取得結果！
        /// </summary>
        new T GetResult();

        /// <summary>
        ///     取得等待者。
        /// </summary>
        /// <returns>等待者</returns>
        new Movable<T> GetAwaiter();
    }
}