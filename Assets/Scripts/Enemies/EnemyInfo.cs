using System;
using UnityEngine;

namespace Game.Enemies
{
    [Serializable]
    public struct EnemyInfo
    {
        public EnemySpawn EnemySpawn;
        public GameObject Prefab;
    }
}