using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Tools/Enemy Data", fileName = "EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        // [field: SerializeField] public GameObject Particle { get; private set; }
    }
}