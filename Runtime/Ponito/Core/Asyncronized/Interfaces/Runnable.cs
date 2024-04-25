using Ponito.Core.Asyncronized.Enums;

namespace Ponito.Core.Asyncronized.Interfaces
{
    public interface Runnable : Completable
    {
        RunMark Mark { get; }
        bool    Run();
    }
}