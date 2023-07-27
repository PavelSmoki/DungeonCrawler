using System.Collections.Generic;
using Game.Enemies;
using UnityEngine;

namespace Game.Gameplay
{
    public class RoomData : MonoBehaviour
    {
        [field: SerializeField] public List<EnemyInfo> EnemiesInfos { get; private set; }

        public GameObject LeftTransition { get; set; }
        public GameObject RightTransition { get; set; }
        public GameObject UpTransition { get; set; }
        public GameObject DownTransition { get; set; }
    }
}