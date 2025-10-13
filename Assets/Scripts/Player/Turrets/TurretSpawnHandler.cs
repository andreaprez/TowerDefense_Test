using System;
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

        private SignalService _signalService;
        private CurrencyService _currencyService;
        private Turret _selectedTurret;
        private TurretType _selectedTurretType;
        private List<Turret> _spawnedTurrets;

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
            _signalService.GetSignal<LevelRestartedSignal>().AddListener(OnLevelRestarted);

            _spawnedTurrets = new List<Turret>();
        }

        private void OnDestroy()
        {
            _signalService.GetSignal<ToggledSelectionForTurretSignal>().RemoveListener(OnToggleSelectionForTurret);
            _signalService.GetSignal<AttemptedTurretPlacementSignal>().RemoveListener(OnTurretPlacementAttempt);
            _signalService.GetSignal<LevelRestartedSignal>().RemoveListener(OnLevelRestarted);
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

        private void OnLevelRestarted()
        {
            foreach (var turret in _spawnedTurrets)
            {
                Destroy(turret);
            }
            _spawnedTurrets.Clear();
        }
    }
}