using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private FixedJoystick _moveJoystick;
        [SerializeField] private FixedJoystick _attackJoystick;
        [SerializeField] private Image _weaponSwitchButtonImage;
        [SerializeField] private Transform _hpRootTransform;
        [SerializeField] private Image _fullHeartImagePrefab;
        [SerializeField] private Sprite _halfHeartSprite;
        
        private readonly List<Image> _hearts = new();

        public Action OnWeaponSwitch;
        public Action<float, float> OnMove;
        public Action<Vector2, Vector2> OnAttack;

        public void OnWeaponSwitchPress()
        {
            OnWeaponSwitch?.Invoke();
        }

        public void SetWeaponImage(Sprite sprite)
        {
            _weaponSwitchButtonImage.sprite = sprite;
        }

        public void ChangeHealthOnUI(int lastHeartAmount)
        {
            if (lastHeartAmount == 1)
            {
                _hearts.Last().sprite = _halfHeartSprite;
                return;
            }
            Destroy(_hearts.Last().gameObject);
            _hearts.RemoveAt(_hearts.Count - 1);
        }

        public void SetHealthOnUI(int heartsCount)
        {
            for (var i = 0; i < heartsCount; i++)
            {
                _hearts.Add(Instantiate(_fullHeartImagePrefab, _hpRootTransform).GetComponent<Image>());
            }
        }

        private void FixedUpdate()
        {
            OnMove?.Invoke(_moveJoystick.Horizontal, _moveJoystick.Vertical);
        }

        private void Update()
        {
            var lookDirection = new Vector2(_attackJoystick.Horizontal, _attackJoystick.Vertical);
            var moveDirection = new Vector2(_moveJoystick.Horizontal, _moveJoystick.Vertical);
            OnAttack?.Invoke(moveDirection, lookDirection);
        }
    }
}