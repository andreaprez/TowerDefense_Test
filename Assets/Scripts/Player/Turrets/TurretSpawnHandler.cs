using System;
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
        private Turret _selectedTurret;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _signalService = ServiceLocator.GetService<SignalService>();
            _signalService.GetSignal<ToggledSelectionForRegularTurretSignal>().AddListener(OnToggleSelectionForRegularTurret);
            _signalService.GetSignal<PlacedTurretSignal>().AddListener(OnPlaceTurret);
        }

        private void OnDestroy()
        {
            _signalService.GetSignal<ToggledSelectionForRegularTurretSignal>().RemoveListener(OnToggleSelectionForRegularTurret);
            _signalService.GetSignal<PlacedTurretSignal>().RemoveListener(OnPlaceTurret);
        }

        private void OnToggleSelectionForRegularTurret()
        {
            _selectedTurret = _selectedTurret == _turretsBuildConfig.RegularTurretPrefab ? null : _turretsBuildConfig.RegularTurretPrefab;
        }

        private void OnPlaceTurret(Vector3 position)
        {
            if (_selectedTurret == null)
                return;

            Instantiate(_selectedTurret, position, Quaternion.identity, transform);
            _signalService.GetSignal<ToggledSelectionForRegularTurretSignal>().Send();
        }
    }
}