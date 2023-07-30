using UnityEngine;

namespace Game.Gameplay
{
    public class RoomTransition : MonoBehaviour
    {
        private static readonly int CloseId = Animator.StringToHash("Close");
        private static readonly int OpenId = Animator.StringToHash("Open");

        [SerializeField] private Collider2D[] _colliders2D;
        [SerializeField] private Animator[] _animators;

        public void Open()
        {
            for(var i = 0; i < _colliders2D.Length; i++)
            {
                _colliders2D[i].isTrigger = true;
                _animators[i].SetTrigger(OpenId);
            }
        }

        public void Close()
        {
            for(var i = 0; i < _colliders2D.Length; i++)
            {
                _colliders2D[i].isTrigger = false;
                _animators[i].SetTrigger(CloseId);
            }
        }
    }
}