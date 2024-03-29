using System;

namespace Ponito.Core.UI
{
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