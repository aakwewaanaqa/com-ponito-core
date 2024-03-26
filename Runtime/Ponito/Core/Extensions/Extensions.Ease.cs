using Cysharp.Threading.Tasks;
using Ponito.Core.Ease;

namespace Ponito.Core.Extensions
{
    public static partial class Extensions
    {
        public static async UniTask Ease(this Setter<float> setter, float from, float to, float duration)
        {
            var rule = new EaseMover(duration);
            while (rule.MoveNext())
            {
                setter.Invoke(rule.Current);
                await UniTask.Yield();
            }
        }
    }
}