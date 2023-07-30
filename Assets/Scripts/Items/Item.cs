using System.Collections.Generic;
using UnityEngine;

namespace Game.Items
{
    public abstract class Item : MonoBehaviour
    {
        [field: SerializeField] public string Name { get; protected set; }
        [field: SerializeField] public Rareness Rareness { get; protected set; }
        public List<ItemInfo> Infos { get; protected set; }
    }
}