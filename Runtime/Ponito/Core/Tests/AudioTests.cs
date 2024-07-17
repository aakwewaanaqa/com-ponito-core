using System.Collections;
using NUnit.Framework;
using Ponito.Core.Extensions;
using Ponito.Core.Samples;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Ponito.Core.Tests
{
    public class AudioTests
    {
        private AudioClip GetAClip()
        {
            const string PATH = "Assets/ABs/Audios/fx-next-level.mp3";
            return AssetDatabase.LoadAssetAtPath<AudioClip>(PATH);
        }

        private AudioClip GetBClip()
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
            yield return new WaitForSeconds(PoAudioManager.DEFAULT_FADE_DURATION / 2f);
            
            _ = sound.Play(bClip);
            yield return new WaitForSeconds(PoAudioManager.DEFAULT_FADE_DURATION);

            var source = sound.GetSource();
            Assert.That(source.clip, Is.EqualTo(bClip));
            Assert.That(source.volume, Is.GreaterThan(0));
            
            yield return new WaitForSeconds(5f);
        }
    }
}