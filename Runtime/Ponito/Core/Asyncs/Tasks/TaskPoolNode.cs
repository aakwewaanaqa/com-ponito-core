namespace Ponito.Core.Asyncs.Tasks
{
    public interface TaskPoolNode<T>
    {
        ref T NextNode { get; }
    }
}