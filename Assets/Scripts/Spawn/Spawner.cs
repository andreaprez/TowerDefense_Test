using System.Collections.Generic;
using TowerDefense.GameFlow;
using TowerDefense.Service;
using UnityEngine;

namespace TowerDefense.Spawn
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private SpawnConfig _spawnConfig;
        [SerializeField] private List<Transform> _spawnPoints;

        private GameFlowService _gameFlowService;
        private float _elapsedTime;

        private void Start()
        {
            ValidateReferences();
            Initialize();
        }

        private void ValidateReferences()
        {
            if (_spawnConfig == null)
                Debug.LogError("Spawn configuration is null. Please reference a SpawnConfig in the Spawner");

            if (_spawnPoints == null || _spawnPoints.Count == 0)
                Debug.LogError("List of spawn points is empty. Please reference at least one SpawnPoint in the Spawner");
        }

        private void Initialize()
        {
            _gameFlowService = ServiceLocator.GetService<GameFlowService>();
        }

        private void Update()
        {
            if (_gameFlowService.GameState != GameState.Gameplay ||
                _spawnConfig == null || _spawnPoints == null || _spawnPoints.Count == 0)
                return;

            if (_elapsedTime < _spawnConfig.SpawnFrequencyInSeconds)
            {
                _elapsedTime += Time.deltaTime;
                return;
            }

            _elapsedTime = 0;
            Spawn();
        }

        private void Spawn()
        {
            if (_spawnConfig.SpawnMultipleInSamePoint)
            {
                var spawnPointIndex = Random.Range(0, _spawnPoints.Count);
                for (var i = 0; i < _spawnConfig.SpawnQuantity; i++)
                {
                    var prefabIndex = Random.Range(0, _spawnConfig.PrefabsToSpawn.Count);
                    Instantiate(_spawnConfig.PrefabsToSpawn[prefabIndex], _spawnPoints[spawnPointIndex].position, Quaternion.identity);
                }
            }
            else
            {
                for (var i = 0; i < _spawnConfig.SpawnQuantity; i++)
                {
                    var spawnPointIndex = Random.Range(0, _spawnPoints.Count);
                    var prefabIndex = Random.Range(0, _spawnConfig.PrefabsToSpawn.Count);
                    Instantiate(_spawnConfig.PrefabsToSpawn[prefabIndex], _spawnPoints[spawnPointIndex].position, Quaternion.identity);
                }
            }
        }
    }
}
