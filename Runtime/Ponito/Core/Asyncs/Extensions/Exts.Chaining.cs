using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Ponito.Core.Asyncs.Tasks;

namespace Ponito.Core.Asyncs.Extensions
{
    public static partial class Exts
    {
        public static bool IsFault(this PoTaskBase a)
        {
            return a.Exception != null;
        }
        
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async PoTask Inject(this PoTask a, Action act)
        {
            await a;
            act?.Invoke();
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async PoTask<T> Inject<T>(this PoTask<T> a, Action act)
        {
            var value = await a;
            act?.Invoke();
            return value;
        }

        /// <summary>
        ///     Catches when <see cref="PoTask"/> has exception
        /// </summary>
        /// <param name="a">the <see cref="PoTask"/></param>
        /// <param name="catcher"><see cref="Action{T}"/> invoked after <see cref="Exception"/></param>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async PoTask Try(this PoTask a, Action<Exception> catcher = null)
        {
            try
            {
                await a;
            }
            catch (Exception e)
            {
                if (catcher != null) catcher(e);
                else throw;
            }
        }

        /// <summary>
        ///     Catches when <see cref="PoTask{T}"/> has exception
        /// </summary>
        /// <param name="a">the <see cref="PoTask{T}"/></param>
        /// <param name="catcher"><see cref="Func{TResult}"/> invoked after <see cref="Exception"/> to give a return <see cref="T"/></param>
        /// <typeparam name="T">return type</typeparam>
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async PoTask<T> Try<T>(this PoTask<T> a, Func<Exception, T> catcher = null)
        {
            try
            {
                return await a;
            }
            catch (Exception e)
            {
                if (catcher != null) return catcher(e);
                throw;
            }
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async PoTask Chain(this PoTask a, Func<PoTask> factory)
        {
            await a;
            await factory();
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async PoTask Chain<T>(this PoTask<T> a, Func<PoTask> factory)
        {
            await a;
            await factory();
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async PoTask<T> Chain<T>(this PoTask a, Func<PoTask<T>> factory)
        {
            await a;
            return await factory();
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async PoTask<U> Chain<T, U>(this PoTask<T> a, Func<PoTask<U>> factory)
        {
            await a;
            return await factory();
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async PoTask Pass<T>(this PoTask<T> a, Func<T, PoTask> factory)
        {
            await factory(await a);
        }
        
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async PoTask<U> Pass<T, U>(this PoTask<T> a, Func<T, PoTask<U>> factory)
        {
            return await factory(await a);
        }
    }
}