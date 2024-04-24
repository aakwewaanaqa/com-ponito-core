using System.Runtime.CompilerServices;

namespace Ponito.Core.Asyncronized.Interfaces
{
    public interface Awaitable : Completable
    {
        void GetResult();

        Awaitable GetAwaiter()
        {
            return this;
        }
    }

    public interface Awaitable<out TResult> : Completable
    {
        TResult GetResult();

        Awaitable<TResult> GetAwaiter()
        {
            return this;
        }
    }
}