using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Game.Enemies;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Game.Gameplay
{
    [UsedImplicitly]
    public class GameplayController : IInitializable
    {
        private const string StartingRoomKey = "StartingRoom";
        private const string LeftRightTransitionKey = "LeftRightTransition";
        private const string UpDownTransitionKey = "UpDownTransition";

        private GameObject[,] _spawnedRooms;
        private RoomData[,] _spawnedRoomsData;

        private readonly EnemyFactory _enemyFactory;
        private readonly DiContainer _container;
        private readonly Grid _grid;

        public GameplayController(EnemyFactory enemyFactory, DiContainer container, Grid grid)
        {
            _enemyFactory = enemyFactory;
            _container = container;
            _grid = grid;
        }

        public void Initialize()
        {
            _spawnedRooms = new GameObject[9, 9];
            _spawnedRoomsData = new RoomData[9, 9];

            _spawnedRooms[4, 4] = Addressables.LoadAssetAsync<GameObject>(StartingRoomKey).WaitForCompletion();
            var startingRoom = Object.Instantiate(_spawnedRooms[4, 4].GetComponent<RoomData>());
            _spawnedRoomsData[4, 4] = startingRoom;

            _enemyFactory.Create(startingRoom);
            _container.InjectGameObject(startingRoom.gameObject);

            for (var i = 0; i < 10; i++)
            {
                PlaceOneRoom();
            }
        }

        private void PlaceOneRoom()
        {
            var vacantPlaces = new HashSet<Vector2Int>();
            for (var x = 0; x < _spawnedRooms.GetLength(0); x++)
            {
                for (var y = 0; y < _spawnedRooms.GetLength(1); y++)
                {
                    if (_spawnedRooms[x, y] == null) continue;

                    var maxX = _spawnedRooms.GetLength(0) - 1;
                    var maxY = _spawnedRooms.GetLength(1) - 1;

                    if (x > 0 && _spawnedRooms[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
                    if (y > 0 && _spawnedRooms[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));
                    if (x < maxX && _spawnedRooms[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
                    if (y < maxY && _spawnedRooms[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));
                }
            }

            var newRoomPrefab = Addressables.LoadAssetAsync<GameObject>("Room" + Random.Range(1, 4))
                .WaitForCompletion();
            var newRoomObj = Object.Instantiate(newRoomPrefab);
            var newRoomData = newRoomObj.GetComponent<RoomData>();
            var position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
            newRoomObj.transform.position = new Vector3((position.x - 4) * 30, (position.y - 4) * 30, 0);
            
            _spawnedRooms[position.x, position.y] = newRoomObj;
            _spawnedRoomsData[position.x, position.y] = newRoomData;
            
            PlaceTransitions(newRoomData, position);
        }

        private void PlaceTransitions(RoomData room, Vector2Int pos)
        {
            // Left Neighbour
            if (_spawnedRoomsData[pos.x - 1, pos.y] != null && _spawnedRoomsData[pos.x - 1, pos.y].RightTransition == null)
            {
                var transitionPrefab =
                    Addressables.LoadAssetAsync<GameObject>(LeftRightTransitionKey).WaitForCompletion();
                var transition = Object.Instantiate(transitionPrefab, _grid.transform);
                transition.transform.position = room.transform.position + Vector3.left * 15;
                room.LeftTransition = transition;
                _spawnedRoomsData[pos.x - 1, pos.y].RightTransition = transition;
            }

            // Right Neighbour
            if (_spawnedRoomsData[pos.x + 1, pos.y] != null && _spawnedRoomsData[pos.x + 1, pos.y].LeftTransition == null)
            {
                var transitionPrefab =
                    Addressables.LoadAssetAsync<GameObject>(LeftRightTransitionKey).WaitForCompletion();
                var transition = Object.Instantiate(transitionPrefab, _grid.transform);
                transition.transform.position = room.transform.position + Vector3.right * 15;
                room.RightTransition = transition;
                _spawnedRoomsData[pos.x + 1, pos.y].LeftTransition = transition;
            }

            // Up Neighbour
            if (_spawnedRoomsData[pos.x, pos.y + 1] != null && _spawnedRoomsData[pos.x, pos.y + 1].DownTransition == null)
            {
                var transitionPrefab =
                    Addressables.LoadAssetAsync<GameObject>(UpDownTransitionKey).WaitForCompletion();
                var transition = Object.Instantiate(transitionPrefab, _grid.transform);
                transition.transform.position = room.transform.position + Vector3.up * 15;
                room.UpTransition = transition;
                _spawnedRoomsData[pos.x, pos.y + 1].DownTransition = transition;
            }

            // Down Neighbour
            if (_spawnedRoomsData[pos.x, pos.y - 1] != null && _spawnedRoomsData[pos.x, pos.y - 1].UpTransition == null)
            {
                var transitionPrefab =
                    Addressables.LoadAssetAsync<GameObject>(UpDownTransitionKey).WaitForCompletion();
                var transition = Object.Instantiate(transitionPrefab, _grid.transform);
                transition.transform.position = room.transform.position + Vector3.down * 15;
                room.DownTransition = transition;
                _spawnedRoomsData[pos.x, pos.y - 1].UpTransition = transition;
            }
        }
    }
}