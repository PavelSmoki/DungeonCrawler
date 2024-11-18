using UnityEngine;

namespace Game.Gameplay.Audio
{
    public class AttackSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClipBundle _attackClipsBundle;

        public bool IsPlaying => _audioSource != null && _audioSource.isPlaying;
        
        private AudioClip _previousClip;
        
        public void PlayAttack() => Play(_attackClipsBundle);

        private void Play(AudioClipBundle bundle)
        {
            if (_audioSource == null || bundle == null)
            {
                return;
            }

            if (bundle.HasAnyClip())
            {
                _audioSource.volume = Random.Range(0.15f, 0.30f);
                _audioSource.pitch = Random.Range(0.8f, 1.1f);
                _audioSource.clip = bundle.GetRandomClip(_previousClip);
                _previousClip = _audioSource.clip;
                _audioSource.Play();
            }
        }
    }
}   