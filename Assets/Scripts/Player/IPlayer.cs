using Game.Items.Armor;
using Game.Items.Weapons;
using UnityEngine;

namespace Game.Player
{
    public interface IPlayer
    {
        Vector2 GetCurrentPosition();
        GameObject TakeItem(WeaponBase item);
        GameObject TakeItem(Armor item);
    }
}