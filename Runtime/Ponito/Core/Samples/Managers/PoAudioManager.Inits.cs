using System.Linq;
using System.Reflection;
using Ponito.Core.Extensions;
using Ponito.Core.Samples.Units;
using UnityEngine;
using UnityEngine.Audio;

namespace Ponito.Core.Samples.Managers
{
    public partial class PoAudioManager
    {
        private static readonly SingletonUnit<PoAudioManager> singleton = new(true, i => i.Initialize());

        public static PoAudioManager Singleton => singleton.Instance;

        /// <summary>
        ///     Gets every field of [<see cref="SerializeField" />]s
        /// </summary>
        /// <returns></returns>
        private static IQueryable<FieldInfo> GetFields()
        {
            var flags = BindingFlags.NonPublic | BindingFlags.Instance;
            return typeof(PoAudioManager)
               .GetFields(flags)
               .Where(f => f.GetCustomAttribute<SerializeField>() is object)
               .AsQueryable();
        }

        private void Initialize()
        {
            var mixer = Resources.Load<AudioMixer>(nameof(PoAudioManager));
            foreach (var info in GetFields())
            {
                new GameObject(info.Name)
                   .EnsureComponent(out AudioSource source)
                   .SetParent(transform, true);

                info.SetValue(this, source);
                if (mixer.IsNull()) continue;
                var groups = mixer.FindMatchingGroups(info.Name);
                source.outputAudioMixerGroup = groups.Length > 0 ? groups[0] : default;
            }
        }
    }
}