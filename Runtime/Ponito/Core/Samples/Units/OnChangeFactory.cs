using System.Threading;
using Ponito.Core.Asyncs.Tasks;
using Ponito.Core.Samples.UI;

namespace Ponito.Core.Samples.Units
{
    public delegate PoTask OnChangeFactory(Statable state, object args, CancellationToken ct);
}