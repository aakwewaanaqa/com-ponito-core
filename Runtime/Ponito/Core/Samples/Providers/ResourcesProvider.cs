using System;
using System.Collections.Generic;
using Ponito.Core.Asyncs.Tasks;
using Ponito.Core.Extensions;
using UnityEngine;

namespace Ponito.Core.Samples.Providers
{
    [AddComponentMenu("Ponito/Core/Samples/Resources Provider")]
    public class ResourcesProvider : MonoSingleton<ResourcesProvider>, AsyncProvider
    {
        private static readonly Dictionary<object, ResourceRequest> tables = new();

        /// <inheritdoc />
        protected override bool IsInitialized => true;

        /// <inheritdoc />
        protected override bool IsDontDestroyOnLoad => false;

        /// <inheritdoc />
        public async PoTask<T> ProvideAsync<T>(object subKey)
        {
            if (tables.TryGetValue(subKey, out var request))
            {
                while (!request.isDone) await PoTask.Yield();

                if (!request.asset.IsObject() || request.asset is not T t)
                    throw new InvalidCastException(nameof(ProvideAsync));

                return t;
            }
            else
            {
                request = Resources.LoadAsync(subKey.ToString(), typeof(T));
                while (!request.isDone) await PoTask.Yield();

                if (!request.asset.IsObject() || request.asset is not T t)
                    throw new InvalidCastException(nameof(ProvideAsync));

                tables.Add(subKey, request);
                return t;
            }
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        /// <inheritdoc />
        public async PoTask<string> MakePath(object subKey)
        {
            throw new InvalidOperationException();
        }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

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