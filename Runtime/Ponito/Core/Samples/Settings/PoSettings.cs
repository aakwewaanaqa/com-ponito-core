using System;
using Ponito.Core.Extensions;
using Ponito.Core.Paths;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Ponito.Core.Samples.Settings
{
    public class PoSettings : ScriptableObject
    {
        private static readonly Lazy<PoSettings> instance = new(Create);

        public static PoSettings Singleton => instance.Value;

        private static PoSettings Create()
        {
            var instance = Resources.Load<PoSettings>(SettingsDef.PO_AUDIO_SETTINGS_RESOURCE_PATH);
#if UNITY_EDITOR
            if (instance.IsObject()) return instance;
            instance = AssetDatabase.LoadAssetAtPath<PoSettings>(SettingsDef.PO_SETTINGS_ASSET_PATH);
            if (instance.IsObject()) return instance;

            instance = CreateInstance<PoSettings>();
            AssetDatabase.CreateAsset(instance, SettingsDef.PO_SETTINGS_ASSET_PATH);
            AssetDatabase.SaveAssets();
#endif
            return instance;
        }
    }
}