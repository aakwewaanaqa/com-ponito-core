using System.Collections.Generic;

namespace Ponito.Core.Ease
{
    public class EasableManager : MonoSingleton<EasableManager>
    {
        protected override bool IsInitialized       => true;
        protected override bool IsDontDestroyOnLoad => true;

        protected override void Initialize()
        {
        }

        public void Run(Easable easable)
        {
            StartCoroutine(easable);
        }
    }
}