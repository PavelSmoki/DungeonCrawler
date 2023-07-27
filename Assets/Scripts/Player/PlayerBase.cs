using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Game.Gameplay;
using Game.Items.Armor;
using Game.Items.Weapons;
using Game.UI;
using ModestTree;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public abstract class PlayerBase : MonoBehaviour, IPlayer
    {
        private const string EnemyTag = "Enemy";

        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private int _speed;

        [SerializeField] private WeaponBase _firstWeapon;
        [SerializeField] private WeaponBase _secondWeapon;
        [SerializeField] private List<ArmorSlot> _defaultArmors;

        private GameUI _gameUI;

        private Dictionary<ArmorType, Armor> _currentArmors;
        private WeaponBase _currentWeapon;

        private float _timeBeforeShoot;
        private bool _isInvincible;

        protected readonly Stack<Heart> Hearts = new(10);
        private PlayerModifiers _playerModifiers;
        [SerializeField] [Range(0.8f, 2f)] private float _defaultSpeedModifier;

        [Inject]
        private void Construct(GameUI gameUI)
        {
            _gameUI = gameUI;
        }

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            SetWeapon(_firstWeapon);
            SetDefaultArmors();

            _playerModifiers = new PlayerModifiers(GetSpeedModifier(), GetDamageModifier(), GetCritChanceModifier(),
                GetAttackSpeedModifier(), GetAttackRangeModifier(), GetShotSpeedModifier());

            _gameUI.OnWeaponSwitch += SwitchWeapon;
            _gameUI.OnMove += Move;
            _gameUI.OnAttack += Attack;
            _gameUI.SetWeaponImage(_currentWeapon.GetComponentInChildren<SpriteRenderer>().sprite);
        }

        private void SwitchWeapon()
        {
            if (_firstWeapon == null || _secondWeapon == null) return;

            if (_currentWeapon == _firstWeapon)
            {
                SetWeapon(_secondWeapon);
                _firstWeapon.gameObject.SetActive(false);
                _secondWeapon.gameObject.SetActive(true);
            }
            else
            {
                SetWeapon(_firstWeapon);
                _firstWeapon.gameObject.SetActive(true);
                _secondWeapon.gameObject.SetActive(false);
            }
        }

        private void SetWeapon(WeaponBase weaponBase)
        {
            _currentWeapon = weaponBase;
            _gameUI.SetWeaponImage(_currentWeapon.GetComponentInChildren<SpriteRenderer>().sprite);
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

        private void Move(float horizontal, float vertical)
        {
            _rb.velocity = new Vector2(horizontal * _speed * _defaultSpeedModifier,
                vertical * _speed * _defaultSpeedModifier);
        }

        private void Attack(Vector2 moveDirection, Vector2 lookDirection)
        {
            AttackTick();

            if (lookDirection.Equals(Vector2.zero))
            {
                RotateWeapon(moveDirection);
            }
            else
            {
                RotateWeapon(lookDirection);
                AttackProcessing();
            }
        }

        private void AttackTick()
        {
            if (_timeBeforeShoot > 0)
            {
                _timeBeforeShoot -= Time.deltaTime;
            }
        }

        private void RotateWeapon(Vector2 direction)
        {
            var toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            _currentWeapon.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 180);
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

        private void OnCollisionEnter2D(Collision2D other) => TakeDamage(other);

        private void OnCollisionStay2D(Collision2D other) => TakeDamage(other);

        private void TakeDamage(Collision2D other)
        {
            if (other.gameObject.CompareTag(EnemyTag) && !_isInvincible)
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
        
        GameObject IPlayer.TakeItem(WeaponBase item)
        {
            if (item.GetType() == typeof(WeaponBase))
            {
                if (_secondWeapon == null)
                {
                    _secondWeapon = item.GetComponent<WeaponBase>();
                    SetWeapon(_secondWeapon);
                    return null;
                }
                if (_currentWeapon == _firstWeapon)
                {
                    _firstWeapon = item.GetComponent<WeaponBase>();
                    SetWeapon(_firstWeapon);
                }
                else
                {
                    _secondWeapon = item.GetComponent<WeaponBase>();
                    SetWeapon(_secondWeapon);
                }

                return _currentWeapon.gameObject;
            }
            
            var pickedArmor = item.GetComponent<Armor>();
            switch (pickedArmor.ArmorType)
            {
                case ArmorType.Head:
                {
                    var dressedArmor = _currentArmors[ArmorType.Head].gameObject;
                    _currentArmors[ArmorType.Head] = pickedArmor;
                    return dressedArmor;
                }
                case ArmorType.Body:
                {
                    var dressedArmor = _currentArmors[ArmorType.Body].gameObject;
                    _currentArmors[ArmorType.Body] = pickedArmor;
                    return dressedArmor;
                }
                case ArmorType.Foot:
                {
                    var dressedArmor = _currentArmors[ArmorType.Foot].gameObject;
                    _currentArmors[ArmorType.Foot] = pickedArmor;
                    return dressedArmor;
                }
            }
            return null;
        }

        GameObject IPlayer.TakeItem(Armor item)
        {
            return null;
        }
    }
}