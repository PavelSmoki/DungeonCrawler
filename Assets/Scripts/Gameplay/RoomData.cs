using System;
using System.Collections.Generic;
using Game.Enemies;
using UniRx;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Gameplay
{
    public class RoomData : MonoBehaviour
    {
        [field: SerializeField] public List<EnemyInfo> EnemiesInfos { get; private set; }

        [SerializeField] private Tilemap _wallTilemap;

        public IReactiveProperty<int> EnemyCount { get; } = new ReactiveProperty<int>(0);
        public Action<RoomData> OnRoomEnter;
        public bool IsRoomCleared { get; private set; }

        public RoomTransition LeftTransition { get; set; }
        public RoomTransition RightTransition { get; set; }
        public RoomTransition UpperTransition { get; set; }
        public RoomTransition LowerTransition { get; set; }


        public void RemoveHorizontalWallOnTransition(Vector3 worldPos)
        {
            for (var i = 0; i < 3; i++)
            {
                var tilePos = _wallTilemap.WorldToCell(worldPos);
                worldPos.y -= 1;
                _wallTilemap.SetTile(tilePos, null);
            }
        }

        public void RemoveVerticalWallOnTransition(Vector3 worldPos)
        {
            for (var i = 0; i < 3; i++)
            {
                var tilePos = _wallTilemap.WorldToCell(worldPos);
                worldPos.x += 1;
                _wallTilemap.SetTile(tilePos, null);
                _wallTilemap.SetTile(tilePos + new Vector3Int(0, -1, 0), null);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnRoomEnter?.Invoke(this);
            IsRoomCleared = true;
        }

        public void OpenAllTransitions()
        {
            if (LeftTransition != null)
            {
                LeftTransition.Open();
            }

            if (RightTransition != null)
            {
                RightTransition.Open();
            }

            if (UpperTransition != null)
            {
                UpperTransition.Open();
            }

            if (LowerTransition != null)
            {
                LowerTransition.Open();
            }
        }

        public void CloseAllTransitions()
        {
            if (LeftTransition != null)
            {
                LeftTransition.Close();
            }

            if (RightTransition != null)
            {
                RightTransition.Close();
            }

            if (UpperTransition != null)
            {
                UpperTransition.Close();
            }

            if (LowerTransition != null)
            {
                LowerTransition.Close();
            }
        }
    }
}