using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private FixedJoystick _moveJoystick;
        [SerializeField] private FixedJoystick _attackJoystick;
        [SerializeField] private Image _weaponSwitchButtonImage;

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