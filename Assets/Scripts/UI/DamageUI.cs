using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Player;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class DamageUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textDamagePrefab;
        [SerializeField] private TextMeshProUGUI _critTextDamagePrefab;

        private readonly List<ActiveText> _activeTexts = new();
        private IPlayer _player;

        [Inject]
        private void Construct(IPlayer player)
        {
            _player = player;
        }

        public void ShowDamage(int damage, Vector2 unitPos, bool isCrit)
        {
            var textDamage = Instantiate(isCrit ? _critTextDamagePrefab : _textDamagePrefab, transform);
            textDamage.text = damage.ToString();
            SelfDestroy(textDamage.gameObject).Forget();

            var at = new ActiveText { MaxTime = 1.0f };
            at.Timer = at.MaxTime;
            at.UIText = textDamage;
            at.UnitPosition = unitPos + Vector2.up;

            at.MoveText(_player.GetCamera());
            _activeTexts.Add(at);
        }

        private async UniTaskVoid SelfDestroy(GameObject textDamage)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            Destroy(textDamage);
        }

        private void Update()
        {
            for (var i = 0; i < _activeTexts.Count; i++)
            {
                var at = _activeTexts[i];
                at.Timer -= Time.deltaTime;

                if (at.Timer <= 0.0f)
                {
                    _activeTexts.RemoveAt(i);
                    --i;
                }
                else
                {
                    var color = at.UIText.color;
                    color.a = at.Timer / at.MaxTime;
                    at.UIText.color = color;

                    at.MoveText(_player.GetCamera());
                }
            }
        }

        private class ActiveText
        {
            public TextMeshProUGUI UIText;
            public float MaxTime;
            public float Timer;
            public Vector3 UnitPosition;

            public void MoveText(Camera camera)
            {
                var delta = 1.0f - Timer / MaxTime;
                var pos = camera.WorldToScreenPoint(UnitPosition + new Vector3(delta, delta, 0f));
                pos.z = 0.0f;
                UIText.transform.position = pos;
            }
        }
    }
}