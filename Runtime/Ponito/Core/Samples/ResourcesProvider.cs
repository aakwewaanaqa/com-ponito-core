using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Ponito.Core.AssetProvide;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ponito.Core.Samples
{
    [AddComponentMenu("Ponito/Core/Samples/Resources Provider")]
    public class ResourcesProvider : MonoSingleton<ResourcesProvider>, AsyncProvider
    {
        private static readonly Dictionary<object, ResourceRequest> tables = new();

        /// <inheritdoc />
        protected override bool isInitialized => true;

        /// <inheritdoc />
        protected override bool isDontDestroyOnLoad => false;

        /// <inheritdoc />
        public async UniTask<T> ProvideAsync<T>(object subKey) where T : Object
        {
            if (tables.TryGetValue(subKey, out var request))
            {
                await request;
                return (T)request.asset;
            }

            request = Resources.LoadAsync<T>(subKey.ToString());
            tables.Add(subKey, request);
            await request;
            return (T)request.asset;
        }

        /// <inheritdoc />
        public async UniTask<string> MakePath(object subKey)
        {
            throw new InvalidOperationException();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Debug.Log("[ResourcesProvider] Resources.UnloadUnusedAssets()", this);
            Resources.UnloadUnusedAssets();
            tables.Clear();
        }

        /// <inheritdoc />
        protected override void Initialize()
        {
        }
    }
}