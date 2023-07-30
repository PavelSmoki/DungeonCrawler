namespace Game.Items
{
    public struct ItemInfo
    {
        public string Id { get; private set; }
        public float Stat { get; private set; }

        public ItemInfo(string id, float stat)
        {
            Id = id;
            Stat = stat;
        }
    }
}