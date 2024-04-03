using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Ponito.Core.AssetProvide;
using Ponito.Core.Extensions;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Ponito.Core.Samples
{
    public class ResourcesProvider : MonoSingleton<ResourcesProvider>, AsyncProvider
    {
        private static readonly Dictionary<object, ResourceRequest> tables = new();
        
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

        public async UniTask<string> MakePath(object subKey)
        {
            throw new InvalidOperationException();
        }
        
        public void Dispose()
        {
            Debug.Log("[ResourcesProvider] Resources.UnloadUnusedAssets()", this);
            Resources.UnloadUnusedAssets();
            tables.Clear();
        }

        protected override bool isInitialized       => true;
        protected override bool isDontDestroyOnLoad => false;
        protected override void Initialize()
        {
        }
    }
}