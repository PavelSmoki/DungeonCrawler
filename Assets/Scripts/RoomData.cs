using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class RoomData : MonoBehaviour
    {
        [field:SerializeField] public List<EnemySpawn> EnemiesPos { get; private set; }
        [field: SerializeField] public List<EnemyData> EnemiesData { get; private set; }
        
    }
}


