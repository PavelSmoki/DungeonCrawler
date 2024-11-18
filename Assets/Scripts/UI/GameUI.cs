using System;
using System.Collections.Generic;
using System.Linq;
using Game.Items.Armor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private FloatingJoystick _moveJoystick;
        [SerializeField] private FixedJoystick _attackJoystick;
        [SerializeField] private Image _weaponSwitchButtonImage;
        [SerializeField] private Transform _hpRootTransform;
        [SerializeField] private Image _fullHeartImagePrefab;
        [SerializeField] private Sprite _halfHeartSprite;
        [SerializeField] private Image _headSlotImage;
        [SerializeField] private Image _bodySlotImage;
        [SerializeField] private Image _footSlotImage;
        [SerializeField] private Sprite _translucentSprite;
        
        [Header("Pause")]
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private Button _pauseButton;
        
        private readonly List<Image> _hearts = new();

        public Action OnWeaponSwitch;
        public Action<float, float> OnMove;
        public Action<Vector2, Vector2> OnAttack;

        public void OnWeaponSwitchPress()
        {
            OnWeaponSwitch?.Invoke();
        }

        public void SetWeaponSpite(Sprite sprite)
        {
            _weaponSwitchButtonImage.sprite = sprite;
        }

        public void SetArmorSprite(Sprite sprite, ArmorType armorType)
        {
            switch (armorType)
            {
                case ArmorType.Head:
                {
                    _headSlotImage.sprite = sprite != null ? sprite : _translucentSprite;
                    break;
                }
                case ArmorType.Body:
                {
                    _bodySlotImage.sprite = sprite != null ? sprite : _translucentSprite;
                    break;
                }
                case ArmorType.Foot:
                {
                    _footSlotImage.sprite = sprite != null ? sprite : _translucentSprite;
                    break;
                }
                default: return;
            }
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

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        } 

        public void Pause()
        {
            _pausePanel.SetActive(true);
            Time.timeScale = 0f;
            _pauseButton.interactable = false;
        }

        public void Resume()
        {
            _pausePanel.SetActive(false);
            Time.timeScale = 1f;
            _pauseButton.interactable = true;
        }

        public void ExitMainMenu()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}