namespace Game
{
    public class Heart 
    {
        public int Amount { get; private set; }
        public EHeartType HeartType { get; private set; }

        public Heart(int amount, EHeartType heartType)
        {
            Amount = amount;
            HeartType = heartType;
        }

        public void TakeDamage()
        {
            Amount--;
        }
    }
}
