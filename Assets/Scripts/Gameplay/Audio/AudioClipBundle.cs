using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Gameplay.Audio
{
    [CreateAssetMenu(fileName = "new Audio Clip Bundle", menuName = "Tools/Audio Clip Bundle")]
    public class AudioClipBundle : ScriptableObject
    {
        [SerializeField] private AudioClip[] _clips = Array.Empty<AudioClip>();

        public bool HasAnyClip()
        {
            return _clips is { Length: > 0 };
        }

        private AudioClip GetIndexedClip(int index)
        {
            return HasAnyClip() ? _clips[index % _clips.Length] : null;
        }
        
        public AudioClip GetRandomClip(AudioClip previousClip = null)
        {
            var randomIndex = Random.Range(0, _clips.Length);
            var result = GetIndexedClip(randomIndex);

            if (previousClip != null && result == previousClip)
            {
                result = GetIndexedClip(randomIndex + 1);
            }

            return result;
        }
    }
}