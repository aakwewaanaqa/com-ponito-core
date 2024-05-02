using System.Runtime.CompilerServices;
using Ponito.Core.Promises.Compilations;

namespace Ponito.Core.Promises
{
    [AsyncMethodBuilder(typeof(PoTaskBuilder))]
    public readonly partial struct PoTask
    {
        private short        token  { get; }
        private PoTaskSource source { get; }
        
        public PoTask(PoTaskSource source, short token)
        {
            this.source = source;
            this.token  = token;
        }

        public PoTaskStatus Status => source?.GetStatus(token) ?? PoTaskStatus.Succeeded;
        
        public readonly struct Awaiter
        {
            
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