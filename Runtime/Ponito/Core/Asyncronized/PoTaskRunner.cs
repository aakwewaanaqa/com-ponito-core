namespace Ponito.Core.Asyncronized
{
    public class PoTaskRunner : MonoSingleton<PoTaskRunner>
    {
        protected override bool isInitialized       => true;
        protected override bool isDontDestroyOnLoad => true;
        protected override void Initialize()
        {
        }
    }
}