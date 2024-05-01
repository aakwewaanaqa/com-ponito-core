using System;

namespace Ponito.Core.Promises
{
    public partial class PoTaskRunner
    {
        protected override bool isInitialized       => true;
        protected override bool isDontDestroyOnLoad => true;

        protected override void Initialize()
        {
        }

        private void Update()
        {
            Run();
        }
    }
}