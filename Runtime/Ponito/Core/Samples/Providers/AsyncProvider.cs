using System;
using System.Threading.Tasks;
using Object = UnityEngine.Object;

namespace Ponito.Core.Samples.Providers
{
    public interface AsyncProvider : IDisposable
    {
        /// <summary>
        ///     Provides <see cref="T" /> asynchronisely through certain method
        /// </summary>
        /// <param name="subKey">the key</param>
        /// <typeparam name="T"><see cref="Object" /> type</typeparam>
        /// <returns>task</returns>
        /// <remarks>make implementation async</remarks>
        Task<T> ProvideAsync<T>(object subKey);

        Task<string> MakePath(object subKey);
    }
}