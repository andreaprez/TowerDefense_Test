using TowerDefense.Currency;
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
        }

        private void OnDestroy()
        {
            _signalService.GetSignal<ToggledSelectionForTurretSignal>().RemoveListener(OnToggleSelectionForTurret);
            _signalService.GetSignal<AttemptedTurretPlacementSignal>().RemoveListener(OnTurretPlacementAttempt);
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

            Instantiate(_selectedTurret, position, Quaternion.identity, transform);
            _signalService.GetSignal<ToggledSelectionForTurretSignal>().Send(_selectedTurretType);
            _currencyService.RemoveCoins(_turretsBuildConfig.TurretCost);
        }
    }
}