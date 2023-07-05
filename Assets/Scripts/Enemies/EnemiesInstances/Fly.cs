using UnityEngine;

namespace Game
{
    public class Fly : AEnemy
    {
        private void Update()
        {
            Follow();
        }

        private void Follow()
        {
            if (_isDelayed)
            {
                transform.position = Vector2.MoveTowards(transform.position, _player.transform.position,
                    _enemyData.Speed * Time.deltaTime);
            }

            /*
            var distance = Vector2.Distance(transform.position, _player.transform.position);
            var direction = _player.transform.position - transform.position;
            */
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                // TODO Taking Damage
            }
        }
    }
}