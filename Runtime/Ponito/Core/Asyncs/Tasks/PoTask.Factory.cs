using Ponito.Core.Asyncs.Tasks.Sources;
using Ponito.Core.DebugHelper;

namespace Ponito.Core.Asyncs.Tasks
{
    public readonly partial struct PoTask
    {
        public static PoTask Yield()
        {
            typeof(PoTask).F(nameof(Yield));
            return new PoTask(new YieldMovable(), 0);
        }
    }
}