using Game.Items.Armor;
using Game.Items.Weapons;
using UnityEngine;

namespace Game.Player
{
    public interface IPlayer
    {
        Vector2 GetCurrentPosition();
        Camera GetCamera();
        WeaponBase TakeItem(WeaponBase item);
        Armor TakeItem(Armor item);
    }
}