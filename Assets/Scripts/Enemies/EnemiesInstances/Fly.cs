using UnityEngine;

namespace Game.Enemies.EnemiesInstances
{
    public class Fly : EnemyBase
    {
        private void Update()
        {
            Follow();
        }

        private void Follow()
        {
            if (IsDelayed)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.GetCurrentPosition(),
                    Speed * Time.deltaTime);
            }

            /*
            var distance = Vector2.Distance(transform.position, _player.transform.position);
            var direction = _player.transform.position - transform.position;
            */
        }
    }
}