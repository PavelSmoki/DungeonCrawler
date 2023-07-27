using System.Collections.Generic;
using UnityEngine;

namespace Game.Items.Weapons
{
    public abstract class Item : MonoBehaviour
    {
        public List<ItemInfo> Infos { get; protected set; }
    }
}