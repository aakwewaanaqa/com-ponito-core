using System.Diagnostics;
using Cysharp.Threading.Tasks;
using Unity.Plastic.Antlr3.Runtime.Misc;

namespace Ponito.Core.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        ///     Runs an <see cref="UniTask"/> on thread pool
        /// </summary>
        /// <param name="task">to run</param>
        /// <seealso cref="UniTask.RunOnThreadPool"/>
        [DebuggerHidden]
        public static async void Run(this UniTask task)
        {
            await UniTask.RunOnThreadPool(async () => await task);
        }
        
        public static UniTask Chain(this UniTask task, UniTask then)
        {
            return UniTask.Create(async () =>
            {
                await task;
                await then;
            });
        }
    }
}