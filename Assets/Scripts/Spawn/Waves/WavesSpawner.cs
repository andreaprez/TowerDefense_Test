using System.Collections.Generic;
using TowerDefense.Enemies;
using TowerDefense.GameFlow;
using TowerDefense.Service;
using TowerDefense.Signals;
using TowerDefense.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TowerDefense.Spawn
{
    public class WavesSpawner : MonoBehaviour
    {
        [SerializeField] private WavesSpawnConfig _spawnConfig;
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private float _spawnPointOffset;

        private SignalService _signalService;
        private int _currentWave;
        private List<Enemy> _spawnedEnemies;

        private void Start()
        {
            Initialize();
            SpawnWave();
        }

        private void Initialize()
        {
            _signalService = ServiceLocator.GetService<SignalService>();
            _signalService.GetSignal<EnemyDiedSignal>().AddListener(OnEnemyDied);
            _signalService.GetSignal<GameRestartedSignal>().AddListener(OnGameRestarted);
        }

        private void OnDestroy()
        {
            _signalService.GetSignal<EnemyDiedSignal>().RemoveListener(OnEnemyDied);
            _signalService.GetSignal<GameRestartedSignal>().RemoveListener(OnGameRestarted);
        }

        private void SpawnWave()
        {
            var currentWaveConfig = _spawnConfig.Waves[_currentWave];
            _spawnedEnemies = new List<Enemy>();

            foreach (var enemyGroup in currentWaveConfig.EnemyGroups)
            {
                for (var i = 0; i < enemyGroup.Quantity; i++)
                {
                    var spawnPointOffsetX = _spawnPointOffset * Random.Range(-1, 2);
                    var spawnPointOffsetZ = _spawnPointOffset * Random.Range(-1, 2);
                    var spawnPointOffsetVector = new Vector3(spawnPointOffsetX, 0f, spawnPointOffsetZ);
                    var spawnPointIndex = Random.Range(0, _spawnPoints.Count);
                    var spawnPosition = _spawnPoints[spawnPointIndex].position + spawnPointOffsetVector;

                    GameObject newEnemyObject = null;
                    switch (enemyGroup.Prefab.GetEnemyType())
                    {
                        case EnemyType.SmallCreep:
                            newEnemyObject = PoolHandler.Instance.SmallCreepsPool.Get();
                            break;
                        case EnemyType.BigCreep:
                            newEnemyObject = PoolHandler.Instance.BigCreepsPool.Get();
                            break;
                    }
                    if (newEnemyObject == null)
                        continue;

                    newEnemyObject.transform.position = spawnPosition;
                    newEnemyObject.transform.rotation = Quaternion.identity;
                    var newEnemy = newEnemyObject.GetComponent<Enemy>();
                    newEnemy.Initialize();
                    _spawnedEnemies.Add(newEnemy);
                }
            }
        }

        private void OnEnemyDied(Enemy enemy)
        {
            _spawnedEnemies.Remove(enemy);
            if (_spawnedEnemies.Count > 0)
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

        private void OnGameRestarted()
        {
            foreach (var enemy in _spawnedEnemies)
            {
                switch (enemy.GetEnemyType())
                {
                    case EnemyType.SmallCreep:
                        PoolHandler.Instance.SmallCreepsPool.Release(enemy.gameObject);
                        break;
                    case EnemyType.BigCreep:
                        PoolHandler.Instance.BigCreepsPool.Release(enemy.gameObject);
                        break;
                }
            }
            _spawnedEnemies.Clear();

            _currentWave = 0;
            SpawnWave();
        }
    }
}