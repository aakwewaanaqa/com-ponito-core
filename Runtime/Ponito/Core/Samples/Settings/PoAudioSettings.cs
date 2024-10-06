using System;
using Ponito.Core.Samples.Audios;
using UnityEditor;
using UnityEngine;

namespace Ponito.Core.Samples.Settings
{
    [CreateAssetMenu(
        menuName = "Ponito/Core/Samples/Settings/Audio Settings",
        fileName = "settings po audio")
    ]
    public partial class PoAudioSettings : ScriptableObject
    {
        private static readonly Lazy<PoAudioSettings> instance = new(Create);

        public static PoAudioSettings Singleton => instance.Value;

        private static PoAudioSettings Create()
        {
            var instance = Resources.Load<PoAudioSettings>(SettingsDef.PO_SETTINGS_ASSET_PATH);
#if UNITY_EDITOR
            instance = AssetDatabase.LoadAssetAtPath<PoAudioSettings>(SettingsDef.PO_SETTINGS_ASSET_PATH);
            if (instance) return instance;

            instance = CreateInstance<PoAudioSettings>();
            AssetDatabase.CreateAsset(instance, SettingsDef.PO_SETTINGS_ASSET_PATH);
            AssetDatabase.SaveAssets();
#endif
            return instance;
        }
    }
}