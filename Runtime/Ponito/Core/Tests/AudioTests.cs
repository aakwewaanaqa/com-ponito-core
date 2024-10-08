#if UNITY_EDITOR
using System.Collections;
using NUnit.Framework;
using Ponito.Core.Asyncs.Extensions;
using Ponito.Core.Extensions;
using Ponito.Core.Samples.Audios;
using Ponito.Core.Samples.Managers;
using Ponito.Core.Samples.Settings;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Ponito.Core.Tests
{
    public class AudioTests
    {
        private static AudioClip GetAClip()
        {
            const string PATH = "Assets/ABs/Audios/fx-next-level.mp3";
            return AssetDatabase.LoadAssetAtPath<AudioClip>(PATH);
        }

        private static AudioClip GetBClip()
        {
            const string PATH = "Assets/ABs/Audios/fx-applaud.mp3";
            return AssetDatabase.LoadAssetAtPath<AudioClip>(PATH);
        }

        [UnityTest]
        public IEnumerator Play()
        {
            new GameObject().EnsureComponent(out AudioListener _);

            var bClip = GetBClip();
            var aClip = GetAClip();

            var sound = PoAudioManager.Singleton;
            _ = sound.Play(aClip);
            yield return (PoAudioManager.DEFAULT_FADE_DURATION / 2f).Delay().WaitAsCoroutine();

            _ = sound.Play(bClip);
            yield return new WaitForSeconds(PoAudioManager.DEFAULT_FADE_DURATION);

            var source = sound.GetSource();
            Assert.That(source.clip,   Is.EqualTo(bClip));
            Assert.That(source.volume, Is.GreaterThan(0));

            yield return new WaitForSeconds(5f);
        }

        [UnityTest]
        public IEnumerator GetCue()
        {
            var settings = PoAudioSettings.Singleton;
            Assert.That(settings, Is.Not.Null);
            Assert.That(settings.uiBinds, Is.Not.Null);
            Assert.That(settings.uiBinds.Count, Is.GreaterThan(0));
            var cue = settings.GetCue(CueType.UI, "button big");
            Assert.That(cue.clip, Is.Not.Null);
            yield break;
        }
    }
}
#endif