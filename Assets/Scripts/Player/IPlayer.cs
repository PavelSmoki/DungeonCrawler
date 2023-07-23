using UnityEngine;

namespace Game.Player
{
    public interface IPlayer
    {
        Vector2 GetCurrentPosition();
        void TakeItem(GameObject item);
    }
}