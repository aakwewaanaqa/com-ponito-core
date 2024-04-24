using System.Runtime.CompilerServices;

namespace Ponito.Core.Asyncronized
{
    public readonly struct RunnerItem
    {
        public readonly Awaiter            awaiter;
        public readonly IAsyncStateMachine stateMachine;

        public RunnerItem(Awaiter awaiter, IAsyncStateMachine stateMachine)
        {
            this.awaiter      = awaiter;
            this.stateMachine = stateMachine;
        }

        public void Dispose()
        {
            
        }
    }
}