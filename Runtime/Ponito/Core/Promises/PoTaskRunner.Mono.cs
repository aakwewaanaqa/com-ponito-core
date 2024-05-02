using System;

namespace Ponito.Core.Promises
{
    public partial class PoTaskRunner
    {
        protected override bool IsInitialized       => true;
        protected override bool IsDontDestroyOnLoad => true;

        protected override void Initialize()
        {
        }

        private void Update()
        {
            Run();
        }
    }
}