using Game.Items.Armor;
using Game.Items.Weapons;
using UnityEngine;

namespace Game.Player
{
    public interface IPlayer
    {
        Vector2 GetCurrentPosition();
        WeaponBase TakeItem(WeaponBase item, Transform chestTransform);
        Armor TakeItem(Armor item, Transform chestTransform);
    }
}