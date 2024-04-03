using System;

namespace Ponito.Core.Samples.UI
{
    /// <summary>
    ///     Blocks all <see cref="PoButton"/> by using this scope in the scope.
    ///     Please remember to <see cref="Dispose"/> or all <see cref="PoButton"/>s remain disabled. 
    /// </summary>
    public class PoButtonBlockScope : IDisposable
    {
        private static uint stack;

        public PoButtonBlockScope()
        {
            stack++;
        }

        public static bool IsBlock => stack > 0;

        public void Dispose()
        {
            stack--;
        }
    }
}