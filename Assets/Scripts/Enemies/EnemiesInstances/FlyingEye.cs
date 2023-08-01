using UnityEngine;

namespace Game.Enemies.EnemiesInstances
{
    public class FlyingEye : EnemyBase
    {
        protected override void Update()
        {
            base.Update();
            Follow();
        }

        private void Follow()
        {
            if (IsDelayed)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.GetCurrentPosition(),
                    Speed * Time.deltaTime);
            }
        }
    }
}