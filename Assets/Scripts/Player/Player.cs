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
    public class Player : MonoBehaviour, IPlayer
    {
        private const string EnemyTag = "Enemy";
        private const int Speed = 7;
        private const string EnemyProjectile = "EnemyProjectile";
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int Damaged = Animator.StringToHash("Damaged");
        private static readonly int Died = Animator.StringToHash("Died");

        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Animator _animator;
        [SerializeField] private Camera _camera;
        [SerializeField] private int _heartsCount;
        [SerializeField] private Transform _weaponPosTransform;
        [SerializeField] private Transform _spriteTransform;


        [SerializeField] private WeaponBase _firstWeapon;
        [SerializeField] private WeaponBase _secondWeapon;
        [SerializeField] private List<ArmorSlot> _defaultArmors;

        [SerializeField] [Range(0.2f, 2f)] private float _defaultSpeedModifier;
        private readonly Stack<Heart> _hearts = new(10);

        private GameUI _gameUI;
        private GameOverUI _gameOverUI;

        private Dictionary<ArmorType, Armor> _currentArmors;
        private WeaponBase _currentWeapon;

        private PlayerModifiers _playerModifiers;

        private float _timeBeforeShoot;
        private bool _isInvincible;

        [Inject]
        private void Construct(GameUI gameUI, GameOverUI gameOverUI)
        {
            _gameOverUI = gameOverUI;
            _gameUI = gameUI;
        }

        private void Start()
        {
            for (var i = 0; i < _heartsCount; i++)
            {
                _hearts.Push(new Heart(2));
            }
            Init();
        }

        private void Init()
        {
            SetWeapon(_firstWeapon);
            SetDefaultArmors();

            CalculateModifiers();
            
            InitGameUI();
        }

        private void InitGameUI()
        {
            _gameUI.OnWeaponSwitch += SwitchWeapon;
            _gameUI.OnMove += Move;
            _gameUI.OnAttack += Attack;
            _gameUI.SetHealthOnUI(_hearts.Count);
            _gameUI.SetWeaponSpite(_currentWeapon.GetComponentInChildren<SpriteRenderer>().sprite);
            _gameUI.SetArmorSprite(_currentArmors[ArmorType.Head]?.SpriteRenderer.sprite, ArmorType.Head);
            _gameUI.SetArmorSprite(_currentArmors[ArmorType.Body]?.SpriteRenderer.sprite, ArmorType.Body);
            _gameUI.SetArmorSprite(_currentArmors[ArmorType.Foot]?.SpriteRenderer.sprite, ArmorType.Foot);
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
            _gameUI.SetWeaponSpite(_currentWeapon.GetComponentInChildren<SpriteRenderer>().sprite);
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

        private void CalculateModifiers()
        {
            _playerModifiers = new PlayerModifiers(GetSpeedModifier(), GetDamageModifier(), GetCritChanceModifier(),
                GetAttackSpeedModifier(), GetAttackRangeModifier(), GetShotSpeedModifier());
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
            if (horizontal != 0 || vertical != 0) RotatePlayer(horizontal);
            _rb.velocity = new Vector2(horizontal * Speed * _playerModifiers.SpeedModifier,
                vertical * Speed * _playerModifiers.SpeedModifier);
            _animator.SetBool(IsRunning, _rb.velocity.sqrMagnitude >= 0.1f);
        }

        private void RotatePlayer(float horizontal)
        {
            var rot = _spriteTransform.rotation;
            if (horizontal < 0)
            {
                rot.y = 180;
                _spriteTransform.rotation = rot;
            }
            else
            {
                rot.y = 0;
                _spriteTransform.rotation = rot;
            }
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
        
        private void AttackProcessing()
        {
            if (_timeBeforeShoot <= 0)
            {
                _currentWeapon.Attack(_playerModifiers.DamageModifier, _playerModifiers.CritChanceModifier,
                    _playerModifiers.AttackRangeModifier, _playerModifiers.ShotSpeedModifier);

                _timeBeforeShoot = 1 / (_playerModifiers.AttackSpeedModifier * _currentWeapon.AttackSpeed);
            }
        }
        
        private void RotateWeapon(Vector2 direction)
        {
            var toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            _currentWeapon.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 180);
        }

        private void OnCollisionEnter2D(Collision2D other) => TakeDamage(other);

        private void OnCollisionStay2D(Collision2D other) => TakeDamage(other);

        private void TakeDamage(Collision2D other)
        {
            if ((other.gameObject.CompareTag(EnemyTag) || other.gameObject.CompareTag(EnemyProjectile)) && !_isInvincible)
            {
                var lastHeart = _hearts.Peek();
                lastHeart.TakeDamage();
                _gameUI.ChangeHealthOnUI(lastHeart.Amount);
                if (lastHeart.Amount <= 0)
                {
                    _hearts.Pop();
                    if (_hearts.IsEmpty())
                    {
                        _animator.SetTrigger(Died);
                        _gameOverUI.ShowLoseScreen().Forget();
                    }
                }
                
                _animator.SetTrigger(Damaged);
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

        Camera IPlayer.GetCamera()
        {
            return _camera;
        }

        WeaponBase IPlayer.TakeItem(WeaponBase item)
        {
            if (_secondWeapon == null)
            {
                _secondWeapon = item;
                _secondWeapon.transform.SetParent(_weaponPosTransform);
                _secondWeapon.transform.position = _weaponPosTransform.position;
                SetWeapon(_secondWeapon);
                return null;
            }

            WeaponBase dressedWeapon;
            if (_currentWeapon == _firstWeapon)
            {
                dressedWeapon = _firstWeapon;
                _firstWeapon = item;
                _firstWeapon.transform.SetParent(_weaponPosTransform);
                _firstWeapon.transform.position = _weaponPosTransform.position;
                SetWeapon(_firstWeapon);
            }
            else
            {
                dressedWeapon = _secondWeapon;
                _secondWeapon = item;
                _secondWeapon.transform.SetParent(_weaponPosTransform);
                _secondWeapon.transform.position = _weaponPosTransform.position;
                SetWeapon(_secondWeapon);
            }

            return dressedWeapon;
        }

        Armor IPlayer.TakeItem(Armor item)
        {
            Armor dressedArmor;
            switch (item.ArmorType)
            {
                case ArmorType.Head:
                {
                    dressedArmor = SwitchArmor(item, ArmorType.Head);
                    break;
                }
                case ArmorType.Body:
                {
                    dressedArmor = SwitchArmor(item, ArmorType.Body);
                    break;  
                }
                case ArmorType.Foot:
                {
                    dressedArmor = SwitchArmor(item, ArmorType.Foot);
                    break;
                }
                default: return null;
            }

            CalculateModifiers();
            return dressedArmor;
        }

        private Armor SwitchArmor(Armor item, ArmorType armorType)
        {
            var dressedArmor = _currentArmors[armorType];
            _currentArmors[armorType] = item;
            _currentArmors[armorType].transform.SetParent(transform);
            _currentArmors[armorType].SpriteRenderer.enabled = false;
            _gameUI.SetArmorSprite(_currentArmors[armorType].SpriteRenderer.sprite, armorType);
            return dressedArmor;
        }
        private void OnDestroy()
        {
            _gameUI.OnWeaponSwitch -= SwitchWeapon;
            _gameUI.OnMove -= Move;
            _gameUI.OnAttack -= Attack;
        }
    }
}