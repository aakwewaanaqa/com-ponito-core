using Ponito.Core.Asyncs.Compilations;
using Ponito.Core.Asyncs.Tasks.Movables;
using Ponito.Core.DebugHelper;

namespace Ponito.Core.Asyncs.Tasks
{
    public partial class PoTask
    {
        public static Yielder Yield()
        {
            // typeof(PoTask).F(nameof(Yield));
            return new Yielder();
        }
    }
}