using System;
using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;

namespace Ponito.Core.AssetProvide
{
    public interface AsyncProvider : IDisposable
    {
        UniTask<T>      ProvideAsync<T>(object subKey) where T : Object;
        UniTask<string> MakePath(object subKey);
    }
}