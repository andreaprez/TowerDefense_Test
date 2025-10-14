using System.Collections.Generic;
using TowerDefense.Currency;
using TowerDefense.GameFlow;
using TowerDefense.Input;
using TowerDefense.Service;
using TowerDefense.Signals;
using UnityEngine;

namespace TowerDefense.Player
{
    public class TurretSpawnHandler : MonoBehaviour
    {
        [SerializeField] private TurretsBuildConfig _turretsBuildConfig;
        [SerializeField] private TurretGhost _turretGhostPrefab;

        private SignalService _signalService;
        private CurrencyService _currencyService;
        private Turret _selectedTurret;
        private TurretType _selectedTurretType;
        private List<Turret> _spawnedTurrets;
        private TurretGhost _turretGhost;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _signalService = ServiceLocator.GetService<SignalService>();
            _currencyService = ServiceLocator.GetService<CurrencyService>();

            _signalService.GetSignal<ToggledSelectionForTurretSignal>().AddListener(OnToggleSelectionForTurret);
            _signalService.GetSignal<AttemptedTurretPlacementSignal>().AddListener(OnTurretPlacementAttempt);
            _signalService.GetSignal<GameRestartedSignal>().AddListener(OnGameRestarted);

            _spawnedTurrets = new List<Turret>();
        }

        private void OnDestroy()
        {
            _signalService.GetSignal<ToggledSelectionForTurretSignal>().RemoveListener(OnToggleSelectionForTurret);
            _signalService.GetSignal<AttemptedTurretPlacementSignal>().RemoveListener(OnTurretPlacementAttempt);
            _signalService.GetSignal<GameRestartedSignal>().RemoveListener(OnGameRestarted);
        }

        private void OnToggleSelectionForTurret(TurretType turretType)
        {
            Turret turretPrefab = null;
            switch (turretType)
            {
                case TurretType.Regular:
                    turretPrefab = _turretsBuildConfig.RegularTurretPrefab;
                    break;
                case TurretType.Freeze:
                    turretPrefab = _turretsBuildConfig.FreezeTurretPrefab;
                    break;
            }
            _selectedTurret = _selectedTurret == turretPrefab ? null : turretPrefab;
            _selectedTurretType = turretType;

            if (_selectedTurret && !_turretGhost)
                InstantiateTurretGhost();
            else if (!_selectedTurret)
                DestroyTurretGhost();
        }

        private void InstantiateTurretGhost()
        {
            var mousePos = Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y, -Camera.main.transform.position.z));
            _turretGhost = Instantiate(_turretGhostPrefab, mousePos, Quaternion.identity, transform);
        }

        private void DestroyTurretGhost()
        {
            Destroy(_turretGhost.gameObject);
            _turretGhost = null;
        }

        private void OnTurretPlacementAttempt(Vector3 position)
        {
            if (_selectedTurret == null)
                return;

            if (_turretsBuildConfig.TurretCost > _currencyService.Coins)
                return;

            var newTurret = Instantiate(_selectedTurret, position, Quaternion.identity, transform);
            _spawnedTurrets.Add(newTurret);
            _currencyService.RemoveCoins(_turretsBuildConfig.TurretCost);
            _signalService.GetSignal<ToggledSelectionForTurretSignal>().Send(_selectedTurretType);
        }

        private void OnGameRestarted()
        {
            foreach (var turret in _spawnedTurrets)
            {
                Destroy(turret.gameObject);
            }
            _spawnedTurrets.Clear();

            if (_selectedTurret)
            {
                _selectedTurret = null;
                DestroyTurretGhost();
            }
        }
    }
}