namespace Game.Gameplay
{
    public class Heart 
    {
        public int Amount { get; private set; }

        public Heart(int amount)
        {
            Amount = amount;
        }

        public void TakeDamage()
        {
            Amount--;
        }
    }
}
