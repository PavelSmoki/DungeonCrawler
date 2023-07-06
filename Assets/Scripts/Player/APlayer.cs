using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Game
{
    public abstract class APlayer : MonoBehaviour, IPlayer
    {
        [SerializeField] private Rigidbody2D _rb;
        private FixedJoystick _joystick;
        private bool _isInvincible;

        protected readonly Stack<Heart> Hearts = new(10);

        [SerializeField] [Range(0.8f, 2f)] private float _speed;
        //


        [Inject]
        private void Construct(FixedJoystick joystick)
        {
            _joystick = joystick;
        }

        private void FixedUpdate()
        {
            _rb.velocity = new Vector2(_joystick.Horizontal * 8 * _speed, _joystick.Vertical * 8 * _speed);
        }

        public Vector2 GetCurrentPosition()
        {
            return transform.position;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Enemy") && !_isInvincible)
            {
                var lastHeart = Hearts.Peek();
                lastHeart.TakeDamage();
                Debug.Log("Damage Taken!");
                if (lastHeart.Amount <= 0)
                {
                    Hearts.Pop();
                    if (Hearts.IsEmpty())
                    {
                        Time.timeScale = 0;
                    }

                    Debug.Log("Heart Destroyed!");
                }
                TakingDamageDelay().Forget();
            }
        }

        private async UniTaskVoid TakingDamageDelay()
        {
            _isInvincible = true;
            await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
            _isInvincible = false;
        }
    }
}