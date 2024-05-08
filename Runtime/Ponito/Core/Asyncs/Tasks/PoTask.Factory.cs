using Ponito.Core.Asyncs.Tasks.Movables;

namespace Ponito.Core.Asyncs.Tasks
{
    public partial class PoTask
    {
        public static Yielder Yield()
        {
            // typeof(PoTask).F(nameof(Yield));
            return new Yielder();
        }

        public static Delayer Delay(int milliseconds)
        {
            return new Delayer(milliseconds);
        }
    }
}