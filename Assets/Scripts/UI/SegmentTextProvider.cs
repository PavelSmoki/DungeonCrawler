using UnityEngine;

namespace Game.UI
{
    [CreateAssetMenu(menuName = "Tools/SegmentInfoProvider", fileName = "SegmentInfoProvider")]
    public class SegmentTextProvider : ScriptableObject
    {
        [field:SerializeField] public Sprite MoveSpeedBonusSprite { get; private set; }
        [field:SerializeField] public Sprite DamageBonusSprite { get; private set; }
        [field:SerializeField] public Sprite CritChanceBonusSprite { get; private set; }
        [field:SerializeField] public Sprite AttackSpeedBonusSprite { get; private set; }
        [field:SerializeField] public Sprite AttackRangeBonusSprite { get; private set; }
        [field:SerializeField] public Sprite ShotSpeedBonusSprite { get; private set; }
        [field:SerializeField] public Sprite DamageSprite { get; private set; }
        [field:SerializeField] public Sprite CritChanceSprite { get; private set; }
        [field:SerializeField] public Sprite CritModifierSprite { get; private set; }
        [field:SerializeField] public Sprite AttackSpeedSprite { get; private set; }
        [field:SerializeField] public Sprite AttackRangeSprite { get; private set; }
        [field: SerializeField] public Sprite ShotSpeedSprite { get; private set; }

        public Sprite GetFragmentInfo(out string text, string id, float stat)
        {
            switch (id)
            {
                case "MoveSpeedBonus":
                {
                    text = $"Move Speed: + {stat * 100}%";
                    return MoveSpeedBonusSprite;
                }
                case "DamageBonus":
                {
                    text = $"Damage: + {stat * 100}%";
                    return DamageBonusSprite;
                }
                case "CritChanceBonus":
                {
                    text = $"CritChance: + {stat * 100}%";
                    return CritChanceBonusSprite;
                }
                case "AttackSpeedBonus":
                {
                    text = $"AttackSpeed: + {stat * 100}%";
                    return AttackSpeedBonusSprite;
                }
                case "AttackRangeBonus":
                {
                    text =$"AttackRange: + {stat * 100}%";
                    return AttackRangeBonusSprite;
                }
                case "ShotSpeedBonus":
                {
                    text =$"ShotSpeed: + {stat * 100}%";
                    return ShotSpeedBonusSprite;
                }
                case "Damage":
                {
                    text =$"Damage: {stat}";
                    return DamageSprite;
                }
                case "CritChance":
                {
                    text =$"CritChance: {stat * 100}%";
                    return CritChanceSprite;
                }
                case "CritModifier":
                {
                    text = $"CritModifier: {stat}x";
                    return CritModifierSprite;
                }
                case "AttackSpeed":
                {
                    text =$"AttackSpeed: {stat}";
                    return AttackSpeedSprite;
                }
                case "AttackRange":
                {
                    text =$"AttackRange: {stat}";
                    return AttackRangeSprite;
                }
                case "ShotSpeed":
                {
                    text = $"ShotSpeed: {stat}";
                    return ShotSpeedSprite;
                }
                default:
                {
                    text = "";
                    return null;
                }
            }
        }
    }
}