namespace Ponito.Core.Asyncs.Tasks
{
    internal static class PlayerLoopHelper
    {
        public static void AddAction(PlayerLoopTiming timing, PlayerLoopItem item)
        {
            PlayerLoopRunner.Instance.Add(item);
        }
    }
}