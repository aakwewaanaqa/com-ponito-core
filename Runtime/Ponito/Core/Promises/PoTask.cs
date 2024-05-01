using System.Runtime.CompilerServices;
using Ponito.Core.Promises.Compilations;

namespace Ponito.Core.Promises
{
    [AsyncMethodBuilder(typeof(PoTaskBuilder))]
    public readonly struct PoTask
    {
        private PoTaskSource source { get; }
        
        public PoTask(PoTaskSource source)
        {
            this.source = source;
        }
    }
    
    [AsyncMethodBuilder(typeof(PoTaskBuilder<>))]
    public readonly struct PoTask<T>
    {
        private PoTaskSource source { get; }
        
        public PoTask(PoTaskSource source)
        {
            this.source = source;
        }
    }
}