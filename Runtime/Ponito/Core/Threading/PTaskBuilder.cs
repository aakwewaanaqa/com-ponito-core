using Ponito.Core.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
    public struct PTaskBuilder
    {
        public PTask Task { get; private set; }
        
        public static PTaskBuilder Create()
        {
            return default;
        }

        public void SetException(Exception ex)
        {
        }

        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }

        public void SetResult()
        {
        }

        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
        }

        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter,
            ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            AwaitOnCompleted(ref awaiter, ref stateMachine);
        }

        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
        }
    }
}