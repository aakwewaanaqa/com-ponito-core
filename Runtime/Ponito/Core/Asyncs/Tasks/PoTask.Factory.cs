using Ponito.Core.Asyncs.Tasks.Sources;
using Ponito.Core.DebugHelper;

namespace Ponito.Core.Asyncs.Tasks
{
    public partial class PoTask
    {
        public static PoTask Yield()
        {
            typeof(PoTask).F(nameof(Yield));
            return new PoTask(false, new YieldMovable());
        }
    }
}