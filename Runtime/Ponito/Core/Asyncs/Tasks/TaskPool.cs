using System.Threading;

namespace Ponito.Core.Asyncs.Tasks
{
    internal struct TaskPool<T> where T : class, TaskPoolNode<T>
    {
        private const int MAX_POOL_SIZE = int.MaxValue;
        
        private int gate;
        private int size;
        private T   root;

        public int Size => size;

        public bool TryPop(out T result)
        {
            if (Interlocked.CompareExchange(ref gate, 1, 0) == 0)
            {
                var v = root;
                if (!(v is null))
                {
                    ref var nextNode = ref v.NextNode;
                    root     = nextNode;
                    nextNode = null;
                    size--;
                    result = v;
                    Volatile.Write(ref gate, 0);
                    return true;
                }

                Volatile.Write(ref gate, 0);
            }
            result = default;
            return false;
        }

        public bool TryPush(T item)
        {
            if (Interlocked.CompareExchange(ref gate, 1, 0) == 0)
            {
                if (size < MAX_POOL_SIZE)
                {
                    item.NextNode = root;
                    root          = item;
                    size++;
                    Volatile.Write(ref gate, 0);
                    return true;
                }
                else
                {
                    Volatile.Write(ref gate, 0);
                }
            }
            return false;
        }
    }
}