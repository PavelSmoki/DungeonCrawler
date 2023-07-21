using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Game.Gameplay;
using Game.Items.Armor;
using Game.Items.Weapons;
using ModestTree;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Player
{
    public abstract class PlayerBase : MonoBehaviour, IPlayer
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private int _speed;


        [SerializeField] private WeaponBase _firstWeapon;
        [SerializeField] private WeaponBase _secondWeapon;
        [SerializeField] private List<ArmorSlot> _defaultArmors;

        private Dictionary<ArmorType, Armor> _currentArmors;
        private WeaponBase _currentWeapon;

        private FixedJoystick _moveJoystick;
        private FixedJoystick _attackJoystick;
        private Button _weaponSwitchButton;

        private float _timeBeforeShoot;
        private bool _isInvincible;
        private bool _isAttackDelayed;
        private float _delayTemp;

        protected readonly Stack<Heart> Hearts = new(10);
        private PlayerModifiers _playerModifiers;
        [SerializeField] [Range(0.8f, 2f)] private float _defaultSpeedModifier;

        [Inject]
        private void Construct([Inject(Id = "MoveJoystick")] FixedJoystick moveJoystick,
            [Inject(Id = "AttackJoystick")] FixedJoystick attackJoystick, Button weaponSwitchButton)
        {
            _moveJoystick = moveJoystick;
            _attackJoystick = attackJoystick;
            _weaponSwitchButton = weaponSwitchButton;
            Init();
        }

        private void Init()
        {
            SetWeapon(_firstWeapon);
            SetDefaultArmors();
            _playerModifiers = new PlayerModifiers(GetSpeedModifier(), GetDamageModifier(), GetCritChanceModifier(),
                GetAttackSpeedModifier(), GetAttackRangeModifier(), GetShotSpeedModifier());
            _weaponSwitchButton.onClick.AddListener(SwitchWeapon);
            SetWeaponSpriteOnButton();
        }

        private void SwitchWeapon()
        {
            if (_firstWeapon == null || _secondWeapon == null) return;

            if (_currentWeapon == _firstWeapon)
            {
                SetWeapon(_secondWeapon);
                _firstWeapon.gameObject.SetActive(false);
                _secondWeapon.gameObject.SetActive(true);
                SetWeaponSpriteOnButton();
            }
            else
            {
                SetWeapon(_firstWeapon);
                _firstWeapon.gameObject.SetActive(true);
                _secondWeapon.gameObject.SetActive(false);
                SetWeaponSpriteOnButton();
            }
        }

        private void SetWeaponSpriteOnButton()
        {
            _weaponSwitchButton.transform.GetChild(0).GetComponent<Image>().sprite =
                _currentWeapon.GetComponentInChildren<SpriteRenderer>().sprite;
        }

        private void SetWeapon(WeaponBase weaponBase)
        {
            _currentWeapon = weaponBase;
        }

        private void SetDefaultArmors()
        {
            _currentArmors = new Dictionary<ArmorType, Armor>
            {
                [ArmorType.Head] = _defaultArmors.FirstOrDefault(armor => armor.ArmorType == ArmorType.Head)?.Armor,
                [ArmorType.Body] = _defaultArmors.FirstOrDefault(armor => armor.ArmorType == ArmorType.Body)?.Armor,
                [ArmorType.Foot] = _defaultArmors.FirstOrDefault(armor => armor.ArmorType == ArmorType.Foot)?.Armor
            };
        }

        private float GetSpeedModifier()
        {
            var bonus = _currentArmors.Where(armor => armor.Value != null)
                .Sum(armor => armor.Value.MoveSpeedBonus);
            return Mathf.Clamp(_defaultSpeedModifier + bonus, 0.8f, 2f);
        }

        private float GetDamageModifier()
        {
            var bonus = _currentArmors.Where(armor => armor.Value != null)
                .Sum(armor => armor.Value.DamageBonus);
            return bonus;
        }

        private float GetCritChanceModifier()
        {
            var bonus = _currentArmors.Where(armor => armor.Value != null)
                .Sum(armor => armor.Value.CritChanceBonus);
            return bonus;
        }

        private float GetAttackSpeedModifier()
        {
            var bonus = _currentArmors.Where(armor => armor.Value != null)
                .Sum(armor => armor.Value.AttackSpeedBonus);
            return bonus;
        }

        private float GetAttackRangeModifier()
        {
            var bonus = _currentArmors.Where(armor => armor.Value != null)
                .Sum(armor => armor.Value.AttackRangeBonus);
            return bonus;
        }

        private float GetShotSpeedModifier()
        {
            var bonus = _currentArmors.Where(armor => armor.Value != null)
                .Sum(armor => armor.Value.ShotSpeedBonus);
            return bonus;
        }

        private void FixedUpdate()
        {
            _rb.velocity = new Vector2(_moveJoystick.Horizontal * _speed * _defaultSpeedModifier,
                _moveJoystick.Vertical * _speed * _defaultSpeedModifier);
        }

        private void Update()
        {
            var lookDirection = new Vector2(_attackJoystick.Horizontal, _attackJoystick.Vertical);
            var moveDirection = new Vector2(_moveJoystick.Horizontal, _moveJoystick.Vertical);

            if (_timeBeforeShoot > 0)
            {
                _timeBeforeShoot -= Time.deltaTime;
            }

            if (lookDirection.Equals(Vector2.zero))
            {
                var toRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);
                _currentWeapon.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 180);
            }
            else
            {
                var toRotation = Quaternion.LookRotation(Vector3.forward, lookDirection);
                _currentWeapon.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 180);
                AttackProcessing();
            }
        }

        private void AttackProcessing()
        {
            if (_timeBeforeShoot <= 0)
            {
                _currentWeapon.Attack(_playerModifiers.DamageModifier, _playerModifiers.CritChanceModifier,
                    _playerModifiers.AttackRangeModifier, _playerModifiers.ShotSpeedModifier);
                _timeBeforeShoot = 1 / (_playerModifiers.AttackSpeedModifier * _currentWeapon.AttackSpeed);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            TakeDamage(other);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            TakeDamage(other);
        }

        private void TakeDamage(Collision2D other)
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

        Vector2 IPlayer.GetCurrentPosition()
        {
            return transform.position;
        }
    }
}