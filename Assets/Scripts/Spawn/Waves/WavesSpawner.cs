using System.Collections.Generic;
using TowerDefense.Enemies;
using TowerDefense.GameFlow;
using TowerDefense.Service;
using TowerDefense.Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TowerDefense.Spawn
{
    public class WavesSpawner : MonoBehaviour
    {
        [SerializeField] private WavesSpawnConfig _spawnConfig;
        [SerializeField] private List<Transform> _spawnPoints;

        private SignalService _signalService;
        private int _currentWave;
        private int _enemyCounter;

        private void Start()
        {
            Initialize();
            SpawnWave();
        }

        private void Initialize()
        {
            _signalService = ServiceLocator.GetService<SignalService>();
            _signalService.GetSignal<EnemyDiedSignal>().AddListener(OnEnemyDied);
        }

        private void OnDestroy()
        {
            _signalService.GetSignal<EnemyDiedSignal>().RemoveListener(OnEnemyDied);
        }

        private void SpawnWave()
        {
            var currentWaveConfig = _spawnConfig.Waves[_currentWave];
            _enemyCounter = 0;

            foreach (var enemyGroup in currentWaveConfig.EnemyGroups)
            {
                for (var i = 0; i < enemyGroup.Quantity; i++)
                {
                    var spawnPointIndex = Random.Range(0, _spawnPoints.Count);
                    Instantiate(enemyGroup.Prefab, _spawnPoints[spawnPointIndex].position, Quaternion.identity, transform);
                    _enemyCounter++;
                }
            }
        }

        private void OnEnemyDied()
        {
            _enemyCounter--;
            if (_enemyCounter > 0)
                return;

            if (_currentWave < _spawnConfig.Waves.Count - 1)
                MoveToNextWave();
            else
                TriggerGameWin();
        }

        private void MoveToNextWave()
        {
            _currentWave++;
            SpawnWave();
        }
 
        private void TriggerGameWin()
        {
            _signalService.GetSignal<GameEndedSignal>().Send(true);
        }
    }
}