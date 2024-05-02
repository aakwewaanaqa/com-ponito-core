namespace Ponito.Core.Asyncs.Tasks
{
    public partial class PlayerLoopRunner
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