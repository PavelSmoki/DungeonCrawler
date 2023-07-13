using System.Collections.Generic;
using Game.Enemies;
using UnityEngine;

namespace Game.Gameplay
{
    public class RoomData : MonoBehaviour
    {
        [field: SerializeField] public List<EnemyInfo> EnemiesInfos { get; private set; }
    }
}