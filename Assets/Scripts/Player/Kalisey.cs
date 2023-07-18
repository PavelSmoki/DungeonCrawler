using Game.Gameplay;

namespace Game.Player
{
    public class Kalisey : PlayerBase
    {
        private void Start()
        {
            Hearts.Push(new Heart(2, HeartType.Red));
            Hearts.Push(new Heart(2, HeartType.Red));
            Hearts.Push(new Heart(2, HeartType.Red));
        }
    }
}