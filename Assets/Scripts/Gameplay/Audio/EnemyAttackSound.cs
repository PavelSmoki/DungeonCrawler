using UnityEngine;

namespace Game.Gameplay.Audio
{
    public class EnemyAttackSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClipBundle _attackClipBundle;

        public bool IsPlaying => _audioSource != null && _audioSource.isPlaying;

        private AudioClip _previousClip;

        public void PlayAttack() => Play(_attackClipBundle);

        private void Play(AudioClipBundle bundle)
        {
            if (_audioSource == null || bundle == null)
            {
                return;
            }

            if (bundle.HasAnyClip())
            {
                _audioSource.volume = Random.Range(0.3f, 0.5f);
                _audioSource.pitch = Random.Range(0.95f, 1.05f);
                _audioSource.clip = bundle.GetRandomClip(_previousClip);
                _previousClip = _audioSource.clip;
                _audioSource.Play();
            }
        }
    }
}