namespace Ponito.Core.Samples.UI
{
    public interface StateWatcher
    {
        public void NotifyChange(Statable state, object args);
    }
}