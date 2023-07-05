using UnityEngine;
using Zenject;

namespace Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField][Range(0.8f, 2f)] private float _speed;
        [SerializeField] private Rigidbody2D _rb;
        private FixedJoystick _joystick;
        
        //TODO Player health and taking damage

        [Inject]
        private void Construct(FixedJoystick joystick)
        {
            _joystick = joystick;
        }

        private void FixedUpdate()
        {
            _rb.velocity = new Vector2(_joystick.Horizontal * 8 * _speed, _joystick.Vertical * 8 * _speed);
        }
    }
}