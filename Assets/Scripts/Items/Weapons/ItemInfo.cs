namespace Game.Items.Weapons
{
    public struct ItemInfo
    {
        public string Id;
        public float Stat;

        public ItemInfo(string id, float stat)
        {
            Id = id;
            Stat = stat;
        }
    }
}