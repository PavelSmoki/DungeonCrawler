namespace Game.Gameplay
{
    public class Heart 
    {
        public int Amount { get; private set; }
        public HeartType HeartType { get; private set; }

        public Heart(int amount, HeartType heartType)
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
