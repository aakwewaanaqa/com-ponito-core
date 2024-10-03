using Ponito.Core.Samples.Units;

namespace Ponito.Core.Samples.Managers
{
    public partial class PoStateManager
    {
        private static readonly SingletonUnit<PoStateManager> unit = new(true);
        public static           PoStateManager                Singleton => unit.Instance;
    }
}