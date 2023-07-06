namespace Game
{
    public class Kalisey : APlayer
    {
        private void Start()
        {
            Hearts.Push(new Heart(2, EHeartType.RED));
            Hearts.Push(new Heart(2, EHeartType.RED));
            Hearts.Push(new Heart(2, EHeartType.RED));
        }
    }
}