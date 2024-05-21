using System;
using Ponito.Core.Asyncs.Tasks;

namespace Ponito.Core.Asyncs.Extensions
{
    public static partial class Exts
    {
        public static async PoTask Inject(this PoTask a, Action act)
        {
            await a;
            act();
        }

        public static async PoTask<T> Inject<T>(this PoTask<T> a, Action act)
        {
            var value = await a;
            act();
            return value;
        }

        public static async PoTask Catch(this PoTask a, Action<Exception> catcher = null)
        {
            await a;

            if (a.ex != null)
            {
                if (catcher != null) catcher(a.ex);
                else throw a.ex;
            }
        }

        public static async PoTask Chain(this PoTask a, Func<PoTask> factory)
        {
            await a;
            await factory();
        }

        public static async PoTask Chain<T>(this PoTask<T> a, Func<PoTask> factory)
        {
            await a;
            await factory();
        }

        public static async PoTask<T> Chain<T>(this PoTask a, Func<PoTask<T>> factory)
        {
            await a;
            return await factory();
        }

        public static async PoTask<U> Chain<T, U>(this PoTask<T> a, Func<PoTask<U>> factory)
        {
            await a;
            return await factory();
        }

        public static async PoTask Pass<T>(this PoTask<T> a, Func<T, PoTask> factory)
        {
            await factory(await a);
        }
        
        public static async PoTask<U> Pass<T, U>(this PoTask<T> a, Func<T, PoTask<U>> factory)
        {
            return await factory(await a);
        }
    }
}