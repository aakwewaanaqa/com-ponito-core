using System.Threading;

namespace Ponito.Core.Asyncs.Tasks
{
    internal readonly struct PlayerLoopHelper
    {
        private static readonly int mainThreadId;

        static PlayerLoopHelper()
        {
            mainThreadId = Thread.CurrentThread.ManagedThreadId;
        }

        public static bool IsMainThread => 
            mainThreadId == Thread.CurrentThread.ManagedThreadId;

        public static void AddAction(PlayerLoopTiming timing, PlayerLoopItem item) => 
            PlayerLoopRunner.Instance.Add(item);
    }
}