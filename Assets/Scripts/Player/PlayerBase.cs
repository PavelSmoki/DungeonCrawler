using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Gameplay;
using Game.Items.Armor;
using Game.Items.Weapons;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public abstract class PlayerBase : MonoBehaviour, IPlayer
    {
        [SerializeField] private Rigidbody2D _rb;
        
        [SerializeField] private WeaponBase _firstWeapon;
        [SerializeField] private WeaponBase _secondWeapon;
        [SerializeField] private Armor _head;
        [SerializeField] private Armor _body;
        [SerializeField] private Armor _foot;
        
        private WeaponBase _currentWeapon;

        private DiContainer _diContainer;
        private FixedJoystick _MoveJoystick;
        private FixedJoystick _AttackJoystick;
        
        private bool _isInvincible;

        protected readonly Stack<Heart> Hearts = new(10);
        [SerializeField] [Range(0.8f, 2f)] private float _speed;

        //

        [Inject]
        private void Construct(DiContainer diContainer, [Inject(Id = "MoveJoystick")] FixedJoystick moveJoystick,
            [Inject(Id = "AttackJoystick")] FixedJoystick attackJoystick)
        {
            _diContainer = diContainer;
            _MoveJoystick = moveJoystick;
            _AttackJoystick = attackJoystick;
            Init();
        }

        private void Init()
        {
            SetWeapon(_firstWeapon);
        }
        
        private void SetWeapon(WeaponBase weaponBase)
        {
            _currentWeapon = weaponBase;
        }

        private void FixedUpdate()
        {
            _rb.velocity = new Vector2(_MoveJoystick.Horizontal * 7 * _speed, _MoveJoystick.Vertical * 7 * _speed);


            var lookDirection = new Vector2(_AttackJoystick.Horizontal, _AttackJoystick.Vertical);
            var moveDirection = new Vector2(_MoveJoystick.Horizontal, _MoveJoystick.Vertical);
            

            if (lookDirection.Equals(Vector2.zero))
            {
                var toRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);
                _currentWeapon.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 180);
            }
            else
            {
                var toRotation = Quaternion.LookRotation(Vector3.forward, lookDirection);
                _currentWeapon.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 180);
                _currentWeapon.Attack();
            }
           
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
        
        public Vector2 GetCurrentPosition()
        {
            return transform.position;
        }
    }
}